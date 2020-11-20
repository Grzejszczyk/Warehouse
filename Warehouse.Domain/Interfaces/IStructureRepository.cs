using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Warehouse.Domain.Models.Entity;

namespace Warehouse.Domain.Interfaces
{
    public interface IStructureRepository
    {
        int AddStructure(Structure structure);
        int UpdateStructure(Structure structure, int structureId);
        void DeleteStructure(int structureId);
        Structure GetStructure(int idStructure);
        IQueryable<Structure> GetStructures();
    }
}
