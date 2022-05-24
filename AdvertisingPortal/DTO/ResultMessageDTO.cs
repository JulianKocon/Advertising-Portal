using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AdvertisingPortal.DTO
{
    public class ResultMessageDTO
    {
        public HttpStatusCode HttpStatus { get; set; }
        public string Message { get; set; }
    }
}
