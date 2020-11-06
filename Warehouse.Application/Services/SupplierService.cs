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
        public int AddNewSupplier(SupplierDetailsVM newSupplierVM)
        {
            //WITHOUT REVERSE MAPPING:

            //Supplier nS = new Supplier();

            //nS.Id = newSupplierVM.Id;
            //nS.Name = newSupplierVM.Name;
            //nS.NIP = newSupplierVM.NIP;
            //nS.City = newSupplierVM.City;
            //nS.ZipCode = newSupplierVM.ZipCode;
            //nS.Street = newSupplierVM.Street;
            //nS.BuildingNo = newSupplierVM.BuildingNo;
            //nS.Email = newSupplierVM.Email;
            //nS.PhoneNo = newSupplierVM.PhoneNo;
            //nS.IsActive = newSupplierVM.IsActive;

            //var s = _supplierRepository.AddSupplier(nS);
            //return s;

            //REVERCE MAPPING:

            var nSrevMapped = _mapper.Map<SupplierDetailsVM, Supplier>(newSupplierVM);
            var sMapped = _supplierRepository.AddSupplier(nSrevMapped);
            return sMapped;
        }

        public void DeleteSupplier(int id)
        {
            _supplierRepository.DeleteSupplier(id);
        }

        public int EditSupplier(SupplierDetailsVM newSupplierVM)
        {
            //WITHOUT REVERSE MAPPING:
            //Supplier newSupplier = _supplierRepository.GetSupplierById(newSupplierVM.Id);

            //newSupplier.Id = newSupplierVM.Id;
            //newSupplier.Name = newSupplierVM.Name;
            //newSupplier.NIP = newSupplierVM.NIP;
            //newSupplier.City = newSupplierVM.City;
            //newSupplier.ZipCode = newSupplierVM.ZipCode;
            //newSupplier.Street = newSupplierVM.Street;
            //newSupplier.BuildingNo = newSupplierVM.BuildingNo;
            //newSupplier.Email = newSupplierVM.Email;
            //newSupplier.PhoneNo = newSupplierVM.PhoneNo;
            //newSupplier.IsActive = newSupplierVM.IsActive;

            //int supplier = _supplierRepository.UpdateSupplier(newSupplier, newSupplierVM.Id);
            //return supplier;

            //REVERCE MAPPING:
            Supplier newSupplier = _mapper.Map<SupplierDetailsVM, Supplier>(newSupplierVM);
            var supplierMapped = _supplierRepository.UpdateSupplier(newSupplier, newSupplierVM.Id);
            return supplierMapped;
        }

        public SuppliersListForListVM GetAllSuppliersForList(int pageSize, int pageNo, string searchString)
        {
            var suppliers = _supplierRepository.GetAllSuppliers().Where(s => s.Name.StartsWith(searchString)).ProjectTo<SupplierForListVM>
                (_mapper.ConfigurationProvider).ToList();
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
    }
}
