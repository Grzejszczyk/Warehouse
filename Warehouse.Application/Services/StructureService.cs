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
        //TODO: Implement structure service
        private readonly IStructureRepository _structureRepository;
        private readonly IMapper _mapper;
        public StructureService(IStructureRepository structureRepo, IMapper mapper)
        {
            _structureRepository = structureRepo;
            _mapper = mapper;
        }
        public int AddStructure(StructureDetailsVM newStructureVM)
        {
            Structure structure = new Structure();
            structure.Id = 0;
            structure.IsDeleted = false;
            structure.Name = newStructureVM.StructureName;
            structure.ProductName = newStructureVM.ProductName;
            structure.Project = newStructureVM.Project;

            var s = _structureRepository.AddStructure(structure);
            return s;
        }

        public int EditStructure(StructureDetailsVM newStrucuteVM)
        {
            Structure structure = _structureRepository.GetStructure(newStrucuteVM.StructureId);
            structure.Name = newStrucuteVM.StructureName;
            structure.ProductName = newStrucuteVM.ProductName;
            structure.Project = newStrucuteVM.Project;
            
            var s = _structureRepository.UpdateStructure(structure, newStrucuteVM.StructureId);
            return s;
            throw new NotImplementedException();
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

        public int SetIsDeleted(int structureId)
        {
            Structure structureEntity = _structureRepository.GetStructure(structureId);
            structureEntity.IsDeleted = true;
            _structureRepository.UpdateStructure(structureEntity, structureEntity.Id);
            return structureEntity.Id;
        }
        public void DeleteStructure(int structureId)
        {
            _structureRepository.DeleteStructure(structureId);
        }

        public StructureDetailsVM GetStructureDetails(int strucureId)
        {
            var structure = _structureRepository.GetStructure(strucureId);
            var supplierVM = _mapper.Map<StructureDetailsVM>(structure);
            return supplierVM;
        }

    }
}
