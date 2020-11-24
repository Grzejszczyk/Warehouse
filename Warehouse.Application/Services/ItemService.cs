using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Warehouse.Application.Interfaces;
using Warehouse.Application.ViewModels.Item;
using Warehouse.Application.ViewModels.Pagination;
using Warehouse.Domain.Interfaces;
using Warehouse.Domain.Models.Entity;

namespace Warehouse.Application.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly ISupplierRepository _supplierRepository;
        private readonly IStructureRepository _structureRepository;
        private readonly IMapper _mapper;
        public ItemService(IItemRepository itemRepo, ISupplierRepository supplierRepo, IStructureRepository structureRepo, IMapper mapper)
        {
            _itemRepository = itemRepo;
            _supplierRepository = supplierRepo;
            _structureRepository = structureRepo;
            _mapper = mapper;
        }

        public ItemsListForListVM GetAllItemsForList(int pageSize, int pageNo, string searchString)
        {
            var items = _itemRepository.GetItems().Where(s => s.Name.StartsWith(searchString)).ProjectTo<ItemForListVM>(_mapper.ConfigurationProvider).ToList();
            var itemsToShow = items.Skip(pageSize * (pageNo - 1)).Take(pageSize);
            var itemsList = new ItemsListForListVM()
            {
                PaggingInfo = new PagingInfo() { CurrentPage = pageNo, ItemsPerPage = pageSize, TotalItems = items.Count() },
                Items = itemsToShow.ToList()
            };

            return itemsList;
        }

        public ItemDetailsVM GetItemDetails(int itemId)
        {
            var item = _itemRepository.GetItemById(itemId);

            var itemVM = _mapper.Map<ItemDetailsVM>(item);

            itemVM.StructuresForItemDetails = new List<StructuresForItemDetails>();
            itemVM.CheckIns = new List<CheckInsOutsForItemDetails>();
            itemVM.CheckOuts = new List<CheckInsOutsForItemDetails>();

            foreach (var s in item.ItemStructures)
            {
                itemVM.StructuresForItemDetails.Add(
                    new StructuresForItemDetails
                    {
                        StructureId = s.StructureId,
                        StructureName = s.Structure.Name,
                        QuantityForStructure = s.ItemQuantity
                    });
            }
            foreach (var cin in item.CheckIns)
            {
                itemVM.CheckIns.Add(
                    new CheckInsOutsForItemDetails
                    {
                        CheckInOutId = cin.Id,
                        ActionDateTime = cin.ModifiedDateTime,
                        Quantity = cin.Quantity
                    });
            }
            foreach (var cou in item.CheckOuts)
            {
                itemVM.CheckOuts.Add(
                    new CheckInsOutsForItemDetails
                    {
                        CheckInOutId = cou.Id,
                        ActionDateTime = cou.ModifiedDateTime,
                        Quantity = cou.Quantity
                    });
            }
            return itemVM;
        }

        public EditItemVM GetItemDetailsForEdit(int itemId)
        {
            var item = _itemRepository.GetItemById(itemId);
            var itemVM = new EditItemVM();
            itemVM = _mapper.Map<EditItemVM>(item);
            itemVM.SupplierForForEditDetails = new List<SupplierForEditItem>();
            var suppliers = _supplierRepository.GetAllSuppliers();

            foreach (var s in suppliers)
            {
                itemVM.SupplierForForEditDetails.Add(new SupplierForEditItem() { SupplierId = s.Id, SupplierName = s.Name, SupplierNIP = s.NIP });
            }

            return itemVM;
        }

        public int AddItem(EditItemVM ItemVM, string userId)
        {
            Item newItem = new Item();

            newItem = _mapper.Map<Item>(ItemVM);

            var supplierMapped = _itemRepository.AddItem(newItem, userId);
            return supplierMapped;
        }

        public int EditItem(EditItemVM ItemVM, string userId)
        {
            Item itemEntity = _itemRepository.GetItemById(ItemVM.Id);

            itemEntity.LowQuantityValue = ItemVM.LowQuantityValue;
            itemEntity.Quantity = ItemVM.Quantity;
            itemEntity.Name = ItemVM.Name;
            itemEntity.Description = ItemVM.Description;
            itemEntity.IsDeleted = false;

            var supplierMapped = _itemRepository.UpdateItem(itemEntity, ItemVM.Id, userId);
            return supplierMapped;
        }

        //TODO: Implementation.
        //TODO: Put below methods ad Item Details.
        public int AssignItemToCategory(EditItemVM editItemVM)
        {
            //model: Caterories list,
            //assigned category (RadioButton)
            throw new NotImplementedException();
        }
        public int AssignItemToSupplier(EditItemVM editItemVM)
        {
            //model: Suppliers List, Create new Supplier,
            //assign supplier (new or existing)
            throw new NotImplementedException();
        }
        public int AssignItemToStructures(EditItemVM editItemVM)
        {
            //model: Structures List
            //assign to structures (List)
            throw new NotImplementedException();
        }

        public int SetIsDeleted(int itemId, string userId)
        {
            Item itemEntity = _itemRepository.GetItemById(itemId);
            itemEntity.IsDeleted = true;
            _itemRepository.UpdateItem(itemEntity, itemEntity.Id, userId);
            return itemEntity.Id;
        }

        //TODO: DeleteItem only for admin.
        //public void DeleteItem(int id)
        //{
        //    _itemRepository.DeleteItem(id);
        //}
    }
}
