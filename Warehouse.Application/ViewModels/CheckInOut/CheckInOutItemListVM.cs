using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse.Application.ViewModels.CheckInOut
{
    public class CheckInOutItemListVM
    {
        public int IdemId { get; set; }
        public List<CheckInOutForListVM> CheckIns { get; set; }
        public List<CheckInOutForListVM> CheckOuts { get; set; }
    }
}
