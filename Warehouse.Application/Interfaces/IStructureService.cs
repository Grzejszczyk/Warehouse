using System;
using System.Collections.Generic;
using System.Text;
using Warehouse.Application.ViewModels.Structure;

namespace Warehouse.Application.Interfaces
{
    public interface IStructureService
    {
        StructuresListForListVM StructuresList(int pageSize, int pageNo, string searchString);
        StructureDetailsVM GetStructureDetails(int structureId);
        int AddStructure(StructureDetailsVM newStructureVM);
        int EditStructure(StructureDetailsVM newStructureVM);
        int SetIsDeleted(int structureId);
        void DeleteStructure(int structureId);
    }
}
