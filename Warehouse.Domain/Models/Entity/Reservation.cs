using System;
using System.Collections.Generic;
using System.Text;
using Warehouse.Domain.Models.Common;

namespace Warehouse.Domain.Models.Entity
{
    public class Reservation : AuditableModelForEntity
    {
        public string ReservationNumber { get; set; }

        public ICollection<ItemReservation> ItemsReserved { get; set; }

        public string Comment { get; set; }
    }
}