using System;
using System.Collections.Generic;
using System.Text;
using Warehouse.Application.ViewModels.Pagination;

namespace Warehouse.Application.ViewModels.Supplier
{
    public class SuppliersListForListVM
    {
        public List<SupplierForListVM> Suppliers { get; set; }
        public PagingInfo PaggingInfo { get; set; }
        public string SearchString { get; set; }
    }
}
