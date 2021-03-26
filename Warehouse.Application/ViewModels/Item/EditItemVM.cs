using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Warehouse.Application.Mapping;
using Warehouse.Application.ViewModels.Supplier;

namespace Warehouse.Application.ViewModels.Item
{
    public class EditItemVM
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string CatalogNumber { get; set; }
        public string DrawingNumber { get; set; }
        public int LowQuantityValue { get; set; }
        public int Quantity { get; set; }

        public byte[] Thumbnail { get; set; }
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
    }

    public class EditItemValidator : AbstractValidator<EditItemVM>
    {
        public EditItemValidator()
        {
            RuleFor(x => x.Name).Length(3, 100);
            RuleFor(x => x.Description).Length(3, 255);
            RuleFor(x => x.LowQuantityValue).NotEmpty();
            RuleFor(x => x.LowQuantityValue).GreaterThanOrEqualTo(1);
            RuleFor(x => x.Quantity).NotEmpty();
            RuleFor(x => x.Quantity).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Thumbnail.Length).LessThan(5242880).WithMessage("Plik za duży");
        }
    }
}
