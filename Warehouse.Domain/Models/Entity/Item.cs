﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warehouse.Domain.Models.Common;

namespace Warehouse.Domain.Models.Entity
{
    public class Item : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int LowQuantityValue { get; set; }
        public Structure Structure {get; set; } //Where Used
        public Category Category { get; set; }
        public Supplier Supplier { get; set; }
        public int Quantity { get; set; }
    }
}
