using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Warehouse.Application.Mapping;
using Warehouse.Application.ViewModels.Item;

namespace Warehouse.Application.ViewModels.CheckInOut
{
    public class StructureForCheckOutVM
    {
        public int StructureId { get; set; }
        public string Name { get; set; }
        public string ProductName { get; set; }
        public string Project { get; set; }

        public List<ItemForCheckInOutListVM> Items { get; set; }
    }
}
