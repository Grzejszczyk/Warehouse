using System;
using System.Collections.Generic;
using System.Text;
using Warehouse.Application.ViewModels.Pagination;
using Warehouse.Application.ViewModels.Structure;

namespace Warehouse.Application.ViewModels.Item
{
    public class ItemsStructuresListVM
    {
        public int ItemId { get; set; }
        public List<ItemStructureForListVM> ItemStructures { get; set; }
        public PagingInfo PaggingInfo { get; set; }
        public string SearchString { get; set; }
    }
}
