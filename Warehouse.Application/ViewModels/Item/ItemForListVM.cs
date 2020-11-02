using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse.Application.ViewModels.Item
{
    public class ItemForListVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public int Quantity { get; set; }
    }
}
