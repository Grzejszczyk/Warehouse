﻿using System;
using System.Collections.Generic;
using System.Text;
using Warehouse.Application.ViewModels.Item;

namespace Warehouse.Application.Interfaces
{
    public interface IItemService
    {
        ItemsListForListVM GetAllItemsForList(int pageSize, int pageNo, string searchString);
        ItemDetailsVM GetItemDetails(int itemId);
        int AddItem(ItemDetailsVM newItemVM);
        int EditItem(ItemDetailsVM newItemVM);
        int SetIsDeleted(int itemId);
        void DeleteItem(int id);
    }
}
