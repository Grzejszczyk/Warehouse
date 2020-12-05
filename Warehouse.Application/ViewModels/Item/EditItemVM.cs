using AutoMapper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Warehouse.Application.Mapping;
using Warehouse.Application.ViewModels.Supplier;

namespace Warehouse.Application.ViewModels.Item
{
    public class EditItemVM : IMapFrom<Warehouse.Domain.Models.Entity.Item>
    {
        public int Id { get; set; }

        public string CreatedById { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string ModifiedById { get; set; }
        public DateTime ModifiedDateTime { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public int LowQuantityValue { get; set; }
        public int Quantity { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Warehouse.Domain.Models.Entity.Item, EditItemVM>().ReverseMap();
        }
    }

    public class EditItemValidator : AbstractValidator<EditItemVM>
    {
        public EditItemValidator()
        {
            RuleFor(x => x.Name).Length(3,100);
            RuleFor(x => x.Description).Length(3,255);
            RuleFor(x => x.LowQuantityValue).NotEmpty();
            RuleFor(x => x.LowQuantityValue).GreaterThanOrEqualTo(1);
            RuleFor(x => x.Quantity).NotEmpty();
            RuleFor(x => x.Quantity).GreaterThanOrEqualTo(0);
        }
    }
}
