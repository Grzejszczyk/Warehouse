using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Warehouse.Domain.Models.Entity;

namespace Warehouse.Domain.Interfaces
{
    public interface IItemRepository
    {
        int AddItem(Item item, string userId);
        IQueryable<Item> GetItemsByStructure(int structureId);
        IQueryable<Item> GetItems();
        Item GetItemById(int id);

        int UpdateItem(Item item, int id, string userId);
        void DeleteItem(int itemId);
    }
}
