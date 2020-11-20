using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Warehouse.Application.Mapping;

namespace Warehouse.Application.ViewModels.Structure
{
    public class StructureForListVM : IMapFrom<Warehouse.Domain.Models.Entity.Structure>
    {
        public int StructureId { get; set; }
        public string StructureName { get; set; }
        public string ProductName { get; set; }
        public string Project { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Warehouse.Domain.Models.Entity.Structure, StructureForListVM>()
                .ForMember(s => s.StructureId, opt => opt.MapFrom(s => s.Id))
                .ForMember(s => s.StructureName, opt => opt.MapFrom(s => s.Name));
        }
    }
}
