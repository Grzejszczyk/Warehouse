using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Warehouse.Application.Interfaces;
using Warehouse.Application.ViewModels.Admin;
using Warehouse.Domain.Interfaces;

namespace Warehouse.Application.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;
        public AdminService(IAdminRepository adminRepo)
        {
            _adminRepository = adminRepo;
        }

        public UsersListVM GetAllUsers()
        {
            var usersVM = new UsersListVM();
            usersVM.Users = new List<UserDataToList>();
            var users = _adminRepository.GetAllUsers();

            foreach (var u in users)
            {
                usersVM.Users.Add(new UserDataToList()
                {
                    UserId = u.Id,
                    UserName = u.UserName,
                });
            }
            return usersVM;
        }

        public UserDataDetails GetUserDetails(string userId)
        {
            var user = _adminRepository.GetUserDetails(userId);

            var rolesForUser = new List<Role>();

            var userVM = new UserDataDetails()
            {
                UserId = user.Id,
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed,
                UserName = user.UserName,
            };
            return userVM;
        }

        public RolesListToAssignToUserVM GetUserRolesForEdit(string userId)
        {
            var roleListForUser = new RolesListToAssignToUserVM();
            var userRoles = _adminRepository.GetUserRoles(userId).ToList();
            var allRoles = _adminRepository.GetAllRoles().ToList();
            roleListForUser.UserId = userId;
            roleListForUser.RoleIsAssignedList = new List<RoleIsAssigned>();

            foreach (var r in allRoles)
            {
                if (userRoles.Contains(r.Id))
                {
                    roleListForUser.RoleIsAssignedList.Add(new RoleIsAssigned() { RoleId = r.Id, RoleName = r.Name, IsAssignedToRole = true });
                }
                else
                {
                    roleListForUser.RoleIsAssignedList.Add(new RoleIsAssigned() { RoleId = r.Id, RoleName = r.Name, IsAssignedToRole = false });
                }
            }
            return roleListForUser;
        }

        public string AssignRolesToUser(string userId, RolesListToAssignToUserVM rolesListToAssignToUserVM)
        {
            var roles = new List<string>();
            foreach (var r in rolesListToAssignToUserVM.RoleIsAssignedList)
            {
                if (r.IsAssignedToRole)
                {
                    roles.Add(r.RoleId);
                }
            }
            _adminRepository.AssignRolesToUser(userId, roles);
            return userId;
        }

        public RolesListVM GetAllRoles()
        {
            var rolesVM = new RolesListVM();
            rolesVM.Roles = new List<Role>();
            var roles = _adminRepository.GetAllRoles();
            foreach (var r in roles)
            {
                rolesVM.Roles.Add(new Role() { Id = r.Id, Name = r.Name });
            }
            return rolesVM;
        }
    }
}
