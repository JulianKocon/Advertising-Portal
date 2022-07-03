using AdvertisingPortal.DTO;
using AdvertisingPortal.Entities;
using AdvertisingPortal.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace AdvertisingPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public readonly IUserDbService _userDbService;

        public UsersController(IUserDbService userDbService)
        {
            _userDbService = userDbService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(UserDTO userDTO)
        {
            if (_userDbService.CheckIfEmailIsOccupied(userDTO.Email) != null)
            {
                return BadRequest("This email is occupied");
            }
            else
            {
                var result = await _userDbService.RegisterUserAsync(userDTO);
                return StatusCode((int)HttpStatusCode.Created, result);
            }

        }

        [HttpPost("Login")]
        public IActionResult LoginUser(UserToLoginDTO userToLoginDTO)
        {
            User user = _userDbService.CheckIfEmailIsOccupied(userToLoginDTO.Email);

            if (user == null)
            {
                return BadRequest("This email is occupied");
            }
            else if (!_userDbService.VerifyPasswordHash(userToLoginDTO.Password, user))
            {
                return BadRequest("Wrong password");
            }
            else
            {
                var result = _userDbService.CreateToken(user);

                return Ok(result);
            }


        }
    }

}
