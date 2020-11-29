﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Warehouse.Domain.Models.Entity;

namespace Warehouse.Domain.Interfaces
{
    public interface IItemStructureRepository
    {
        int AddItemToStructure(int itemId, int structureId, int itemsQty, string userId);
        int RemoveItemFromStructure(int itemId, int structureId, string userId);
        //int AddManyItemsToStructure(int[] itemIds, int structureId, int itemsQty, string userId);
        int AddItemToManyStructures(List<ItemStructure> itemStructures, string userId);
        IQueryable<ItemStructure> GetStructuresForItem(int itemId);
        IQueryable<Structure> GetAllStructures();
        IQueryable<ItemStructure> GetAllItemStructuresForItem(int itemId);
    }
}