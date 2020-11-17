using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse.Application.ViewModels.Structure
{
    public class StrutureDetailsVM
    {
        public int StructureId { get; set; }
        public string CreatedById { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string ModifiedById { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
        public string ProductName { get; set; }
        public string Subassembly { get; set; }
        public string Project { get; set; }
        public List<ItemForStructureListVM> StructureItems { get; set; }
    }
}
