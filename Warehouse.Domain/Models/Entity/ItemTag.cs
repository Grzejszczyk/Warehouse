using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse.Domain.Models.Entity
{
    public class ItemTag
    {
        public int ItemId { get; set; }
        public Item Item { get; set; }
        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
