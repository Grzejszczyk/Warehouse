﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warehouse.Application.Interfaces;
using Warehouse.Application.ViewModels.CheckInOut;

namespace Warehouse.Web.Controllers
{
    [Authorize]
    public class CheckInOutController : Controller
    {
        private readonly ILogger<CheckInOutController> _logger;
        private readonly ICheckInOutService _checkInOutService;
        private int pageSizeStd;
        public CheckInOutController(ILogger<CheckInOutController> logger, ICheckInOutService checkInOutService)
        {
            _logger = logger;
            _checkInOutService = checkInOutService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin, SuperUser, User, Operator, Viewer")]
        [HttpGet]
        public IActionResult ItemsCheckOutList(int pageSize = 8, int pageNo = 1)
        {
            pageSizeStd = pageSize;
            var model = _checkInOutService.GetAllItemsForCheckInOutList(pageSizeStd, pageNo, "");
            return View(model);
        }

        [Authorize(Roles = "Admin, SuperUser, User, Operator, Viewer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ItemsCheckOutList(int pageSize = 8, int pageNo = 1, string searchString = "")
        {
            pageSizeStd = pageSize;
            if (searchString is null)
            {
                searchString = String.Empty;
            }
            var model = _checkInOutService.GetAllItemsForCheckInOutList(pageSizeStd, pageNo, searchString);
            return View(model);
        }

        [Authorize(Roles = "Admin, SuperUser, User, Operator, Viewer")]
        [HttpGet]
        public IActionResult ItemsCheckInList(int pageSize = 8, int pageNo = 1)
        {
            pageSizeStd = pageSize;
            var model = _checkInOutService.GetAllItemsForCheckInOutList(pageSizeStd, pageNo, "");
            return View(model);
        }
        [Authorize(Roles = "Admin, SuperUser, User, Operator, Viewer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ItemsCheckInList(int pageSize = 8, int pageNo = 1, string searchString = "")
        {
            pageSizeStd = pageSize;
            if (searchString is null)
            {
                searchString = String.Empty;
            }
            var model = _checkInOutService.GetAllItemsForCheckInOutList(pageSizeStd, pageNo, searchString);
            return View(model);
        }

        [Authorize(Roles = "Admin, SuperUser, User, Operator, Viewer")]
        [HttpPost]
        public IActionResult CheckOutItem(ItemForCheckInOutListVM itm)
        {
            _checkInOutService.CheckOut(itm.Id, itm.CheckInOutQty, User.Identity.Name);
            return RedirectToAction("ItemsCheckOutList");
        }

        [Authorize(Roles = "Admin, SuperUser, User, Operator, Viewer")]
        [HttpPost]
        public IActionResult CheckInItem(ItemForCheckInOutListVM itm)
        {
            _checkInOutService.CheckIn(itm.Id, itm.CheckInOutQty, User.Identity.Name);
            return RedirectToAction("ItemsCheckInList");
        }

        [Authorize(Roles = "Admin, SuperUser, User, Operator, Viewer")]
        [HttpGet]
        public IActionResult StructuresList(int pageSize = 8, int pageNo = 1)
        {
            pageSizeStd = pageSize;
            var model = _checkInOutService.GetStructures(pageSizeStd, pageNo, "");
            return View(model);
        }

        [Authorize(Roles = "Admin, SuperUser, User, Operator, Viewer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult StructuresList(int pageSize = 8, int pageNo = 1, string searchString = "")
        {
            pageSizeStd = pageSize;
            if (searchString is null)
            {
                searchString = String.Empty;
            }
            var model = _checkInOutService.GetStructures(pageSizeStd, pageNo, searchString);
            return View(model);
        }

        [Authorize(Roles = "Admin, SuperUser, User, Operator, Viewer")]
        [HttpPost]
        public IActionResult CheckOutStructureItems(int structureId)
        {
            _checkInOutService.CheckOutByStructure(structureId, User.Identity.Name);
            return RedirectToAction("StructuresList");
        }
    }
}
