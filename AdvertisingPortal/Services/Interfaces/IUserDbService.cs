using System.Threading.Tasks;
using AdvertisingPortal.DTO;
using AdvertisingPortal.Entities;

namespace AdvertisingPortal.Services.Interfaces
{
    public interface IUserDbService
    {
        Task<User> RegisterUserAsync(UserDTO userDTO);
        string CreateToken(User user);
        User CheckIfEmailIsOccupied(string email);
        bool VerifyPasswordHash(string password, User user);
    }
}
