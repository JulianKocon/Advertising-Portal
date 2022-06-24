using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AdvertisingPortal.DataAccess;

namespace AdvertisingPortal.Entities
{
    public class Advertisement
    {
        public Advertisement()
        {
            PurchaseOrders = new HashSet<PurchaseOrder>();
            Categories = new HashSet<Category>();
        }

        [Key]
        public int IdAdvertisement { get; set; }
        public int IdUser { get; set; }

        [ForeignKey("IdUser")]
        public User User { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(100)]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public int IdRegion { get; set; }

        [ForeignKey("IdRegion")]
        public Region Region { get; set; }
        public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; }
        public virtual ICollection<Category> Categories { get; set; }

    }
}