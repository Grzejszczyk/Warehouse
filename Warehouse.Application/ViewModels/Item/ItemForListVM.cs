using AutoMapper;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Warehouse.Application.Mapping;

namespace Warehouse.Application.ViewModels.Item
{
    public class ItemForListVM : IMapFrom<Warehouse.Domain.Models.Entity.Item>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Warehouse.Domain.Models.Entity.Item, ItemForListVM>();
        }
    }
}
