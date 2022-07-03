using AdvertisingPortal.DataAccess;
using AdvertisingPortal.Entities;
using AdvertisingPortal.Services.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertisingPortal.Services.Implementations
{
    public class PurchaseOrderDbService : IPurchaseOrderDbService
    {
        public readonly PortalDbContext _context;

        public PurchaseOrderDbService(PortalDbContext context)
        {
            _context = context;
        }

        public async Task<PurchaseOrder> Purchase(User buyer, Advertisement ad)
        {

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

            return purchaseOrder;

        }

        public Advertisement GetAdvertisement(int idAdvertisement)
        {
            return _context.Advertisements.SingleOrDefault(x => x.IdAdvertisement == idAdvertisement);
        }

        public User GetUser(string username)
        {
            return _context.Users.SingleOrDefault(x => x.Username.Equals(username));
        }
    }
}
