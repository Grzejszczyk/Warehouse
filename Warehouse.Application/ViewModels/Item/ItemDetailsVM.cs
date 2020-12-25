using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Warehouse.Application.Mapping;
using Warehouse.Domain.Models.Entity;

namespace Warehouse.Application.ViewModels.Item
{
    public class ItemDetailsVM : IMapFrom<Warehouse.Domain.Models.Entity.Item>
    {
        public int Id { get; set; }

        public string CreatedById { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string ModifiedById { get; set; }
        public DateTime ModifiedDateTime { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string CatalogNumber { get; set; }
        public string DrawingNumber { get; set; }
        public int LowQuantityValue { get; set; }
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public int Quantity { get; set; }

        public byte[] ImageFile { get; set; }
        //For View img src, not mapped, assigned in service method.
        public string ImageData { get; set; }

        public string MagazineZoneName { get; set; }

        public List<StructuresForItemDetails> StructuresForItemDetails { get; set; }
        public List<CheckInsForItemDetails> CheckIns { get; set; }
        public List<CheckOutsForItemDetails> CheckOuts { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Warehouse.Domain.Models.Entity.Item, ItemDetailsVM>()
                .ForMember(s => s.SupplierId, opt => opt.MapFrom(d => d.Supplier.Id))
                .ForMember(s => s.SupplierName, opt => opt.MapFrom(d => d.Supplier.Name))
                .ForMember(s => s.ImageFile, opt => opt.MapFrom(d => d.ItemImage.Image))
                .ForMember(s => s.ImageData, opt => opt.Ignore())
                .ForMember(s => s.MagazineZoneName, opt => opt.MapFrom(d => d.MagazineZone.Name))
                .ForMember(s => s.StructuresForItemDetails, opt => opt.Ignore())
                .ForMember(s => s.CheckIns, opt => opt.Ignore())
                .ForMember(s => s.CheckOuts, opt => opt.Ignore());
        }
    }
}
