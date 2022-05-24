using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdvertisingPortal.DataAccess;
using AdvertisingPortal.DTO;
using AdvertisingPortal.Entities;
using AdvertisingPortal.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AdvertisingPortal.Services.Implementations
{
    public class AdvertisementDbService : IAdvertisementDbService
    {
        public readonly PortalDbContext _context;

        public AdvertisementDbService(PortalDbContext context)
        {
            _context = context;
        }
        public async Task<Advertisement> GetAdvertisementAsync(int IdAdvertisement)
        {
            return await _context.Advertisements.Where(x => x.IdAdvertisement == IdAdvertisement).SingleOrDefaultAsync();
        }

        public async Task<AdvertisementDetailedDTO> GetDetailedAdvertisementAsync(int IdAdvertisement)
        {
            Advertisement ad = await _context.Advertisements.SingleOrDefaultAsync(x => x.IdAdvertisement == IdAdvertisement);

            if(ad == null)
            {
                return null;
            }

            IQueryable<AdvertisementDetailedDTO> adDTO = _context.Advertisements
                .Include(x => x.IdUser)
                .Where(x => x.IdAdvertisement == IdAdvertisement)
                .Select(x => new AdvertisementDetailedDTO
                {
                    Owner = new UserDTO
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
    }
}
