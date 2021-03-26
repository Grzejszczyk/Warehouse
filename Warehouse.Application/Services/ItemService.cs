using AutoMapper;
using AutoMapper.QueryableExtensions;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Warehouse.Application.Interfaces;
using Warehouse.Application.Mapping;
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
        private readonly IStructureRepository _structureRepository;
        private readonly IItemStructureRepository _itemStructureRepository;
        private ItemMapping itemMapping;

        public ItemService(IItemRepository itemRepo, ISupplierRepository supplierRepo, IItemStructureRepository itemStructureRepo, IStructureRepository structureRepository)
        {
            _itemRepository = itemRepo;
            _supplierRepository = supplierRepo;
            _structureRepository = structureRepository;
            _itemStructureRepository = itemStructureRepo;
            itemMapping = new ItemMapping();
        }


        public ItemsListVM GetAllItemsForList(int pageSize, int pageNo, string searchString)
        {
            var items = _itemRepository.GetItems()
                .Where(s => s.Name.Contains(searchString));

            var itemsListVM = itemMapping.MapItems(items);

            var itemsToShow = itemsListVM.Skip(pageSize * (pageNo - 1)).Take(pageSize);
            var itemsList = new ItemsListVM()
            {
                PaggingInfo = new PagingInfo() { CurrentPage = pageNo, ItemsPerPage = pageSize, TotalItems = items.Count() },
                Items = itemsToShow.ToList()
            };

            return itemsList;
        }
        public ItemsListVM GetItemsBySupplier(int supplierId, int pageSize, int pageNo, string searchString)
        {
            var items = _itemRepository.GetItemsBySupplier(supplierId)
                .Where(s => s.Name.Contains(searchString));

            var itemsListVM = itemMapping.MapItems(items);

            var itemsToShow = itemsListVM.Skip(pageSize * (pageNo - 1)).Take(pageSize);
            var itemsList = new ItemsListVM()
            {
                PaggingInfo = new PagingInfo() { CurrentPage = pageNo, ItemsPerPage = pageSize, TotalItems = items.Count() },
                Items = itemsToShow.ToList()
            };

            return itemsList;
        }
        public ItemsStructuresListVM GetItemsByStructure(int structureId, int pageSize, int pageNo, string searchString)
        {
            var itemsForStructure = _itemStructureRepository.GetAllStructures()
                .Where(s => s.StructureId == structureId);

            var itemsListVM = itemMapping.MapItemStructure(itemsForStructure);

            var itemsToShow = itemsListVM.Skip(pageSize * (pageNo - 1)).Take(pageSize);
            var itemsList = new ItemsStructuresListVM()
            {
                PaggingInfo = new PagingInfo() { CurrentPage = pageNo, ItemsPerPage = pageSize, TotalItems = itemsForStructure.Count() },
                ItemStructures = itemsToShow.ToList()
            };

            return itemsList;
        }

        public ItemDetailsVM GetItemDetails(int itemId)
        {
            var itemVM = itemMapping.MapItemDetails(_itemRepository.GetItem(itemId));
            return itemVM;
        }

        public int AddItem(EditItemVM ItemVM, string userName)
        {
            Item newItem = new Item();
            itemMapping.MapItemEntityFromVM(ItemVM, newItem);
            int itemId = _itemRepository.AddItem(newItem, userName);
            return itemId;
        }

        public EditItemVM GetItemDetailsForEdit(int itemId)
        {
            var item = _itemRepository.GetItem(itemId);
            var itemVM = new EditItemVM();
            itemVM = itemMapping.MapItemForEditVM(_itemRepository.GetItem(itemId));

            return itemVM;
        }

        public int EditItem(EditItemVM itemVM, string userName)
        {
            Item itemEntity = _itemRepository.GetItem(itemVM.Id);
            itemMapping.MapItemEntityFromVM(itemVM, itemEntity);
            itemEntity.IsDeleted = false;

            //Save Image and Thumbnail from IFileForm:
            itemMapping.SaveImageAsThumbnail(itemVM);

            var supplierMapped = _itemRepository.EditItem(itemEntity, itemVM.Id, userName);
            return supplierMapped;
        }

        public int AssignItemToSupplier(int itemId, int supplierId, string userName)
        {
            var item = _itemRepository.GetItem(itemId);
            var supplier = _supplierRepository.GetSupplierById(supplierId);
            item.Supplier = supplier;
            itemId = _itemRepository.EditItem(item, itemId, userName);
            return itemId;
        }

        public ItemsStructuresListVM GetItemStructuresForAssign(int itemId)
        {
            var itemStructures = _itemStructureRepository.GetAllStructures();
            var itemStructuresListVM = new ItemsStructuresListVM();
            itemStructuresListVM.ItemId = itemId;
            itemStructuresListVM.ItemStructures = itemMapping.MapItemStructure(itemStructures);
            var itemStustureQty = _itemStructureRepository.GetAllItemStructuresByItemId(itemId);

            foreach (var istr in itemStustureQty)
            {
                if (istr.ItemQuantity > 0)
                {
                    itemStructuresListVM.ItemStructures
                        .FirstOrDefault(x => x.StructureId == istr.StructureId && x.ItemId == istr.ItemId)
                        .ItemQtyForStructure = istr.ItemQuantity;
                }
            }
            return itemStructuresListVM;
        }

        public int AssignItemToStructures(ItemsStructuresListVM itemsStructuresListVM, string userName)
        {
            var itemStructures = new List<ItemStructure>();

            foreach (var i in itemsStructuresListVM.ItemStructures)
            {
                itemStructures.Add(new ItemStructure()
                {
                    ItemId = i.ItemId,
                    StructureId = i.StructureId,
                    ItemQuantity = i.ItemQtyForStructure
                });
            }

            var itemStructure = _itemStructureRepository.AddItemToStructures(itemStructures, itemsStructuresListVM.ItemId, userName);
            return itemStructure;
        }

        public int SetIsDeleted(int itemId, string userName)
        {
            Item itemEntity = _itemRepository.GetItem(itemId);
            itemEntity.IsDeleted = true;
            _itemRepository.EditItem(itemEntity, itemEntity.Id, userName);
            return itemEntity.Id;
        }
    }
}
