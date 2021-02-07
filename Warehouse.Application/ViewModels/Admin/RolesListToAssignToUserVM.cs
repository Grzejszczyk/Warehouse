using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse.Application.ViewModels.Admin
{
    public class RolesListToAssignToUserVM
    {
        public string UserId { get; set; }
        public List<RoleIsAssigned> RoleIsAssignedList { get; set; }
    }
}
