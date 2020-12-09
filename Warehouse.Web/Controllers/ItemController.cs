﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Warehouse.Application.Interfaces;
using Warehouse.Application.ViewModels.Item;
using Warehouse.Application.ViewModels.Supplier;
using Warehouse.Web.Filters;

namespace Warehouse.Web.Controllers
{
    [Authorize]
    public class ItemController : Controller
    {
        private readonly ILogger<ItemController> _logger;
        private readonly IItemService _itemService;
        private readonly ISupplierService _supplierService;
        private int pageSizeStd;

        public ItemController(ILogger<ItemController> logger, IItemService itemRepo, ISupplierService supplierRepo)
        {
            _logger = logger;
            _itemService = itemRepo;
            _supplierService = supplierRepo;
        }

        [CheckPermissions("ViewItems")] //Or policy = CanView
        [HttpGet]
        public IActionResult ItemsList(int pageSize = 5, int pageNo = 1)
        {
            //TODO: Implement Logger.
            _logger.LogInformation("Jestem w Warehousecontroller ItemsList - to logger.");
            pageSizeStd = pageSize;
            var model = _itemService.GetAllItemsForList(pageSizeStd, pageNo, "");
            return View(model);
        }
        [CheckPermissions("ViewItems")] //Or policy = CanView
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ItemsList(int pageSize = 5, int pageNo = 1, string searchString = "")
        {
            pageSizeStd = pageSize;
            if (searchString is null) { searchString = String.Empty; }
            var model = _itemService.GetAllItemsForList(pageSizeStd, pageNo, searchString);
            return View(model);
        }
        [CheckPermissions("ViewItems")]  //Or policy = CanView
        [HttpGet]
        public IActionResult ItemsListFromSupplier(int supplierId, int pageSize = 7, int pageNo = 1)
        {
            pageSizeStd = pageSize;
            var model = _itemService.GetItemsBySupplier(supplierId, pageSizeStd, pageNo, "");
            return View("ItemsList", model);
        }
        [CheckPermissions("ViewItems")]  //Or policy = CanView
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ItemsListFromSupplier(int supplierId, int pageSize = 7, int pageNo = 1, string searchString = "")
        {
            pageSizeStd = pageSize;
            if (searchString is null) { searchString = String.Empty; }
            var model = _itemService.GetItemsBySupplier(supplierId, pageSizeStd, pageNo, searchString);
            return View("ItemsList", model);
        }
        [CheckPermissions("ViewItems")]  //Or policy = CanView
        [HttpGet]
        public IActionResult ItemsListFromStructure(int structureId, int pageSize = 7, int pageNo = 1)
        {
            pageSizeStd = pageSize;
            var model = _itemService.GetItemsByStructure(structureId, pageSizeStd, pageNo, "");
            return View("ItemsList", model);
        }
        [CheckPermissions("ViewItems")]  //Or policy = CanView
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ItemsListFromStructure(int structureId, int pageSize = 7, int pageNo = 1, string searchString = "")
        {
            pageSizeStd = pageSize;
            if (searchString is null) { searchString = String.Empty; }
            var model = _itemService.GetItemsByStructure(structureId, pageSizeStd, pageNo, searchString);
            return View("ItemsList", model);
        }

        [Authorize(Policy = "CanManageItems")]
        [HttpGet]
        public IActionResult EditItem(int id = 0)
        {
            if (id != 0)
            {
                var item = _itemService.GetItemDetailsForEdit(id);
                return View(item);
            }
            else { return View(new EditItemVM()); }
        }
        [Authorize(Policy = "CanManageItems")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditItem(EditItemVM model)
        {
            int newItemId = 0;
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {
                    newItemId = _itemService.AddItem(model, "testUserId");
                }
                else
                {
                    newItemId = _itemService.EditItem(model, "testUserId");
                }
                return RedirectToAction("ItemDetails", new { id = newItemId });
            }
            return View(model);
        }

        [CheckPermissions("ViewItems")]  //Or policy = CanView
        public IActionResult ItemDetails(int id)
        {
            var model = _itemService.GetItemDetails(id);
            return View(model);
        }

        [Authorize(Policy = "CanManageItems")]
        public IActionResult SetIsDeletedItem(int id)
        {
            _itemService.SetIsDeleted(id, "testUserId");
            return RedirectToAction("ItemsList");
        }

        [Authorize(Policy = "CanManageItems")]
        [HttpGet]
        public IActionResult AssignItemToSupplier(int itemId)
        {
            var itemSuppliers = _itemService.GetItemForSuppliersList(itemId);

            return View(itemSuppliers);
        }

        [Authorize(Policy = "CanManageItems")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AssignItemToSupplier(int itemId, int supplierId)
        {
            var assignedItem = _itemService.AssignItemToSupplier(
                itemId,
                supplierId,
                "testUserId");

            return RedirectToAction("ItemDetails", new { id = assignedItem });
        }

        [Authorize(Policy = "CanManageItems")]
        [HttpGet]
        public IActionResult AssignItemToStructures(int itemId)
        {
            var itemsStructuresListVM = _itemService.GetItemStructuresForAssign(itemId);
            return View(itemsStructuresListVM);
        }

        [Authorize(Policy = "CanManageItems")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AssignItemToStructures(ItemsStructuresListVM itemsStructuresListVM, int itemId)
        {
            var assignToStructures = _itemService.AssignItemToStructures(itemsStructuresListVM, "testUserId");

            return RedirectToAction("ItemDetails", new { id = assignToStructures });
        }
    }
}
