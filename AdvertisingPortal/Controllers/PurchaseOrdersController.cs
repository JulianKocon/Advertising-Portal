using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdvertisingPortal.DTO;
using AdvertisingPortal.Entities;
using AdvertisingPortal.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdvertisingPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseOrdersController : ControllerBase
    {
        private readonly IPurchaseOrderDbService _purchaseOrderDbService;

        public PurchaseOrdersController(IPurchaseOrderDbService purchaseOrderDbService)
        {
            _purchaseOrderDbService = purchaseOrderDbService;
        }


        [Authorize]
        [HttpGet("{IdAdvertisement}")]
        public async Task<IActionResult> BuyProduct(int idAdvertisement)
        {
            string username = User.Identity.Name;

            Advertisement ad = _purchaseOrderDbService.GetAdvertisement(idAdvertisement);
            if (ad == null)
            {
                return BadRequest("No advertisement available with given id.");
            }
            else if(ad.IsAvailable){
                return BadRequest("This advetisement is archived.");
            }

            User buyer = _purchaseOrderDbService.GetUser(username);

            if (buyer.Money < ad.Price)
            {
                return BadRequest("You don't have enough money.");
            }
            else
            {
                var result = await _purchaseOrderDbService.Purchase(buyer, ad);
                return Ok(result);
            }

        }
    }
}
