using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Warehouse.Application.Interfaces;
using Warehouse.Application.ViewModels.Structure;
using Warehouse.Domain.Interfaces;
using Warehouse.Domain.Models.Entity;

namespace Warehouse.Application.Mapping
{
    internal class StructureMapping
    {
        internal List<StructureForListVM> MapStructureToListVM(IQueryable<Structure> structures)
        {
            return structures.Select(s => new StructureForListVM()
            {
                ProductName = s.ProductName,
                Project = s.Project,
                StructureId = s.Id,
                StructureName = s.Name
            }).ToList();
        }

        internal void MapStructureEntityFromVM(StructureDetailsVM structureDetailsVM, Structure structure)
        {
            structure.ProductName = structureDetailsVM.ProductName;
            structure.Project = structureDetailsVM.Project;
            structure.Id = structureDetailsVM.Id;
            structure.Name = structureDetailsVM.StructureName;
        }

        internal StructureDetailsVM MapStructureVM(Structure structure)
        {
            return new StructureDetailsVM
            {
                Id = structure.Id,
                CreatedById = structure.CreatedById,
                CreatedDateTime = structure.CreatedDateTime,
                ModifiedById = structure.ModifiedById,
                ModifiedDateTime = structure.ModifiedDateTime,
                ProductName = structure.ProductName,
                StructureName = structure.Name,
                Project = structure.Project
            };
        }
    }
}
