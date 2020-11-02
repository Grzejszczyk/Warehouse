using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Warehouse.Application.ViewModels.Item
{
    public class NewItemVM
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [Range(0, Int32.MaxValue)]
        public int LowQuantityValue { get; set; }
        [Required]
        [Range(0, Int32.MaxValue)]
        public int Quantity { get; set; }
        public int StructureId { get; set; }
        public int CategoryId { get; set; }
    }
}
