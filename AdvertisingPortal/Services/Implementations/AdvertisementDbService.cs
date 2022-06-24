using AdvertisingPortal.DataAccess;
using AdvertisingPortal.DTO;
using AdvertisingPortal.Entities;
using AdvertisingPortal.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AdvertisingPortal.Services.Implementations
{
    public class AdvertisementDbService : IAdvertisementDbService
    {
        public readonly PortalDbContext _context;

        public AdvertisementDbService(PortalDbContext context)
        {
            _context = context;
        }

        public async Task<Advertisement> AddAdvertisementAsync(string username, AdvertisementToAddDTO advertisement)
        {

            var user = await _context.Users.SingleOrDefaultAsync(x => x.Username.Equals(username));


            if (await _context.Advertisements.AnyAsync(x => x.IdUser == user.IdUser && x.Name.Equals(advertisement.Name)))
            {
                return null;
            }

            Advertisement ad = new()
            {
                User = user,
                Name = advertisement.Name,
                Price = advertisement.Price,
                Description = advertisement.Description,
                Date = DateTime.UtcNow,
                Region = await _context.Regions.SingleOrDefaultAsync(x => x.Name.Equals(advertisement.Region)),
                IsAvailable = true
            };

            await _context.Advertisements.AddAsync(ad);
            await _context.SaveChangesAsync();

            return ad;
        }

        public async Task<ResultMessageDTO> DeleteAdvertisementAsync(string username, int idAdvertisement)
        {
            Advertisement ad = await _context.Advertisements
                                    .Where(x => x.User.Username.Equals(username))
                                    .SingleOrDefaultAsync(x => x.IdAdvertisement == idAdvertisement);
            if (ad == null)
            {
                return new ResultMessageDTO
                {
                    HttpStatus = HttpStatusCode.NotFound,
                    Message = "You don't have an advertisement with given id"
                };
            }
            else if (ad.IsAvailable == false)
            {
                return new ResultMessageDTO
                {
                    HttpStatus = HttpStatusCode.Unauthorized,
                    Message = "You can't delete archived advertisement"
                };
            }

            _context.Advertisements.Remove(ad);
            await _context.SaveChangesAsync();

            return new ResultMessageDTO
            {
                HttpStatus = HttpStatusCode.OK,
                Message = "Advertisement deleted"
            };

        }

        public async Task<Advertisement> GetAdvertisementAsync(int idAdvertisement)
        {
            return await _context.Advertisements.Where(x => x.IdAdvertisement == idAdvertisement).SingleOrDefaultAsync();
        }

        public async Task<AdvertisementDetailedDTO> GetDetailedAdvertisementAsync(int idAdvertisement)
        {
            Advertisement ad = await _context.Advertisements.SingleOrDefaultAsync(x => x.IdAdvertisement == idAdvertisement);

            if (ad == null)
            {
                return null;
            }

            IQueryable<AdvertisementDetailedDTO> adDTO = _context.Advertisements
                .Include(x => x.User)
                .Where(x => x.IdAdvertisement == idAdvertisement)
                .Select(x => new AdvertisementDetailedDTO
                {
                    Owner = new UserToAdvertisementDetailedDTO
                    {
                        Username = x.User.Username,
                        Email = x.User.Email
                    },
                    Name = x.Name,
                    Price = x.Price,
                    Description = x.Description,
                    Date = x.Date,
                    Categories = x.Categories.Select(y => new CategoryDTO
                    {
                        Name = y.Name
                    }).ToList()
                });

            AdvertisementDetailedDTO advertisementToReturn = adDTO.First();

            return advertisementToReturn;
        }

        public async Task<ResultMessageDTO> ModifyAdvertisementAsync(string username, int idAdvertisement, AdvertisementToAddDTO advertisementToAddDTO)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Username.Equals(username));

            Advertisement ad = await _context.Advertisements.Where(x => x.IdAdvertisement == idAdvertisement).SingleOrDefaultAsync(x => x.IdUser == user.IdUser);

            if (ad == null)
            {
                return new ResultMessageDTO
                {
                    HttpStatus = HttpStatusCode.NotFound,
                    Message = "Wrong advertisement id"
                };
            }
            else if (ad.IsAvailable == false)
            {
                return new ResultMessageDTO
                {
                    HttpStatus = HttpStatusCode.Unauthorized,
                    Message = "You can't modify archived advertisement"
                };
            }

            ad.Name = advertisementToAddDTO.Name;
            ad.Price = advertisementToAddDTO.Price;
            ad.Description = advertisementToAddDTO.Description;

            await _context.SaveChangesAsync();

            return new ResultMessageDTO
            {
                HttpStatus = HttpStatusCode.OK,
                Message = "Advertisement modified"
            };
        }
    }
}
