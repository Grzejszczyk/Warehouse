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
            return itemVM;
        }

        public int AddNewItem(ItemDetailsVM newItemVM)
        {
            //WITHOUT REVERSE MAPPING:
            //Item newItem = new Item();

            //newItem.Name = newItemVM.Name;
            //newItem.Description = newItemVM.Description;
            //newItem.LowQuantityValue = newItemVM.LowQuantityValue;
            //newItem.Quantity = newItemVM.Quantity;
            //newItem.Structure = _itemRepository.GetItems().FirstOrDefault(s => s.Structure.Id == 1).Structure;
            //newItem.Category = _itemRepository.GetItems().FirstOrDefault(c => c.Category.Id == 1).Category;

            //int item = _itemRepository.AddItem(newItem);
            //return item;

            //REVERCE MAPPING: (issue solved)
            Item newItem = new Item();
            _mapper.Map<ItemDetailsVM, Item>(newItemVM, newItem);
            //TODO: Remove structure and category hacks!
            newItem.Structure = _itemRepository.GetItems().FirstOrDefault(s => s.Structure.Id == 1).Structure;
            newItem.Category = _itemRepository.GetItems().FirstOrDefault(c => c.Category.Id == 1).Category;
            var supplierMapped = _itemRepository.AddItem(newItem);
            return supplierMapped;
        }

        public int EditItem(ItemDetailsVM ItemVM)
        {
            //WITHOUT REVERSE MAPPING:
            //Item newItem = _itemRepository.GetItemById(ItemVM.Id);

            //newItem.Name = ItemVM.Name;
            //newItem.Description = ItemVM.Description;
            //newItem.LowQuantityValue = ItemVM.LowQuantityValue;
            //newItem.Quantity = ItemVM.Quantity;
            //newItem.Structure = _itemRepository.GetItems().FirstOrDefault(s => s.Structure.Id == 1).Structure;
            //newItem.Category = _itemRepository.GetItems().FirstOrDefault(c => c.Category.Id == 1).Category;

            //int item = _itemRepository.UpdateItem(newItem, ItemVM.Id);
            //return item;

            //REVERCE MAPPING: (issue solved)
            Item newItem = _itemRepository.GetItemById(ItemVM.Id);
            _mapper.Map<ItemDetailsVM, Item>(ItemVM, newItem);
            //TODO: Remove structure and category hacks!
            newItem.Structure = _itemRepository.GetItems().FirstOrDefault(s => s.Structure.Id == 1).Structure;
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
