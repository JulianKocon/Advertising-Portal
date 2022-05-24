using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AdvertisingPortal.Entities;

namespace AdvertisingPortal.DataAccess
{
    public class PurchaseOrder
    {
        public int IdUser { get; set; }
        [ForeignKey("IdUser")]
        public User User { get; set; }
        public int IdAdvertisement { get; set; }
        [ForeignKey("IdAdvertisement")]
        public Advertisement Advertisement { get; set; }
        public DateTime OrderDate { get; set; }

    }
}