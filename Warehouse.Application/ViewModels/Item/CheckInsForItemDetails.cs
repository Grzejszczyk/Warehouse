using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Warehouse.Application.Mapping;

namespace Warehouse.Application.ViewModels.Item
{
    public class CheckInsForItemDetails
    {
        public int CheckInId { get; set; }
        public DateTime ActionDateTime { get; set; }
        public string UserName { get; set; }
        public int Quantity { get; set; }
    }
}
