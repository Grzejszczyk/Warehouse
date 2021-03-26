using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Warehouse.Domain.Interfaces;
using Warehouse.Domain.Models.Entity;

namespace Warehouse.Infrastructure.Repositories
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly Context _context;
        public SupplierRepository(Context context)
        {
            _context = context;
        }

        public int AddSupplier(Supplier supplier, string userName)
        {
            //TODO: Remove fake user
            //var userId = _context.Users.FirstOrDefault(u => u.UserName == userName).Id;
            _context.Suppliers.Add(supplier);
            _context.SaveChanges("API Test User"/*userId*/);
            return supplier.Id;
        }
        public IQueryable<Supplier> GetAllSuppliers()
        {
            var suppliers = _context.Suppliers.Where(i => i.IsDeleted == false);
            return suppliers;
        }
        public Supplier GetSupplierById(int id)
        {
            var supplier = _context.Suppliers.FirstOrDefault(s => s.Id == id);
            return supplier;
        }
        public int EditSupplier(Supplier updatedSupplier, int supplierId, string userName)
        {
            //TODO: Remove fake user
            //var userId = _context.Users.FirstOrDefault(u => u.UserName == userName).Id;
            if (_context.Suppliers.Any(s => s.Id == supplierId))
            {
                _context.Suppliers.Update(updatedSupplier);
                _context.SaveChanges("API Test User"/*userId*/);
            }
            return supplierId;
        }
    }
}
