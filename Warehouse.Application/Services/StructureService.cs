using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Warehouse.Application.Interfaces;
using Warehouse.Application.Mapping;
using Warehouse.Application.ViewModels.Pagination;
using Warehouse.Application.ViewModels.Structure;
using Warehouse.Domain.Interfaces;
using Warehouse.Domain.Models.Entity;

namespace Warehouse.Application.Services
{
    public class StructureService : IStructureService
    {
        private readonly IStructureRepository _structureRepository;
        private StructureMapping structureMapping;
        public StructureService(IStructureRepository structureRepo, IMapper mapper)
        {
            _structureRepository = structureRepo;
            structureMapping = new StructureMapping();
        }

        public StructuresListForListVM StructuresList(int pageSize, int pageNo, string searchString)
        {
            var structure = _structureRepository.GetStructures()
                .Where(s => s.IsDeleted == false)
                .Where(s => s.Name.StartsWith(searchString))
                .Skip(pageSize * (pageNo - 1)).Take(pageSize);

            var structuresToShow = structureMapping.MapStructureToListVM(structure);

            var structureList = new StructuresListForListVM()
            {
                PaggingInfo = new PagingInfo() { CurrentPage = pageNo, ItemsPerPage = pageSize, TotalItems = structure.Count() },
                Structures = structuresToShow.ToList()
            };
            return structureList;
        }
        public StructureDetailsVM GetStructureDetails(int strucureId)
        {
            var structure = _structureRepository.GetStructure(strucureId);
            var supplierVM = structureMapping.MapStructureVM(structure);
            return supplierVM;
        }

        public StructureDetailsVM GetStructureDetailsForEdit(int structureId)
        {
            var structure = _structureRepository.GetStructure(structureId);
            var structureVM = structureMapping.MapStructureVM(structure);
            return structureVM;
        }

        public int AddStructure(StructureDetailsVM newStructureVM, string userId)
        {
            Structure structure = new Structure();
            structureMapping.MapStructureEntityFromVM(newStructureVM, structure);
            var s = _structureRepository.AddStructure(structure, userId);
            return s;
        }

        public int EditStructure(StructureDetailsVM structuteVM, string userId)
        {
            Structure structure = _structureRepository.GetStructure(structuteVM.Id);
            structureMapping.MapStructureEntityFromVM(structuteVM, structure);
            var structureMapped = _structureRepository.EditStructure(structure, structuteVM.Id, userId);
            return structureMapped;
        }

        public int SetIsDeleted(int structureId, string userId)
        {
            Structure structureEntity = _structureRepository.GetStructure(structureId);
            structureEntity.IsDeleted = true;
            _structureRepository.EditStructure(structureEntity, structureId, userId);
            return structureEntity.Id;
        }
    }
}
