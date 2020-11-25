using System;
using System.Collections.Generic;
using System.Text;
using Warehouse.Application.ViewModels.Item;

namespace Warehouse.Application.Interfaces
{
    public interface IItemService
    {
        ItemsListForListVM GetAllItemsForList(int pageSize, int pageNo, string searchString);
        ItemDetailsVM GetItemDetails(int itemId);
        int AddItem(EditItemVM newItemVM, string userId);
        int EditItem(EditItemVM newItemVM, string userId);
        EditItemVM GetItemDetailsForEdit(int itemId);
        ItemToSupplierVM GetItemForSuppliersList(int itemId);
        public int AssignItemToSupplier(int itemId, int supplierId, string userId);
        int AssignItemToStructures(EditItemVM editItemVM);

        int SetIsDeleted(int itemId, string userId);
        //void DeleteItem(int id); //This will be for admin.
    }
}
