
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AdvertisingPortal.Entities
{
     public class Category
    {
        public Category()
        {
            Advertisements = new HashSet<Advertisement>();
        }

        [Key]
        public int IdCategory { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Advertisement> Advertisements { get; set; }
    }
}