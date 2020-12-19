using System;
using System.Collections.Generic;
using System.Text;
using Warehouse.Domain.Models.Common;

namespace Warehouse.Domain.Models.Entity
{
    public class ContactPerson : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }

        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }
    }
}
