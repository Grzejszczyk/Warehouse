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
        int AddStructure(StructureDetailsVM newStructureVM, string userId);
        int EditStructure(StructureDetailsVM structureVM, string userId);
        int SetIsDeleted(int structureId, string userId);
    }
}
