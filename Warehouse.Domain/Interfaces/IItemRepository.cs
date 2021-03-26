using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Warehouse.Domain.Models.Entity;

namespace Warehouse.Domain.Interfaces
{
    public interface IItemRepository
    {
        int AddItem(Item item, string userName);

        IQueryable<Item> GetItems();
        IQueryable<Item> GetItemsBySupplier(int supplierId);
        IQueryable<Item> GetItemsByStructure(int structureId);

        Item GetItem(int id);

        int EditItem(Item item, int id, string userName);
    }
}
