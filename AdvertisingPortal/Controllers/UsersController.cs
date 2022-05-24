using System.Net;
using System.Threading.Tasks;
using AdvertisingPortal.DTO;
using AdvertisingPortal.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
            var result = await _userDbService.RegisterUserAsync(userDTO);

            if (result == null)
            {
                return BadRequest("This email is occupied");
            }
            else
            {
                return StatusCode((int)HttpStatusCode.Created, result);
            }

        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginUser(UserToLoginDTO userToLoginDTO)
        {
            ResultMessageDTO result = await _userDbService.LoginUserAsync(userToLoginDTO);

            if(result.Token == null)
            {
                return StatusCode((int)result.HttpStatus, result.Message);
            }
            else
            {
                return StatusCode((int)result.HttpStatus, result.Token);
            }

        }

    }
}
