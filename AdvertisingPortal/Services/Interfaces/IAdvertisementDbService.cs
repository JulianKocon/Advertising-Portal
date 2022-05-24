using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdvertisingPortal.DTO;
using AdvertisingPortal.Entities;

namespace AdvertisingPortal.Services.Interfaces
{
    public interface IAdvertisementDbService
    {
        Task<Advertisement> GetAdvertisementAsync(int IdAdvertisement);
        Task<AdvertisementDetailedDTO>GetDetailedAdvertisementAsync(int IdAdvertisement);
    }
}
