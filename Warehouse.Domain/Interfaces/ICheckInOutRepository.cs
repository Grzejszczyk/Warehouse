using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Warehouse.Domain.Models.Entity;

namespace Warehouse.Domain.Interfaces
{
    public interface ICheckInOutRepository
    {
        int CheckInItem(int idemId, int itemQty, string userId);
        int CheckOutItem(int idemId, int itemQty, string userId);
        int CheckOutByStructure(int structureId, string userId);
        IQueryable<Structure> GetStructures();
        IQueryable<Item> GetItems();
        IQueryable<ItemStructure> GetItemsByStructure(int structureId);
    }
}
