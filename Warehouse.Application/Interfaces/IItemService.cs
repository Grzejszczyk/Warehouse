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
        int AddNewItem(NewItemVM newItemVM);
        int EditItem(NewItemVM newItemVM);
        public NewItemVM GetItemForEdit(int id);
        void DeleteItem(int id);
    }
}
