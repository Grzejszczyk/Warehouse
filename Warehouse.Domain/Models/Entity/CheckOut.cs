using System;
using System.Collections.Generic;
using System.Text;
using Warehouse.Domain.Models.Common;

namespace Warehouse.Domain.Models.Entity
{
    public class CheckOut : BaseEntity
    {
        public Item Item { get; set; }
        public int Quantity { get; set; }
    }
}
