using Microsoft.AspNetCore.Authorization;
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
        private readonly ILogger<ItemController> _logger;
        private readonly ICheckInOutService _checkInOutService;
        private int pageSizeStd;
        public CheckInOutController(ILogger<ItemController> logger, ICheckInOutService checkInOutRepo)
        {
            _logger = logger;
            _checkInOutService = checkInOutRepo;
        }
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Policy = "CanView")]
        [HttpGet]
        public IActionResult ItemsCheckOutList(int pageSize = 8, int pageNo = 1)
        {
            pageSizeStd = pageSize;
            var model = _checkInOutService.GetAllItemsForCheckInOutList(pageSizeStd, pageNo, "");
            return View(model);
        }

        [Authorize(Policy = "CanView")]
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

        [Authorize(Policy = "CanView")]
        [HttpGet]
        public IActionResult ItemsCheckInList(int pageSize = 8, int pageNo = 1)
        {
            pageSizeStd = pageSize;
            var model = _checkInOutService.GetAllItemsForCheckInOutList(pageSizeStd, pageNo, "");
            return View(model);
        }
        [Authorize(Policy = "CanView")]
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

        [Authorize(Policy = "CanCheckInOut")]
        [HttpPost]
        public IActionResult CheckOutItem(ItemForCheckInOutListVM itm)
        {
            _checkInOutService.CheckOut(itm.Id, itm.CheckInOutQty, "testCheckIOUser");
            return RedirectToAction("ItemsCheckOutList");
        }

        [Authorize(Policy = "CanCheckInOut")]
        [HttpPost]
        public IActionResult CheckInItem(ItemForCheckInOutListVM itm)
        {
            _checkInOutService.CheckIn(itm.Id, itm.CheckInOutQty, "testCheckIOUser");
            return RedirectToAction("ItemsCheckInList");
        }

        [Authorize(Policy = "CanView")]
        [HttpGet]
        public IActionResult StructuresList(int pageSize = 8, int pageNo = 1)
        {
            pageSizeStd = pageSize;
            var model = _checkInOutService.GetStructures(pageSizeStd, pageNo, "");
            return View(model);
        }

        [Authorize(Policy = "CanView")]
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

        [Authorize(Policy = "CanCheckInOut")]
        [HttpPost]
        public IActionResult CheckOutStructureItems(int structureId)
        {
            _checkInOutService.CheckOutByStructure(structureId, "testUser");
            return RedirectToAction("StructuresList");
        }
    }
}
