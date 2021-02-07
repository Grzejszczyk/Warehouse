using System;
using System.Collections.Generic;
using System.Text;
using Warehouse.Application.ViewModels.Admin;

namespace Warehouse.Application.Interfaces
{
    public interface IAdminService
    {
        UsersListVM GetAllUsers();
        RolesListVM GetAllRoles();
        UserDataDetails GetUserDetails(string userId);
        RolesListToAssignToUserVM GetUserRolesForEdit(string userId);
        string AssignRolesToUser(string userId, RolesListToAssignToUserVM rolesListToAssignToUserVM);
    }
}
