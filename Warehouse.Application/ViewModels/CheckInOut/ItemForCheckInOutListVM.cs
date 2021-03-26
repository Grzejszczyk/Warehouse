using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Warehouse.Application.Mapping;

namespace Warehouse.Application.ViewModels.CheckInOut
{
    public class ItemForCheckInOutListVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public int CheckInOutQty { get; set; }
    }
}
