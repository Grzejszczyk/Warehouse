using System;
using System.Collections.Generic;
using System.Text;
using Warehouse.Application.ViewModels.Pagination;

namespace Warehouse.Application.ViewModels.CheckInOut
{
    public class StructuresListForCheckOutVM
    {
        public List<StructureForCheckOutVM> Structures { get; set; }
        public PagingInfo PaggingInfo { get; set; }
        public string SearchString { get; set; }
    }
}
