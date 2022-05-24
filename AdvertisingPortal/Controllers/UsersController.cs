using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AdvertisingPortal.DTO;
using AdvertisingPortal.Entities;
using AdvertisingPortal.Services.Interfaces;
using Microsoft.AspNetCore.Http;
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
                return StatusCode((int) HttpStatusCode.Created, result);
            }

        }

    }
}
