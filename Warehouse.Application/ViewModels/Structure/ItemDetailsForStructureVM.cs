using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warehouse.Domain.Models.Common;

namespace Warehouse.Application.ViewModels.Structure
{
    public class ItemDetailsForStructureVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ItemsQuantityForStructure { get; set; }
    }
}