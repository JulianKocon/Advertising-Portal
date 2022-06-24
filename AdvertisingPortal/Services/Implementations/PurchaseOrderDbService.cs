using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AdvertisingPortal.DataAccess;
using AdvertisingPortal.DTO;
using AdvertisingPortal.Entities;
using AdvertisingPortal.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AdvertisingPortal.Services.Implementations
{
    public class PurchaseOrderDbService : IPurchaseOrderDbService
    {
        public readonly PortalDbContext _context;

        public PurchaseOrderDbService(PortalDbContext context)
        {
            _context = context;
        }

        public async Task<ResultMessageDTO> Purchase(int idAdvertisement, string username)
        {
            User buyer = await _context.Users.SingleOrDefaultAsync(x => x.Username.Equals(username));

            Advertisement ad = await _context.Advertisements.SingleOrDefaultAsync(x => x.IdAdvertisement == idAdvertisement);

            if(ad == null)
            {
                return new ResultMessageDTO
                {
                    HttpStatus = HttpStatusCode.BadRequest,
                    Message = "No advertisement available with given id."
                };
            }else if(ad.IsAvailable == false)
            {
                return new ResultMessageDTO
                {
                    HttpStatus = HttpStatusCode.BadRequest,
                    Message = "You can't buy archived advertisement."
                };
            }if(buyer.Money < ad.Price)
            {
                return new ResultMessageDTO
                {
                    HttpStatus = HttpStatusCode.BadRequest,
                    Message = "You don't have enough money."
                };
            }

            buyer.Money -= ad.Price;

            PurchaseOrder purchaseOrder = new()
            {
                IdUser = buyer.IdUser,
                IdAdvertisement = ad.IdAdvertisement,
                OrderDate = DateTime.UtcNow
            };

            ad.IsAvailable = false;
            await _context.PurchaseOrders.AddAsync(purchaseOrder);
            await _context.SaveChangesAsync();
            
            return new ResultMessageDTO
            {
                HttpStatus = HttpStatusCode.OK,
                Message = "Order completed"
            };
           
        }
    }
}
