using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Warehouse.Application.Mapping;

namespace Warehouse.Application.ViewModels.Item
{
    public class StructuresForItemDetails : IMapFrom<Warehouse.Domain.Models.Entity.ItemStructure>
    {
        public int StructureId { get; set; }
        public string StructureName { get; set; }
        public int QuantityForStructure { get; set; }

        //Issue: Mapping doesn't work.
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Warehouse.Domain.Models.Entity.ItemStructure, StructuresForItemDetails>()
                .ForMember(s => s.StructureId, d => d.MapFrom(d => d.StructureId))
                .ForMember(s => s.StructureName, d => d.MapFrom(d => d.Structure.Name))
                .ForMember(s => s.QuantityForStructure, d => d.MapFrom(d => d.ItemQuantity))
                .ReverseMap();
        }
    }
}
