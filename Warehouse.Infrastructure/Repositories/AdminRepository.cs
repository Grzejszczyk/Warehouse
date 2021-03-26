using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Warehouse.Domain.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Warehouse.Infrastructure.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly Context _context;

        public AdminRepository(Context context)
        {
            _context = context;
        }
        public IQueryable<IdentityUser> GetAllUsers()
        {
            var users = _context.Users;
            return users;
        }
        public IdentityUser GetUserDetails(string userId)
        {
            var user = _context.Users
                //.Include(r=>r.UserRoles)
                .FirstOrDefault(i => i.Id == userId);
            return user;
        }

        public IQueryable<IdentityRole> GetAllRoles()
        {
            var roles = _context.Roles;
            return roles;
        }
        public IQueryable<string> GetUserRoles(string userId)
        {
            var roles = _context.UserRoles
                .Where(u => u.UserId == userId)
                .Select(u => u.RoleId);
            return roles;
        }

        public string AssignRolesToUser(string userId, List<string> roles)
        {
            var user = _context.UserRoles.Where(u => u.UserId == userId);
            _context.UserRoles.RemoveRange(user);

            foreach (var r in roles)
            {
                var newUserRoles = _context.UserRoles.Add(new IdentityUserRole<string> { UserId = userId, RoleId = r });
            }

            _context.SaveChanges();

            return userId;
        }
    }
}
