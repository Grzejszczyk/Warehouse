using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Warehouse.Application.Interfaces;
using Warehouse.Application.ViewModels.Pagination;
using Warehouse.Application.ViewModels.Supplier;
using Warehouse.Domain.Interfaces;
using Warehouse.Domain.Models.Entity;

namespace Warehouse.Application.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;
        public SupplierService(ISupplierRepository repo)
        {
            _supplierRepository = repo;
        }
        public int AddNewSupplier(NewSupplierVM newSupplierVM)
        {
            Supplier nS = new Supplier();

            nS.Name = newSupplierVM.Name;
            nS.NIP = newSupplierVM.NIP;
            nS.City = newSupplierVM.City;
            nS.ZipCode = newSupplierVM.ZipCode;
            nS.Street = newSupplierVM.Street;
            nS.BuildingNo = newSupplierVM.BuildingNo;
            nS.Email = newSupplierVM.Email;
            nS.PhoneNo = newSupplierVM.PhoneNo;
            nS.IsActive = newSupplierVM.IsActive;

            var s = _supplierRepository.AddSupplier(nS);
            return s;
        }

        public SuppliersListForListVM GetAllSuppliersForList(int pageSize, int pageNo, string searchString)
        {
            var suppliers = _supplierRepository.GetAllSuppliers().Where(s => s.Name.StartsWith(searchString));
            SuppliersListForListVM result = new SuppliersListForListVM();

            result.PaggingInfo = new PagingInfo();
            result.PaggingInfo.ItemsPerPage = pageSize;
            result.PaggingInfo.CurrentPage = pageNo;
            result.PaggingInfo.TotalItems = suppliers.Count();

            result.SearchString = searchString;

            var suppliersToShow = suppliers.Skip(pageSize * (pageNo - 1)).Take(pageSize);

            result.Suppliers = new List<SupplierForListVM>();
            foreach (var s in suppliersToShow)
            {
                var supplierVM = new SupplierForListVM()
                {
                    Id = s.Id,
                    Name = s.Name,
                    NIP = s.NIP,
                    Email = s.Email,
                    IsActive = s.IsActive
                };
                result.Suppliers.Add(supplierVM);
            }
            return result;
        }

        public SupplierDetailsVM GetSupplierDetails(int supplierId)
        {
            var supplier = _supplierRepository.GetSupplierById(supplierId);
            var supplierVM = new SupplierDetailsVM();

            supplierVM.Id = supplier.Id;
            supplierVM.Name = supplier.Name;
            supplierVM.NIP = supplier.NIP;
            supplierVM.City = supplier.City;
            supplierVM.ZipCode = supplier.ZipCode;
            supplierVM.Street = supplier.Street;
            supplierVM.BuildingNo = supplier.BuildingNo;
            supplierVM.Email = supplier.Email;
            supplierVM.PhoneNo = supplier.PhoneNo;
            supplierVM.IsActive = supplier.IsActive;

            return supplierVM;
        }
    }
}
