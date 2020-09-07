using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warehouse.Web.Models.Common;

namespace Warehouse.Web.Models.Entity
{
    public class Structure : BaseEntity
    {
        public string ProductName { get; set; }
        public string Subassembly { get; set; }
        public string Project { get; set; }
    }
}
