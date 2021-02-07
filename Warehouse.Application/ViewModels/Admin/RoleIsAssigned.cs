using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse.Application.ViewModels.Admin
{
    public class RoleIsAssigned
    {
        public bool IsAssignedToRole { get; set; }
        public string RoleName { get; set; }
        public string RoleId { get; set; }
    }
}
