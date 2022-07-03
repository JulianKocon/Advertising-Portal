using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdvertisingPortal.DataAccess;
using AdvertisingPortal.DTO;
using AdvertisingPortal.Entities;

namespace AdvertisingPortal.Services.Interfaces
{
    public interface IPurchaseOrderDbService
    {
        Task<PurchaseOrder> Purchase(User buyer, Advertisement ad);
        public Advertisement GetAdvertisement(int idAdvertisement);
        public User GetUser(string username);
    }
}
