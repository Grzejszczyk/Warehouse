using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warehouse.Domain.Models.Common;

namespace Warehouse.Domain.Models.Entity
{
    public class Structure : BaseEntity
    {
        public string ProductName { get; set; }
        public string Subassembly { get; set; }
        public string Project { get; set; }
        public ICollection<Item> Items { get; set; }
    }
}
