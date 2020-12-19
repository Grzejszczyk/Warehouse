using System;
using System.Collections.Generic;
using System.Text;
using Warehouse.Domain.Models.Common;

namespace Warehouse.Domain.Models.Entity
{
    public class ItemImage : BaseEntity
    {
        public byte[] Image { get; set; }

        public int ItemId { get; set; }
        public Item Item { get; set; }
    }
}
