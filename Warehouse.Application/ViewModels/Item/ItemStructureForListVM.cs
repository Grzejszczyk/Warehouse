using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Warehouse.Application.Mapping;
using System.Linq;

namespace Warehouse.Application.ViewModels.Item
{
    public class ItemStructureForListVM
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int StructureId { get; set; }
        public string StructureName { get; set; }
        public string ProjectName { get; set; }
        public string ProductName { get; set; }
        public int ItemTotalQty { get; set; }
        public int ItemQtyForStructure { get; set; }
    }
}
