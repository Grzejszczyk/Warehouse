using System;
using System.Collections.Generic;
using System.Text;
using Warehouse.Domain.Models.Common;

namespace Warehouse.Domain.Models.Entity
{
    public class ItemStructure
    {
        public string CreatedById { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string ModifiedById { get; set; }
        public DateTime ModifiedDateTime { get; set; }

        public int ItemId { get; set; }
        public Item Item { get; set; }
        public int ItemQuantity { get; set; }
        public int StructureId { get; set; }
        public Structure Structure { get; set; }
    }
}
