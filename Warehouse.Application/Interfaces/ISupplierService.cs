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
        int AddNewSupplier(SupplierDetailsVM newSupplierVM);
        int EditSupplier(SupplierDetailsVM newItemVM);
        void DeleteSupplier(int id);
    }
}
