using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Warehouse.Application.Mapping;

namespace Warehouse.Application.ViewModels.Item
{
    public class SupplierForEditItem : IMapFrom<Warehouse.Domain.Models.Entity.Supplier>
    {
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string SupplierNIP { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Warehouse.Domain.Models.Entity.Supplier, SupplierForEditItem>()
                .ReverseMap();
        }
    }
}
