using System;
using System.Collections.Generic;
using System.Text;
using Warehouse.Domain.Models.Common;

namespace Warehouse.Domain.Models.Entity
{
    public class Tag : BaseEntity
    {
        public string TagName { get; set; }
        public ICollection<ItemTag> ItemTags { get; set; }
    }
}
