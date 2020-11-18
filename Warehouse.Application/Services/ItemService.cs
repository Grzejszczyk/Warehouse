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
        private readonly IMapper _mapper;
        public ItemService(IItemRepository repo, IMapper mapper)
        {
            _itemRepository = repo;
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
            //TODO: Assign to structure
            //TODO: Assign to supplier or add new supplier
            Item newItem = new Item();
            _mapper.Map<ItemDetailsVM, Item>(newItemVM, newItem);
            //TODO: Remove structure and category hacks!
            //////!!!!!!newItem.Structure = _itemRepository.GetItems().FirstOrDefault(s => s.Structure.Id == 1).Structure;
            newItem.Category = _itemRepository.GetItems().FirstOrDefault(c => c.Category.Id == 1).Category;
            var supplierMapped = _itemRepository.AddItem(newItem);
            return supplierMapped;
        }

        public int EditItem(ItemDetailsVM ItemVM)
        {
            //Assign to structure
            Item newItem = _itemRepository.GetItemById(ItemVM.Id);
            _mapper.Map<ItemDetailsVM, Item>(ItemVM, newItem);
            //TODO: Remove structure and category hacks!
            ///////!!!!!!newItem.Structure = _itemRepository.GetItems().FirstOrDefault(s => s.Structure.Id == 1).Structure;
            newItem.Category = _itemRepository.GetItems().FirstOrDefault(c => c.Category.Id == 1).Category;
            var supplierMapped = _itemRepository.UpdateItem(newItem, ItemVM.Id);
            return supplierMapped;
        }

        public void DeleteItem(int id)
        {
            _itemRepository.DeleteItem(id);
        }
    }
}
