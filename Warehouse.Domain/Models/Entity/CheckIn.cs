using System;
using System.Collections.Generic;
using System.Text;
using Warehouse.Domain.Models.Common;

namespace Warehouse.Domain.Models.Entity
{
    public class CheckIn : AuditableModelForEntity
    {
        public Item Item { get; set; }
        public int Quantity { get; set; }
        public bool IsCheckInFromShopStore { get; set; }
    }
}
