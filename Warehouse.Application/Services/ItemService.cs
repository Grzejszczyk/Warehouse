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

        public int AddItem(ItemDetailsVM newItemVM)
        {
            Item newItem = new Item();
            //TODO: Automapper Issue. Manual mapping...
            //newItem = _mapper.Map<Item>(newItemVM);

            //hacks area:
            //TODO: Continue implements and remove hacks
            newItemVM.CategoryId = 1;
            newItemVM.StructuresForItemDetails = new List<StructuresForItemDetails>();
            newItemVM.StructuresForItemDetails.Add(new StructuresForItemDetails() { StructureId = 1, QuantityForStructure = 99 });
            newItemVM.SupplierId = 1;
            //end

            newItem.Category = _itemRepository.GetCategoryById(newItemVM.CategoryId);
            newItem.Description = newItemVM.Description;
            newItem.IsDeleted = false;

            newItem.ItemStructures = new List<ItemStructure>();
            foreach (var structure in newItemVM.StructuresForItemDetails)
            {
                ItemStructure itemStructure = new ItemStructure();
                itemStructure.Item = newItem;
                itemStructure.Structure = _structureRepository.GetStructure(structure.StructureId);
                itemStructure.ItemQuantity = structure.QuantityForStructure;
                newItem.ItemStructures.Add(itemStructure);
            }

            newItem.LowQuantityValue = newItemVM.LowQuantityValue;
            newItem.Name = newItemVM.Name;
            newItem.Quantity = newItemVM.Quantity;
            newItem.Supplier = _supplierRepository.GetSupplierById(newItemVM.SupplierId);
            newItem.Id = 0;
            var supplierMapped = _itemRepository.AddItem(newItem);
            return supplierMapped;
        }

        public int EditItem(ItemDetailsVM ItemVM)
        {
            //Assign to structure
            Item itemEntity = _itemRepository.GetItemById(ItemVM.Id);
            //TODO: Automapper Issue. Manual mapping...
            //_mapper.Map<ItemDetailsVM, Item>(ItemVM, itemEntity);

            //hacks area:
            //TODO: Continue implements and remove hacks
            ItemVM.CategoryId = 1;
            ItemVM.StructuresForItemDetails = new List<StructuresForItemDetails>();
            ItemVM.StructuresForItemDetails.Add(new StructuresForItemDetails() { StructureId = 1, QuantityForStructure = 99 });
            ItemVM.SupplierId = 1;
            //end

            itemEntity.Category = _itemRepository.GetCategoryById(ItemVM.CategoryId);
            itemEntity.Description = ItemVM.Description;
            itemEntity.IsDeleted = false;

            itemEntity.ItemStructures = new List<ItemStructure>();
            foreach (var structure in ItemVM.StructuresForItemDetails)
            {
                ItemStructure itemStructure = new ItemStructure();
                itemStructure.Item = itemEntity;
                itemStructure.Structure = _structureRepository.GetStructure(structure.StructureId);
                itemStructure.ItemQuantity = structure.QuantityForStructure;
                itemEntity.ItemStructures.Add(itemStructure);
            }

            itemEntity.LowQuantityValue = ItemVM.LowQuantityValue;
            itemEntity.Name = ItemVM.Name;
            itemEntity.Quantity = ItemVM.Quantity;
            itemEntity.Supplier = _supplierRepository.GetSupplierById(ItemVM.SupplierId);

            var supplierMapped = _itemRepository.UpdateItem(itemEntity, ItemVM.Id);
            return supplierMapped;
        }

        public int SetIsDeleted(int itemId)
        {
            Item itemEntity = _itemRepository.GetItemById(itemId);
            itemEntity.IsDeleted = true;
            _itemRepository.UpdateItem(itemEntity, itemEntity.Id);
            return itemEntity.Id;
        }

        public void DeleteItem(int id)
        {
            _itemRepository.DeleteItem(id);
        }
    }
}
