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
        Task<Advertisement> GetAdvertisementAsync(int idAdvertisement);
        Task<AdvertisementDetailedDTO>GetDetailedAdvertisementAsync(int idAdvertisement);
        Task<Advertisement> AddAdvertisementAsync(string username, AdvertisementToAddDTO advertisement);
        Task<ResultMessageDTO> ModifyAdvertisementAsync(string username, int idAdvertisement, AdvertisementToAddDTO advertisement);
        Task<ResultMessageDTO> DeleteAdvertisementAsync(string username, int idAdvertisement);
    }
}
