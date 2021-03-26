using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Warehouse.Application.Mapping;

namespace Warehouse.Application.ViewModels.Item
{
    public class SupplierForEditItem
    {
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string SupplierNIP { get; set; }
    }
}
