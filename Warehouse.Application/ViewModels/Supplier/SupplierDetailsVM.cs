using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Warehouse.Application.Mapping;

namespace Warehouse.Application.ViewModels.Supplier
{
    public class SupplierDetailsVM : IMapFrom<Warehouse.Domain.Models.Entity.Supplier>
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string NIP { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNo { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string ZipCode { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string BuildingNo { get; set; }
        [Required]
        public bool IsActive { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Warehouse.Domain.Models.Entity.Supplier, SupplierDetailsVM>().ReverseMap();
        }
    }
}
