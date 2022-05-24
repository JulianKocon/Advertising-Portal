using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdvertisingPortal.Entities;

namespace AdvertisingPortal.DTO
{
    public class AdvertisementDetailedDTO
    {
        public UserDTO Owner { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public virtual ICollection<CategoryDTO> Categories { get; set; }
    }
}
