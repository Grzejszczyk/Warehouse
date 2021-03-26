using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Warehouse.Application.Mapping;
using Warehouse.Application.ViewModels.Pagination;

namespace Warehouse.Application.ViewModels.Supplier
{
    public class SupplierForListVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NIP { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
    }
}
