using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Warehouse.Application.Interfaces;
using Warehouse.Application.Mapping;
using Warehouse.Application.ViewModels.Pagination;
using Warehouse.Application.ViewModels.Supplier;
using Warehouse.Domain.Interfaces;
using Warehouse.Domain.Models.Entity;

namespace Warehouse.Application.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;
        private SupplierMapping supplierMapping;
        public SupplierService(ISupplierRepository repo, IMapper mapper)
        {
            _supplierRepository = repo;
            supplierMapping = new SupplierMapping();
        }

        public int AddSupplier(SupplierDetailsVM newSupplierVM, string userId)
        {
            Supplier supplier = new Supplier();
            supplierMapping.MapSupplierEntityFromVM(newSupplierVM, supplier);
            var sMapped = _supplierRepository.AddSupplier(supplier, userId);
            return sMapped;
        }

        public int EditSupplier(SupplierDetailsVM newSupplierVM, string userId)
        {
            Supplier supplier = _supplierRepository.GetSupplierById(newSupplierVM.Id);
            supplierMapping.MapSupplierEntityFromVM(newSupplierVM, supplier);
            var supplierMapped = _supplierRepository.EditSupplier(supplier, newSupplierVM.Id, userId);
            return supplierMapped;
        }

        public SuppliersListForListVM GetSuppliers(int pageSize, int pageNo, string searchString)
        {
            var suppliers = _supplierRepository.GetAllSuppliers()
                .Where(s => s.IsActive == true)
                .Where(s => s.IsDeleted == false)
                .Where(s => s.Name.StartsWith(searchString))
                .Skip(pageSize * (pageNo - 1)).Take(pageSize);

            var suppliersToShow = supplierMapping.MapSuppliersToListVM(suppliers);

            var suppliersList = new SuppliersListForListVM()
            {
                PaggingInfo = new PagingInfo() { CurrentPage = pageNo, ItemsPerPage = pageSize, TotalItems = suppliers.Count() },
                Suppliers = suppliersToShow.ToList()
            };

            return suppliersList;
        }

        public SupplierDetailsVM GetSupplierDetails(int supplierId)
        {
            var supplier = _supplierRepository.GetSupplierById(supplierId);
            var supplierVM = supplierMapping.MapSupplierVM(supplier);
            return supplierVM;
        }

        public int SetIsDeleted(int supplierId, string userId)
        {
            Supplier supplierEntity = _supplierRepository.GetSupplierById(supplierId);
            supplierEntity.IsDeleted = true;
            _supplierRepository.EditSupplier(supplierEntity, supplierEntity.Id, userId);
            return supplierEntity.Id;
        }
    }
}
