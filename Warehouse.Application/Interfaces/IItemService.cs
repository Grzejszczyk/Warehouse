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
        int AddNewItem(ItemDetailsVM newItemVM);
        int EditItem(ItemDetailsVM newItemVM);
        void DeleteItem(int id);
    }
}
