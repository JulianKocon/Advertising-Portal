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
            ResultMessageDTO result = await _purchaseOrderDbService.Purchase(idAdvertisement, username);

            return StatusCode((int)result.HttpStatus, result.Message);
        }
    }
}
