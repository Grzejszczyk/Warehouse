using System;
using System.Collections.Generic;
using System.Text;
using Warehouse.Application.ViewModels.Item;

namespace Warehouse.Application.Interfaces
{
    public interface IItemService
    {
        ItemsListVM GetAllItemsForList(int pageSize, int pageNo, string searchString);
        ItemsListVM GetItemsBySupplier(int supplierId, int pageSize, int pageNo, string searchString);
        ItemsStructuresListVM GetItemsByStructure(int structureId, int pageSize, int pageNo, string searchString);

        ItemDetailsVM GetItemDetails(int itemId);

        int AddItem(EditItemVM newItemVM, string userId);
        EditItemVM GetItemDetailsForEdit(int itemId);
        int EditItem(EditItemVM newItemVM, string userId);

        int SetIsDeleted(int itemId, string userId);

        int AssignItemToSupplier(int itemId, int supplierId, string userId);

        ItemsStructuresListVM GetItemStructuresForAssign(int itemId);
        int AssignItemToStructures(ItemsStructuresListVM itemsStructuresListVM, string userId);
    }
}
