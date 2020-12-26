using System;
using System.Collections.Generic;
using System.Text;
using Warehouse.Domain.Models.Common;

namespace Warehouse.Domain.Models.Entity
{
    public class MagazineZone : AuditableModelForEntity
    {
        public string Name { get; set; }
        public ICollection<Item> Items { get; set; }
    }
}
