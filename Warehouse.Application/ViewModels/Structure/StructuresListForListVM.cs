using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Warehouse.Application.Mapping;
using Warehouse.Application.ViewModels.Pagination;

namespace Warehouse.Application.ViewModels.Structure
{
    public class StructuresListForListVM
    {
        public List<StructureForListVM> Structures { get; set; }
        public PagingInfo PaggingInfo { get; set; }
        public string SearchString { get; set; }
    }
}
