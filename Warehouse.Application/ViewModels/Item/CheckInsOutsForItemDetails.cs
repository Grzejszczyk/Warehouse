using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse.Application.ViewModels.Item
{
    public class CheckInsOutsForItemDetails
    {
        public int CheckInOutId { get; set; }
        public DateTime ActionDateTime { get; set; }
        public int Quantity { get; set; }
    }
}
