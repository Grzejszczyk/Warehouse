using System;
using System.Collections.Generic;
using System.Text;
using Warehouse.Application.ViewModels.Structure;

namespace Warehouse.Application.ViewModels.Item
{
    public class ItemsStructuresListVM
    {
        public int ItemId { get; set; }
        public List<ItemStructureVM> ItemStructures { get; set; }
    }
}
