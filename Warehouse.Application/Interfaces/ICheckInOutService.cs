using System;
using System.Collections.Generic;
using System.Text;
using Warehouse.Application.ViewModels.CheckInOut;
using Warehouse.Application.ViewModels.Item;
using Warehouse.Application.ViewModels.Structure;
using Warehouse.Domain.Models.Entity;

namespace Warehouse.Application.Interfaces
{
    public interface ICheckInOutService
    {
        int CheckIn(int itemId, int itemQty, string userId);
        int CheckOut(int itemId, int itemQty, string userId);
        int CheckOutByStructure(int structureId, string userId);

        ItemsListForCheckInOutListVM GetAllItemsForCheckInOutList(int pageSize, int pageNo, string searchString);
        StructuresListForCheckOutVM GetStructures(int pageSize, int pageNo, string searchString);
        ItemsListForCheckInOutListVM GetItemsFromStructureForCheckInOutList(int structureId);
    }
}
