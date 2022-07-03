using AdvertisingPortal.DTO;
using AdvertisingPortal.Entities;
using AdvertisingPortal.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace AdvertisingPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertisementsController : ControllerBase
    {
        private readonly IAdvertisementDbService _advertisementDbService;

        public AdvertisementsController(IAdvertisementDbService advertisementDbService)
        {
            _advertisementDbService = advertisementDbService;
        }


        [Authorize]
        [HttpGet("{IdAdvertisement}")]
        public async Task<IActionResult> GetAdvertisement(int idAdvertisement)
        {
            Advertisement ad = await _advertisementDbService.GetAdvertisementAsync(idAdvertisement);
            if (ad == null)
            {
                return BadRequest("No advertisement with given Id");
            }
            else
            {
                return Ok(ad);
            }
        }

        [Authorize]
        [HttpGet("{IdAdvertisement}/detailed")]
        public async Task<IActionResult> GetDetailedAdvertisement(int idAdvertisement)
        {

            AdvertisementDetailedDTO ad = await _advertisementDbService.GetDetailedAdvertisementAsync(idAdvertisement);

            if (ad == null)
            {
                return BadRequest("No advertisement with given Id");
            }
            return Ok(ad);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddAdvertisement(AdvertisementToAddDTO advertisement)
        {
            var username = User.Identity.Name;
            var result = await _advertisementDbService.AddAdvertisementAsync(username, advertisement);

            if (result == null)
            {
                return BadRequest("You already have adverisement with this name");
            }

            return StatusCode((int)HttpStatusCode.Created, advertisement);
        }

        [Authorize]
        [HttpPut("{IdAdvertisement}")]
        public async Task<IActionResult> ModifyAdvertisement(int idAdvertisement, AdvertisementToAddDTO advertisement)
        {
            var username = User.Identity.Name;

            Advertisement ad = await _advertisementDbService.GetUserAdvertisement(username, idAdvertisement);

            if (ad == null)
            {
                return NotFound("Wrong advertisement id");

            }
            else if (ad.IsAvailable == false)
            {
                return Unauthorized("You can't modify archived advertisement");

            }
            await _advertisementDbService.ModifyAdvertisementAsync(ad, advertisement);

            return Ok("Advertisement modified");
        }

        [Authorize]
        [HttpDelete("{IdAdvertisement}")]
        public async Task<IActionResult> DeleteAdvertisement(int idAdvertisement)
        {
            var username = User.Identity.Name;

            Advertisement ad = await _advertisementDbService.GetUserAdvertisement(username, idAdvertisement);

            if (ad == null)
            {
                return NotFound("You don't have an advertisement with given id");

            }
            else if (ad.IsAvailable == false)
            {
                return Unauthorized("You can't delete archived advertisement");

            }

            await _advertisementDbService.DeleteAdvertisementAsync(ad);

            return Ok("Advertisement deleted");
        }
    }
}
