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

        Item GetItemById(int id);

        void UpdateItem();
        void DeleteItem(int itemId);
    }
}
