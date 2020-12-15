using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Warehouse.Application.Interfaces;
using Warehouse.Application.ViewModels.Item;
using Warehouse.Application.ViewModels.Pagination;
using Warehouse.Application.ViewModels.Structure;
using Warehouse.Application.ViewModels.Supplier;
using Warehouse.Domain.Interfaces;
using Warehouse.Domain.Models.Entity;

namespace Warehouse.Application.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly ISupplierRepository _supplierRepository;
        private readonly IItemStructureRepository _itemStructureRepository;
        private readonly IMapper _mapper;
        public ItemService(IItemRepository itemRepo, ISupplierRepository supplierRepo, IItemStructureRepository itemStructureRepo, IMapper mapper)
        {
            _itemRepository = itemRepo;
            _supplierRepository = supplierRepo;
            _itemStructureRepository = itemStructureRepo;
            _mapper = mapper;
        }

        public ItemsListForListVM GetAllItemsForList(int pageSize, int pageNo, string searchString)
        {
            var items = _itemRepository.GetItems()
                .Where(s => s.Name.Contains(searchString))
                .ProjectTo<ItemForListVM>(_mapper.ConfigurationProvider).ToList();

            var itemsToShow = items.Skip(pageSize * (pageNo - 1)).Take(pageSize);
            var itemsList = new ItemsListForListVM()
            {
                PaggingInfo = new PagingInfo() { CurrentPage = pageNo, ItemsPerPage = pageSize, TotalItems = items.Count() },
                Items = itemsToShow.ToList()
            };

            return itemsList;
        }
        public ItemsListForListVM GetItemsBySupplier(int supplierId, int pageSize, int pageNo, string searchString)
        {
            var items = _itemRepository.GetItemsBySupplier(supplierId)
                .Where(s => s.Name.Contains(searchString))
                .ProjectTo<ItemForListVM>(_mapper.ConfigurationProvider).ToList();

            var itemsToShow = items.Skip(pageSize * (pageNo - 1)).Take(pageSize);
            var itemsList = new ItemsListForListVM()
            {
                PaggingInfo = new PagingInfo() { CurrentPage = pageNo, ItemsPerPage = pageSize, TotalItems = items.Count() },
                Items = itemsToShow.ToList()
            };

            return itemsList;
        }
        public ItemsListForListVM GetItemsByStructure(int structureId, int pageSize, int pageNo, string searchString)
        {
            var items = _itemRepository.GetItemsByStructure(structureId)
                .Where(s => s.Name.Contains(searchString))
                .ProjectTo<ItemForListVM>(_mapper.ConfigurationProvider).ToList();

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
            itemVM.CheckIns = new List<CheckInsForItemDetails>();
            itemVM.CheckOuts = new List<CheckOutsForItemDetails>();

            foreach (var s in item.ItemStructures)
            {
                itemVM.StructuresForItemDetails.Add(_mapper.Map<StructuresForItemDetails>(s));
            }
            foreach (var cin in item.CheckIns)
            {
                itemVM.CheckIns.Add(_mapper.Map<CheckInsForItemDetails>(cin));
            }
            foreach (var cou in item.CheckOuts)
            {
                itemVM.CheckOuts.Add(_mapper.Map<CheckOutsForItemDetails>(cou));
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

        public EditItemVM GetItemDetailsForEdit(int itemId)
        {
            var item = _itemRepository.GetItemById(itemId);
            var itemVM = new EditItemVM();
            itemVM = _mapper.Map<EditItemVM>(item);

            return itemVM;
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

        public ItemToSupplierVM GetItemForSuppliersList(int itemId)
        {
            var suppliers = _supplierRepository.GetAllSuppliers().Where(s=>s.IsActive==true)
                .ProjectTo<SupplierForListVM>(_mapper.ConfigurationProvider).ToList();

            var assignItemToSupplier = new ItemToSupplierVM();
            assignItemToSupplier.SuppliersList = suppliers;

            var item = _itemRepository.GetItemById(itemId);
            assignItemToSupplier.ItemDetails = _mapper.Map<ItemDetailsVM>(item);

            return assignItemToSupplier;
        }

        public int AssignItemToSupplier(int itemId, int supplierId, string userId)
        {
            var item = _itemRepository.GetItemById(itemId);
            var supplier = _supplierRepository.GetSupplierById(supplierId);
            item.Supplier = supplier;
            itemId = _itemRepository.UpdateItem(item, itemId, userId);
            return itemId;
        }

        public ItemsStructuresListVM GetItemStructuresForAssign(int itemId)
        {
            var itemStructuresListVM = new ItemsStructuresListVM();
            itemStructuresListVM.ItemId = itemId;
            itemStructuresListVM.ItemStructures = new List<ItemStructureVM>();

            var allStructures = _itemStructureRepository.GetAllStructures();
            var itemStustureForItem = _itemStructureRepository.GetAllItemStructuresForItem(itemId);

            foreach (var s in allStructures)
            {
                itemStructuresListVM.ItemStructures.Add(new ItemStructureVM()
                {
                    StructureId = s.Id,
                    StructureName = s.Name,
                    ProductName = s.ProductName,
                    ProjectName = s.Project,
                    ItemId = itemId
                });
            };
            foreach (var istr in itemStustureForItem)
            {
                if (istr.ItemQuantity > 0)
                {
                    itemStructuresListVM.ItemStructures
                        .FirstOrDefault(x => x.StructureId == istr.StructureId && x.ItemId == istr.ItemId)
                        .ItemQty = istr.ItemQuantity;
                }
            }
            return itemStructuresListVM;
        }
        public int AssignItemToStructures(ItemsStructuresListVM itemsStructuresListVM, string userId)
        {
            var itemStructures = new List<ItemStructure>();

            foreach (var i in itemsStructuresListVM.ItemStructures)
            {
                itemStructures.Add(new ItemStructure() { ItemId = i.ItemId, StructureId = i.StructureId, ItemQuantity = i.ItemQty });
            }

            var itemStructure = _itemStructureRepository.AddItemToManyStructures(itemStructures, itemsStructuresListVM.ItemId, userId);
            return itemStructure;
        }

        public int SetIsDeleted(int itemId, string userId)
        {
            Item itemEntity = _itemRepository.GetItemById(itemId);
            itemEntity.IsDeleted = true;
            _itemRepository.UpdateItem(itemEntity, itemEntity.Id, userId);
            return itemEntity.Id;
        }
    }
}
