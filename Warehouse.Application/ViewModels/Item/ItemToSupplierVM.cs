using System;
using System.Collections.Generic;
using System.Text;
using Warehouse.Application.ViewModels.Supplier;

namespace Warehouse.Application.ViewModels.Item
{
    public class ItemToSupplierVM
    {
        public ItemDetailsVM ItemDetails { get; set; }
        public List<SupplierForListVM> SuppliersList { get; set; }
    }
}
