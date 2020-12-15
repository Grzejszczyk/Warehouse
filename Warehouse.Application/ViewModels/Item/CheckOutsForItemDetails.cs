using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Warehouse.Application.Mapping;

namespace Warehouse.Application.ViewModels.Item
{
    public class CheckOutsForItemDetails : IMapFrom<Warehouse.Domain.Models.Entity.CheckOut>
    {
        public int CheckOutId { get; set; }
        public DateTime ActionDateTime { get; set; }
        public int Quantity { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Warehouse.Domain.Models.Entity.CheckOut, CheckOutsForItemDetails>()
                .ForMember(s => s.CheckOutId, d => d.MapFrom(d => d.Id))
                .ForMember(s => s.ActionDateTime, d => d.MapFrom(d => d.ModifiedDateTime))
                .ForMember(s => s.Quantity, d => d.MapFrom(d => d.Quantity))
                .ReverseMap();
        }
    }
}
