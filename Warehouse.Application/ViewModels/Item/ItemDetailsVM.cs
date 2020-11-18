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
        public string CategoryName { get; set; }
        public string SupplierName { get; set; }

        [Required]
        [Range(0, Int32.MaxValue)]
        public int Quantity { get; set; }

        public List<StructuresForItemDetails> StructuresForItemDetails { get; set; }
        public List<CheckInsOutsForItemDetails> CheckIns { get; set; }
        public List<CheckInsOutsForItemDetails> CheckOuts { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Warehouse.Domain.Models.Entity.Item, ItemDetailsVM>()
                .ForMember(s => s.CategoryName, opt => opt.MapFrom(d => d.Category.CategoryName))
                .ForMember(s => s.SupplierName, opt => opt.MapFrom(d => d.Supplier.Name))
                .ForMember(s=>s.CheckOuts, opt=>opt.Ignore())
                .ForMember(s=>s.CheckIns, opt=>opt.Ignore())
                .ReverseMap();
        }
    }
}
