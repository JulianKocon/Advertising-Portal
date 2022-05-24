using System.Net;
using System.Threading.Tasks;
using AdvertisingPortal.DTO;
using AdvertisingPortal.Entities;
using AdvertisingPortal.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> GetAdvertisement(int IdAdvertisement)
        {
            Advertisement ad = await _advertisementDbService.GetAdvertisementAsync(IdAdvertisement);
            if (ad == null)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, "No advertisement with given Id");
            }
            else
            {
                return Ok(ad);
            }
        }

        [Authorize]
        [HttpGet("{IdAdvertisement}/detailed")]
        public async Task<IActionResult> GetDetailedAdvertisement(int IdAdvertisement)
        {
            AdvertisementDetailedDTO ad = await _advertisementDbService.GetDetailedAdvertisementAsync(IdAdvertisement);

            if(ad == null)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, "No advertisement with given Id");
            }
            return Ok(ad);
        }
    }
}
