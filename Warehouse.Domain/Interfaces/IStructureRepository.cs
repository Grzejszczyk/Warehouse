using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Warehouse.Domain.Models.Entity;

namespace Warehouse.Domain.Interfaces
{
    public interface IStructureRepository
    {
        int AddStructure(Structure structure, string userId);
        int UpdateStructure(Structure structure, int structureId, string userId);
        Structure GetStructure(int idStructure);
        IQueryable<Structure> GetStructures();
    }
}
