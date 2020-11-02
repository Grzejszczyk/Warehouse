using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse.Application.ViewModels.Item
{
    public class ItemDetailsVM
    {
        public int Id { get; set; }

        public int CreatedById { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int ModifiedById { get; set; }
        public DateTime ModifiedDateTime { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public int LowQuantityValue { get; set; }
        public int StructureId { get; set; } //Where Used Id
        public string StructureName { get; set; } //Where Used
        public string CategoryName { get; set; }
        public string SupplierName { get; set; }
        public int Quantity { get; set; }
    }
}
