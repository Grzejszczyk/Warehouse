﻿using System;
using System.Collections.Generic;
using System.Text;
using Warehouse.Application.ViewModels.Supplier;

namespace Warehouse.Application.Interfaces
{
    public interface ISupplierService
    {
        SuppliersListForListVM GetAllSuppliersForList(int pageSize, int pageNo, string searchString);
        SupplierDetailsVM GetSupplierDetails(int supplierId);
        int AddNewSupplier(NewSupplierVM newSupplierVM);
        int EditSupplier(NewSupplierVM newItemVM);
        public NewSupplierVM GetSupplierForEdit(int id);
        void DeleteSupplier(int id);
    }
}
