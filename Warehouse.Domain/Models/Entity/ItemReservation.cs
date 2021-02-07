using System;
using System.Collections.Generic;
using System.Text;
using Warehouse.Domain.Models.Common;

namespace Warehouse.Domain.Models.Entity
{
    public class ItemReservation : BaseEntity
    {
        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }
        public int ItemId { get; set; }
        public Item Item { get; set; }
        public int ItemQtyForReservation { get; set; }
    }
}
