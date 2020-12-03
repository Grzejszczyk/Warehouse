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

        IQueryable<Item> GetItems();
        IQueryable<Item> GetItemsBySupplier(int supplierId);
        IQueryable<Item> GetItemsByStructure(int structureId);

        Item GetItemById(int id);

        int UpdateItem(Item item, int id, string userId);
    }
}
