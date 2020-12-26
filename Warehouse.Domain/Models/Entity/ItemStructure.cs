using System;
using System.Collections.Generic;
using System.Text;
using Warehouse.Domain.Models.Common;

namespace Warehouse.Domain.Models.Entity
{
    public class ItemStructure : AuditableModelForEntity
    {
        public int ItemId { get; set; }
        public Item Item { get; set; }
        public int ItemQuantity { get; set; }
        public int StructureId { get; set; }
        public Structure Structure { get; set; }
    }
}
