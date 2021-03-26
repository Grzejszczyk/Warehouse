using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Warehouse.Domain.Models.Entity;

namespace Warehouse.Domain.Interfaces
{
    public interface IItemStructureRepository
    {
        int AddItemToStructures(List<ItemStructure> itemStructures, int itemId, string userId);
        IQueryable<ItemStructure> GetAllStructures();
        IQueryable<ItemStructure> GetAllItemStructuresByItemId(int itemId);
    }
}
