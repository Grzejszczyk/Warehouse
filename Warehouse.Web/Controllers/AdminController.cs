using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warehouse.Application.Interfaces;
using Warehouse.Application.ViewModels.Admin;

namespace Warehouse.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly IAdminService _adminService;

        public AdminController(ILogger<AdminController> logger, IAdminService adminService)
        {
            _logger = logger;
            _adminService = adminService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Users()
        {
            var users = _adminService.GetAllUsers();
            return View(users);
        }
        public IActionResult UserDetails(string userId)
        {
            var user = _adminService.GetUserDetails(userId);
            return View(user);
        }
        public IActionResult Roles()
        {
            var roles = _adminService.GetAllRoles();
            return View(roles);
        }

        [HttpGet]
        public IActionResult EditUserRoles(string userId)
        {
            var userRoles = _adminService.GetUserRolesForEdit(userId);
            return View(userRoles);
        }
        [HttpPost]
        public IActionResult EditUserRoles(RolesListToAssignToUserVM model)
        {
            _adminService.AssignRolesToUser(model.UserId, model);
            return RedirectToAction("UserDetails", new { userId = model.UserId } );
        }
    }
}
