using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AdvertisingPortal.Entities;

namespace AdvertisingPortal.DataAccess
{
    public class Region
    {
        [Key]
        public int IdRegion { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Advertisement> Advertisements { get; set; }
    }
}