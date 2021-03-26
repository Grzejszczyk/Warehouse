using System;
using System.Collections.Generic;
using System.Text;
using Warehouse.Application.ViewModels.Supplier;

namespace Warehouse.Application.Interfaces
{
    public interface ISupplierService
    {
        SuppliersListForListVM GetSuppliers(int pageSize, int pageNo, string searchString);
        SupplierDetailsVM GetSupplierDetails(int supplierId);
        int AddSupplier(SupplierDetailsVM newSupplierVM, string userId);
        int EditSupplier(SupplierDetailsVM newItemVM, string userId);
        int SetIsDeleted(int supplierId, string userId);
    }
}
