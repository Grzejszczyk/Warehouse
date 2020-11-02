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
        //CRUD:
        public int AddSupplier(Supplier supplier)
        {
            _context.Suppliers.Add(supplier);
            _context.SaveChanges();
            return supplier.Id;
        }
        public IQueryable<Supplier> GetAllSuppliers()
        {
            var suppliers = _context.Suppliers.AsQueryable();
            return suppliers;
        }
        public Supplier GetSupplierById(int id)
        {
            var supplier = _context.Suppliers.FirstOrDefault(s => s.Id == id);
            return supplier;
        }
        public int UpdateSupplier(Supplier updatedSupplier, int supplierId)
        {
            //var supplier = _context.Suppliers.FirstOrDefault(s => s.Id == id);

            //supplier.IsActive = updatedSupplier.IsActive;

            //supplier.Name = updatedSupplier.Name;
            //supplier.NIP = updatedSupplier.NIP;
            //supplier.PhoneNo = updatedSupplier.PhoneNo;
            //supplier.Email = updatedSupplier.Email;

            //supplier.City = updatedSupplier.City;
            //supplier.Street = updatedSupplier.Street;
            //supplier.BuildingNo = updatedSupplier.BuildingNo;
            //supplier.ZipCode = updatedSupplier.ZipCode;

            var supplier = _context.Suppliers.Find(supplierId);
            if (supplier != null)
            {
                _context.Suppliers.Update(supplier);
                _context.SaveChanges();
            }
            _context.Update(updatedSupplier);
            _context.SaveChanges();

            return supplier.Id;
        }
        public void DeleteSupplier(int supplierId)
        {
            var supplier = _context.Suppliers.Find(supplierId);
            if (supplier != null)
            {
                _context.Suppliers.Remove(supplier);
                _context.SaveChanges();
            }
            //TODO: Supplier cannot be removed if items exist from this supplier!
        }
    }
}
