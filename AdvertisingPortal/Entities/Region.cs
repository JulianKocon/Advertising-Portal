using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AdvertisingPortal.Entities;

namespace AdvertisingPortal.DataAccess
{
    public class Region
    {
        public Region()
        {
            Advertisements = new HashSet<Advertisement>();
            Users = new HashSet<User>();
        }

        [Key]
        public int IdRegion { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Advertisement> Advertisements { get; set; }
    }
}