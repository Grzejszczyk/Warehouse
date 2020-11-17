using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse.Application.ViewModels.CheckInOut
{
    public class CheckInOutListVM
    {
        public int IdemId { get; set; }
        public List<CheckInForListVM> CheckIns { get; set; }
        public List<CheckOutForListVM> CheckOuts { get; set; }
    }
}
