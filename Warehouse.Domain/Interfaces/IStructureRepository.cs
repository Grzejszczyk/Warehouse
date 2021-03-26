using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Warehouse.Domain.Models.Entity;

namespace Warehouse.Domain.Interfaces
{
    public interface IStructureRepository
    {
        IQueryable<Structure> GetStructures();
        IQueryable<Structure> GetStructuresByItemId(int itemId);
        Structure GetStructure(int idStructure);
        int AddStructure(Structure structure, string userName);
        int EditStructure(Structure structure, int structureId, string userName);

        int EditItemsStructure(int structureId, List<ItemStructure> itemStructure, string userName);
    }
}
