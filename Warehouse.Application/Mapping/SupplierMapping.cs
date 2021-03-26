using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Warehouse.Application.ViewModels.Supplier;
using Warehouse.Domain.Interfaces;
using Warehouse.Domain.Models.Entity;

namespace Warehouse.Application.Mapping
{
    internal class SupplierMapping
    {
        internal List<SupplierForListVM> MapSuppliersToListVM(IQueryable<Supplier> supplier)
        {
            return supplier.Select(supplierElement => new SupplierForListVM
            {
                Id = supplierElement.Id,
                Name = supplierElement.Name,
                NIP = supplierElement.NIP,
                Email = supplierElement.Email,
                IsActive = supplierElement.IsActive
            }).ToList();
        }
        internal void MapSupplierEntityFromVM(SupplierDetailsVM supplierDetailsVM, Supplier supplier)
        {
            supplier.Name = supplierDetailsVM.Name;
            supplier.NIP = supplierDetailsVM.NIP;
            supplier.Email = supplierDetailsVM.Email;
            supplier.PhoneNo = supplierDetailsVM.PhoneNo;
            supplier.City = supplierDetailsVM.City;
            supplier.ZipCode = supplierDetailsVM.ZipCode;
            supplier.Street = supplierDetailsVM.Street;
            supplier.BuildingNo = supplierDetailsVM.BuildingNo;
            supplier.IsActive = supplierDetailsVM.IsActive;
            supplier.ContactPersonName = supplierDetailsVM.ContactPersonName;
            supplier.ContactPersonSurname = supplierDetailsVM.ContactPersonSurname;
            supplier.ContactPersonEmail = supplierDetailsVM.ContactPersonEmail;
            supplier.ContactPersonPhoneNo = supplierDetailsVM.ContactPersonPhoneNo;
            supplier.Id = supplierDetailsVM.Id;
        }

        internal SupplierDetailsVM MapSupplierVM(Supplier supplier)
        {
            return new SupplierDetailsVM
            {
                Id = supplier.Id,
                CreatedById = supplier.CreatedById,
                CreatedDateTime = supplier.CreatedDateTime,
                ModifiedById = supplier.ModifiedById,
                ModifiedDateTime = supplier.ModifiedDateTime,
                Name = supplier.Name,
                NIP = supplier.NIP,
                Email = supplier.Email,
                PhoneNo = supplier.PhoneNo,
                City = supplier.City,
                ZipCode = supplier.ZipCode,
                Street = supplier.Street,
                BuildingNo = supplier.BuildingNo,
                IsActive = supplier.IsActive,
                ContactPersonName = supplier.ContactPersonName,
                ContactPersonSurname = supplier.ContactPersonSurname,
                ContactPersonEmail = supplier.ContactPersonEmail,
                ContactPersonPhoneNo = supplier.ContactPersonPhoneNo
            };
        }

    }
}
