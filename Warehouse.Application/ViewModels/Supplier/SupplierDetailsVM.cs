using AutoMapper;
using FluentValidation;
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

        public string CreatedById { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string ModifiedById { get; set; }
        public DateTime? ModifiedDateTime { get; set; }

        public string Name { get; set; }
        public string NIP { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Street { get; set; }
        public string BuildingNo { get; set; }
        public bool IsActive { get; set; }

        //Contact person:
        public string ContactPersonName { get; set; }
        public string ContactPersonSurname { get; set; }
        public string ContactPersonEmail { get; set; }
        public string ContactPersonPhoneNo { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Warehouse.Domain.Models.Entity.Supplier, SupplierDetailsVM>().ReverseMap();
        }
    }
    public class SupplierDetailsValidator : AbstractValidator<SupplierDetailsVM>
    {
        public SupplierDetailsValidator()
        {
            RuleFor(x => x.Name).Length(3, 100);
            RuleFor(x => x.NIP).Length(10);
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.PhoneNo).Length(7, 13);
            RuleFor(x => x.City).Length(3, 50);
            RuleFor(x => x.ZipCode).Matches("^(([0-9]{2})*-([0-9]{3}))$");
            RuleFor(x => x.Street).Length(3, 100);
            RuleFor(x => x.BuildingNo).Length(1, 100);
        }
    }
}
