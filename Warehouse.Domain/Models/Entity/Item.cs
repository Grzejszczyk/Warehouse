using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warehouse.Domain.Models.Common;

namespace Warehouse.Domain.Models.Entity
{
    public class Item : AuditableModelForEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string CatalogNumber { get; set; }
        public string DrawingNumber { get; set; }
        public int Quantity { get; set; }
        public int LowQuantityValue { get; set; }
        public byte[] Thumbnail { get; set; }
        public bool IsDeleted { get; set; }

        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }

        public ICollection<ItemStructure> ItemStructures { get; set; }
        public ICollection<CheckIn> CheckIns { get; set; }
        public ICollection<CheckOut> CheckOuts { get; set; }
    }
}
