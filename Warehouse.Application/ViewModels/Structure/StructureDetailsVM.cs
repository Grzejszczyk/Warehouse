using AutoMapper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Warehouse.Application.Mapping;

namespace Warehouse.Application.ViewModels.Structure
{
    public class StructureDetailsVM
    {
        public int Id { get; set; }

        public string CreatedById { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string ModifiedById { get; set; }
        public DateTime ModifiedDateTime { get; set; }

        public string ProductName { get; set; }
        public string StructureName { get; set; }
        public string Project { get; set; }
        public List<ItemDetailsForStructureVM> StructureItems { get; set; }
    }

    public class StructureDetailsValidator : AbstractValidator<StructureDetailsVM>
    {
        public StructureDetailsValidator()
        {
            RuleFor(x => x.StructureName).NotNull();
            RuleFor(x => x.StructureName).NotEmpty();
            RuleFor(x => x.StructureName).Length(3, 100);
            RuleFor(x => x.ProductName).NotNull();
            RuleFor(x => x.ProductName).NotEmpty();
            RuleFor(x => x.ProductName).Length(3, 100);
            RuleFor(x => x.Project).NotNull();
            RuleFor(x => x.Project).NotEmpty();
            RuleFor(x => x.Project).Length(3, 100);
        }
    }
}
