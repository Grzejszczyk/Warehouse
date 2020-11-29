using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Warehouse.Application.Mapping;
using System.Linq;

namespace Warehouse.Application.ViewModels.Item
{
    public class ItemStructureVM : IMapFrom<Warehouse.Domain.Models.Entity.ItemStructure>
    {
        public bool IsAssigned { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int StructureId { get; set; }
        public string StructureName { get; set; }
        public string ProjectName { get; set; }
        public string ProductName { get; set; }
        public int ItemQty { get; set; }

        //TODO: remove automapper
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Warehouse.Domain.Models.Entity.ItemStructure, ItemStructureVM>()
                .ForMember(s => s.IsAssigned, opt => opt.Ignore())
                .ForMember(s => s.ItemId, opt => opt.MapFrom(d => d.ItemId))
                .ForMember(s => s.ItemName, opt => opt.MapFrom(d => d.Item.Name))
                .ForMember(s => s.StructureId, opt => opt.MapFrom(d => d.Structure.Id))
                .ForMember(s => s.StructureName, opt => opt.MapFrom(d => d.Structure.Name))
                .ForMember(s => s.ProjectName, opt => opt.MapFrom(d => d.Structure.Project))
                .ForMember(s => s.ProductName, opt => opt.MapFrom(d => d.Structure.ProductName))
                .ForMember(s => s.ItemQty, opt => opt.MapFrom(d => d.ItemQuantity));
        }
    }
}
