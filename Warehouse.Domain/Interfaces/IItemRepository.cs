using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Warehouse.Domain.Models.Entity;

namespace Warehouse.Domain.Interfaces
{
    public interface IItemRepository
    {
        int AddItem(Item item);
        IQueryable<Item> GetItemsByStructure(int structureId);
        IQueryable<Item> GetItemsByCategory(int categoryId);
        IQueryable<Item> GetItems();
        Item GetItemById(int id);

        int UpdateItem(Item item, int id);
        void DeleteItem(int itemId);

        public Category GetCategoryById(int id);
    }
}
