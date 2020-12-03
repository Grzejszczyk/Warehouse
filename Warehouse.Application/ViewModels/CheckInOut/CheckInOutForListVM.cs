using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse.Application.ViewModels.CheckInOut
{
    public class CheckInOutForListVM
    {
        public int CheckInOutId { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public int CreatedById { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}
