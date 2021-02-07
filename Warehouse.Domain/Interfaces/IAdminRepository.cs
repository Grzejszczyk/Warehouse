using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Warehouse.Domain.Interfaces
{
    public interface IAdminRepository
    {
        IQueryable<IdentityUser> GetAllUsers();
        IQueryable<IdentityRole> GetAllRoles();
        IdentityUser GetUserDetails(string userId);
        IQueryable<string> GetUserRoles(string userId);
        public string AssignRolesToUser(string userId, List<string> roles);
    }
}
