using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warehouse.Domain.Models.Common;

namespace Warehouse.Domain.Models.Entity
{
    public class Supplier : AuditableModelForEntity
    {
        public string Name { get; set; }
        public string NIP { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Street { get; set; }
        public string BuildingNo { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        //Contact person:
        public string ContactPersonName { get; set; }
        public string ContactPersonSurname { get; set; }
        public string ContactPersonEmail { get; set; }
        public string ContactPersonPhoneNo { get; set; }

        public ICollection<Item> Items { get; set; }
    }
}
