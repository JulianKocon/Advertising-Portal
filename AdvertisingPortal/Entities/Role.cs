using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AdvertisingPortal.Entities
{
    public class Role
    {
        [Key]
        public int IdRole { get; set; }
        public string Name { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}