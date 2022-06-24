using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdvertisingPortal.DTO;

namespace AdvertisingPortal.Services.Interfaces
{
    public interface IPurchaseOrderDbService
    {
        Task<ResultMessageDTO> Purchase(int idAdvertisement, string username);
    }
}
