using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warehouse.Web.Models.Common;

namespace Warehouse.Web.Models.Entity
{
    public class Item : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int LowQuantityValue { get; set; }
        public int WhereUsed { get; set; }  //Structure Id
    }
}
