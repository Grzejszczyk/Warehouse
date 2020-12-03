using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Warehouse.Application.Interfaces;
using Warehouse.Application.ViewModels.Pagination;
using Warehouse.Application.ViewModels.Structure;
using Warehouse.Domain.Interfaces;
using Warehouse.Domain.Models.Entity;

namespace Warehouse.Application.Services
{
    public class StructureService : IStructureService
    {
        private readonly IStructureRepository _structureRepository;
        private readonly IMapper _mapper;
        public StructureService(IStructureRepository structureRepo, IMapper mapper)
        {
            _structureRepository = structureRepo;
            _mapper = mapper;
        }
        public StructuresListForListVM StructuresList(int pageSize, int pageNo, string searchString)
        {
            var suppliers = _structureRepository.GetStructures().Where(s => s.Name.StartsWith(searchString)).ProjectTo<StructureForListVM>(_mapper.ConfigurationProvider).ToList();

            var suppliersToShow = suppliers.Skip(pageSize * (pageNo - 1)).Take(pageSize);
            var suppliersList = new StructuresListForListVM()
            {
                PaggingInfo = new PagingInfo() { CurrentPage = pageNo, ItemsPerPage = pageSize, TotalItems = suppliers.Count() },
                Structures = suppliersToShow.ToList()
            };
            return suppliersList;
        }
        public StructureDetailsVM GetStructureDetails(int strucureId)
        {
            var structure = _structureRepository.GetStructure(strucureId);
            var supplierVM = _mapper.Map<StructureDetailsVM>(structure);
            return supplierVM;
        }
        public int AddStructure(StructureDetailsVM newStructureVM, string userId)
        {
            Structure structure = new Structure();
            structure.Id = 0;
            structure.IsDeleted = false;
            structure.Name = newStructureVM.StructureName;
            structure.ProductName = newStructureVM.ProductName;
            structure.Project = newStructureVM.Project;

            var s = _structureRepository.AddStructure(structure, userId);
            return s;
        }

        public EditStructureVM GetStructureDetailsForEdit(int structureId)
        {
            var structure = _structureRepository.GetStructure(structureId);
            var structureVM = new EditStructureVM();
            structureVM = _mapper.Map<EditStructureVM>(structure);
            return structureVM;
        }

        public int EditStructure(StructureDetailsVM strucuteVM, string userId)
        {
            Structure structure = _structureRepository.GetStructure(strucuteVM.StructureId);
            structure.Name = strucuteVM.StructureName;
            structure.ProductName = strucuteVM.ProductName;
            structure.Project = strucuteVM.Project;
            
            var s = _structureRepository.UpdateStructure(structure, strucuteVM.StructureId, userId);
            return s;
        }

        public int SetIsDeleted(int structureId, string userId)
        {
            Structure structureEntity = _structureRepository.GetStructure(structureId);
            structureEntity.IsDeleted = true;
            _structureRepository.UpdateStructure(structureEntity, structureId, userId);
            return structureEntity.Id;
        }
    }
}
