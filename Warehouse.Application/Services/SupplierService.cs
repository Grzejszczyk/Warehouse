using AutoMapper;
using AutoMapper.QueryableExtensions;
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
        private readonly IMapper _mapper;
        public SupplierService(ISupplierRepository repo, IMapper mapper)
        {
            _supplierRepository = repo;
            _mapper = mapper;
        }
        public int AddSupplier(SupplierDetailsVM newSupplierVM, string userId)
        {
            var nSrevMapped = new Supplier();
            _mapper.Map<SupplierDetailsVM, Supplier>(newSupplierVM, nSrevMapped);
            var sMapped = _supplierRepository.AddSupplier(nSrevMapped, userId);
            return sMapped;
        }

        public int EditSupplier(SupplierDetailsVM newSupplierVM, string userId)
        {
            Supplier newSupplier = _supplierRepository.GetSupplierById(newSupplierVM.Id);
            _mapper.Map<SupplierDetailsVM, Supplier>(newSupplierVM, newSupplier);
            var supplierMapped = _supplierRepository.UpdateSupplier(newSupplier, newSupplierVM.Id, userId);
            return supplierMapped;
        }

        public SuppliersListForListVM GetAllSuppliersForList(int pageSize, int pageNo, string searchString)
        {
            var suppliers = _supplierRepository.GetAllSuppliers().Where(s => s.Name.StartsWith(searchString)).ProjectTo<SupplierForListVM>(_mapper.ConfigurationProvider).ToList();

            var suppliersToShow = suppliers.Skip(pageSize * (pageNo - 1)).Take(pageSize);
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
            var supplierVM = _mapper.Map<SupplierDetailsVM>(supplier);
            return supplierVM;
        }

        public int SetIsDeleted(int supplierId, string userId)
        {
            Supplier supplierEntity = _supplierRepository.GetSupplierById(supplierId);
            supplierEntity.IsDeleted = true;
            _supplierRepository.UpdateSupplier(supplierEntity, supplierEntity.Id, userId);
            return supplierEntity.Id;
        }
        public void DeleteSupplier(int id)
        {
            _supplierRepository.DeleteSupplier(id);
        }
    }
}
