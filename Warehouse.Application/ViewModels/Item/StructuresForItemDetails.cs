using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Warehouse.Application.Mapping;

namespace Warehouse.Application.ViewModels.Item
{
    public class StructuresForItemDetails
    {
        public int StructureId { get; set; }
        public string StructureName { get; set; }
        public int QuantityForStructure { get; set; }
    }
}
