using System;
using System.Collections.Generic;
using System.Text;
using Warehouse.Application.ViewModels.Supplier;

namespace Warehouse.Application.Interfaces
{
    public interface ISupplierService
    {
        SuppliersListForListVM GetAllSuppliersForList(int pageSize, int pageNo, string searchString);
        SupplierDetailsVM GetSupplierDetails(int supplierId);
        int AddSupplier(SupplierDetailsVM newSupplierVM);
        int EditSupplier(SupplierDetailsVM newItemVM);
        int SetIsDeleted(int supplierId);
        void DeleteSupplier(int supplierid);
    }
}
