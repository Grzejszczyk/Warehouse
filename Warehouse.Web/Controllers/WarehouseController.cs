﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Warehouse.Application.Interfaces;
using Warehouse.Application.ViewModels.Item;
using Warehouse.Application.ViewModels.Supplier;

namespace Warehouse.Web.Controllers
{
    public class WarehouseController : Controller
    {
        private readonly ILogger<WarehouseController> _logger;
        private readonly IItemService _itemService;
        private readonly ISupplierService _supplierService;
        private int pageSizeStd;

        public WarehouseController(ILogger<WarehouseController> logger, IItemService itemRepo, ISupplierService supplierRepo)
        {
            _logger = logger;
            _itemService = itemRepo;
            _supplierService = supplierRepo;
        }

        #region Items

        [HttpGet]
        public IActionResult ItemsList(int pageSize = 5, int pageNo = 1)
        {
            _logger.LogInformation("Jestem w Warehousecontroller - to logger.");
            pageSizeStd = pageSize;
            var model = _itemService.GetAllItemsForList(pageSizeStd, pageNo, "");
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ItemsList(int pageSize = 5, int pageNo = 1, string searchString = "")
        {
            pageSizeStd = pageSize;
            if (searchString is null)
            {
                searchString = String.Empty;
            }
            var model = _itemService.GetAllItemsForList(pageSizeStd, pageNo, searchString);
            return View(model);
        }
        [HttpGet]
        public IActionResult EditItem(int id = 0)
        {
            if (id != 0)
            {
                var item = _itemService.GetItemForEdit(id);
                return View(item);
            }
            else
            {
                return View(new NewItemVM());
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditItem(NewItemVM model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {
                    var id = _itemService.AddNewItem(model);
                } else
                {
                    var id = _itemService.EditItem(model);
                }
                return RedirectToAction("ItemsList");
            }
            return View(model);
        }
        public IActionResult ItemDetails(int id)
        {
            var model = _itemService.GetItemDetails(id);
            return View(model);
        }
        public IActionResult DeleteItem(int id)
        {
            _itemService.DeleteItem(id);
            return RedirectToAction("ItemsList");
        }

        #endregion

        #region Supplier

        [HttpGet]
        public IActionResult SuppliersList(int pageSize = 5, int pageNo = 1)
        {
            pageSizeStd = pageSize;
            var model = _supplierService.GetAllSuppliersForList(pageSizeStd, pageNo, "");
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuppliersList(int pageSize = 5, int pageNo = 1, string searchString = "")
        {
            pageSizeStd = pageSize;
            if (searchString is null)
            {
                searchString = String.Empty;
            }
            var model = _supplierService.GetAllSuppliersForList(pageSizeStd, pageNo, searchString);
            return View(model);
        }
        [HttpGet]
        public IActionResult AddSupplier()
        {
            return View(new NewSupplierVM());
        }
        [HttpPost]
        public IActionResult AddSupplier(NewSupplierVM model)
        {
            if (ModelState.IsValid)
            {
                var id = _supplierService.AddNewSupplier(model);
                return RedirectToAction("SuppliersList");
            }
            return View(model);
        }

        #endregion
    }
}
