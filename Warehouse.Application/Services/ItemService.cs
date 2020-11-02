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
        public ItemService(IItemRepository repo)
        {
            _itemRepository = repo;
        }

        public ItemsListForListVM GetAllItemsForList(int pageSize, int pageNo, string searchString)
        {
            var items = _itemRepository.GetItems().Where(s => s.Name.StartsWith(searchString));

            ItemsListForListVM result = new ItemsListForListVM();
            result.PaggingInfo = new PagingInfo();

            result.PaggingInfo.ItemsPerPage = pageSize;
            result.PaggingInfo.CurrentPage = pageNo;
            result.PaggingInfo.TotalItems = items.Count();
            result.SearchString = searchString;
            var itemsToShow = items.Skip(pageSize * (pageNo - 1)).Take(pageSize);

            result.Items = new List<ItemForListVM>();
            foreach (var item in itemsToShow)
            {
                var itemVM = new ItemForListVM()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Category = item.Category.CategoryName,
                    Quantity = item.Quantity
                };
                result.Items.Add(itemVM);
            }
            return result;
        }

        public ItemDetailsVM GetItemDetails(int itemId)
        {
            var item = _itemRepository.GetItemById(itemId);
            var result = new ItemDetailsVM();

            result.Id = item.Id;

            result.CreatedById = item.CreatedById;
            result.CreatedDateTime = item.CreatedDateTime;
            result.ModifiedById = (int)item.ModifiedById;
            result.ModifiedDateTime = (DateTime)item.ModifiedDateTime;

            result.Name = item.Name;
            result.Description = item.Description;
            result.LowQuantityValue = result.LowQuantityValue;
            result.StructureId = item.Structure.Id;
            result.StructureName = item.Structure.Subassembly;
            result.CategoryName = item.Category.CategoryName;
            result.SupplierName = item.Supplier.Name;
            result.Quantity = item.Quantity;

            return result;
        }

        public int AddNewItem(NewItemVM newItemVM)
        {
            Item newItem = new Item();

            newItem.Name = newItemVM.Name;
            newItem.Description = newItemVM.Description;
            newItem.LowQuantityValue = newItemVM.LowQuantityValue;
            newItem.Quantity = newItemVM.Quantity;
            newItem.Structure = _itemRepository.GetItems().FirstOrDefault(s => s.Structure.Id == 1).Structure;
            newItem.Category = _itemRepository.GetItems().FirstOrDefault(c => c.Category.Id == 1).Category;

            int item = _itemRepository.AddItem(newItem);
            return item;
        }

        public NewItemVM GetItemForEdit(int id)
        {
            Item editItem = _itemRepository.GetItemById(id);
            var item = new NewItemVM();

            item.Id = editItem.Id;
            item.CategoryId = editItem.Category.Id;
            item.Description = editItem.Description;
            item.LowQuantityValue = editItem.LowQuantityValue;
            item.Name = editItem.Name;
            item.Quantity = editItem.Quantity;
            item.StructureId = editItem.Structure.Id;
            
            return item;
        }

        public int EditItem(NewItemVM newItemVM)
        {
            Item newItem = _itemRepository.GetItemById(newItemVM.Id);

            newItem.Name = newItemVM.Name;
            newItem.Description = newItemVM.Description;
            newItem.LowQuantityValue = newItemVM.LowQuantityValue;
            newItem.Quantity = newItemVM.Quantity;
            newItem.Structure = _itemRepository.GetItems().FirstOrDefault(s => s.Structure.Id == 1).Structure;
            newItem.Category = _itemRepository.GetItems().FirstOrDefault(c => c.Category.Id == 1).Category;

            int item = _itemRepository.UpdateItem(newItem, newItemVM.Id);
            return item;
        }

        public void DeleteItem(int id)
        {
            _itemRepository.DeleteItem(id);
        }
    }
}
