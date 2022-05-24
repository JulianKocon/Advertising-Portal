using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using AdvertisingPortal.DataAccess;

namespace AdvertisingPortal.Entities
{
    public class User
    {
        [Key]
        public int IdUser { get; set; }
        [Required]
        [MaxLength(50)]
        public string Username { get; set; }
        [Required]
        [MaxLength(100)]
        public string Email { get; set; }
        [Required]
        [MaxLength(2000)]
        public byte[] PasswordHash { get; set; }
        [Required]
        [MaxLength(2000)]
        public byte[] PasswordSalt { get; set; }
        
        [Column(TypeName = "money")]
        public decimal Money { get; set; }
        public int IdRegion { get; set; }

        [ForeignKey("IdRegion")]
        public string Region { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
        public virtual ICollection<Advertisement> Advertisements { get; set; }
        public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; }
    }
}
