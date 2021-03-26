using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Warehouse.Application.Mapping;

namespace Warehouse.Application.ViewModels.Structure
{
    public class StructureForListVM
    {
        public int StructureId { get; set; }
        public string StructureName { get; set; }
        public string ProductName { get; set; }
        public string Project { get; set; }
    }
}
