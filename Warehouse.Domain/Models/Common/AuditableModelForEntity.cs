using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Warehouse.Domain.Models.Common
{
    public class AuditableModelForEntity : BaseEntity
    {
        public string CreatedById { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string ModifiedById { get; set; }
        public DateTime ModifiedDateTime { get; set; }
    }
}
