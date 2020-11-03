using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Warehouse.Application.Mapping;
using Warehouse.Domain.Models.Entity;

namespace Warehouse.Application.ViewModels.Item
{
    public class ItemDetailsVM : IMapFrom<Warehouse.Domain.Models.Entity.Item>
    {
        public int Id { get; set; }

        public int CreatedById { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int ModifiedById { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [Range(0, Int32.MaxValue)]
        public int LowQuantityValue { get; set; }
        public int StructureId { get; set; } //Where Used Id
        public string StructureName { get; set; } //Where Used
        public string CategoryName { get; set; }
        public string SupplierName { get; set; }
        [Required]
        [Range(0, Int32.MaxValue)]
        public int Quantity { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Warehouse.Domain.Models.Entity.Item, ItemDetailsVM>()
                .ForMember(s => s.StructureId, opt => opt.MapFrom(d => d.Structure.Id))
                .ForMember(s => s.StructureName, opt => opt.MapFrom(d => d.Structure.ProductName))
                .ForMember(s => s.CategoryName, opt => opt.MapFrom(d => d.Category.CategoryName))
                .ForMember(s => s.SupplierName, opt => opt.MapFrom(d => d.Supplier.Name));
        }
    }
}
