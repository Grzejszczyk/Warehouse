using System;
using System.Collections.Generic;
using System.Text;
using Warehouse.Application.ViewModels.Pagination;

namespace Warehouse.Application.ViewModels.Item
{
    public class ItemsListVM
    {
        public List<ItemForItemsListVM> Items { get; set; }
        public PagingInfo PaggingInfo { get; set; }
        public string SearchString { get; set; }
    }
}
