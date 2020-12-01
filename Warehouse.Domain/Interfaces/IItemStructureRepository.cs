using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Warehouse.Domain.Models.Entity;

namespace Warehouse.Domain.Interfaces
{
    public interface IItemStructureRepository
    {
        int AddItemToManyStructures(List<ItemStructure> itemStructures, int itemId, string userId);
        IQueryable<Structure> GetAllStructures();
        IQueryable<ItemStructure> GetAllItemStructuresForItem(int itemId);
    }
}
