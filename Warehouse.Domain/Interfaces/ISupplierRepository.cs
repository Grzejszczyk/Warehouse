using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Warehouse.Domain.Models.Entity;

namespace Warehouse.Domain.Interfaces
{
    public interface ISupplierRepository
    {
        public int AddSupplier(Supplier supplier, string userName);
        public IQueryable<Supplier> GetAllSuppliers();
        public Supplier GetSupplierById(int id);
        public int EditSupplier(Supplier updatedSupplier, int supplierId, string userName);
    }
}
