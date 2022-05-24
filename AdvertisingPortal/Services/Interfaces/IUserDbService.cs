using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdvertisingPortal.DTO;
using AdvertisingPortal.Entities;

namespace AdvertisingPortal.Services.Interfaces
{
    public interface IUserDbService
    {
        Task<User> RegisterUserAsync(UserDTO userDTO);
    }
}
