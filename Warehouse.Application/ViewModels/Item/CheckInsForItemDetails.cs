using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Warehouse.Application.Mapping;

namespace Warehouse.Application.ViewModels.Item
{
    public class CheckInsForItemDetails : IMapFrom<Warehouse.Domain.Models.Entity.CheckIn>
    {
        public int CheckInId { get; set; }
        public DateTime ActionDateTime { get; set; }
        public string UserName { get; set; }
        public int Quantity { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Warehouse.Domain.Models.Entity.CheckIn, CheckInsForItemDetails>()
                .ForMember(s => s.CheckInId, d => d.MapFrom(d => d.Id))
                .ForMember(s => s.ActionDateTime, d => d.MapFrom(d => d.ModifiedDateTime))
                .ForMember(s => s.Quantity, d => d.MapFrom(d => d.Quantity))
                .ForMember(s => s.UserName, d => d.MapFrom(d => d.ModifiedById))
                .ReverseMap();
        }
    }
}
