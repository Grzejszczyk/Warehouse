using System;
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
                var item = _itemService.GetItemDetails(id);
                return View(item);
            }
            else
            {
                return View(new ItemDetailsVM());
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditItem(ItemDetailsVM model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {
                    var id = _itemService.AddItem(model);
                } else
                {
                    model.CreatedById = _itemService.GetItemDetails(model.Id).CreatedById;
                    model.CreatedDateTime = _itemService.GetItemDetails(model.Id).CreatedDateTime;
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
        public IActionResult EditSupplier(int id = 0)
        {
            if (id != 0)
            {
                var supplier = _supplierService.GetSupplierDetails(id);
                return View(supplier);
            }
            else
            {
                return View(new SupplierDetailsVM());
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditSupplier(SupplierDetailsVM model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {
                    var id = _supplierService.AddSupplier(model);
                }
                else
                {
                    var id = _supplierService.EditSupplier(model);
                }
                return RedirectToAction("SuppliersList");
            }
            return View(model);
        }
        public IActionResult SupplierDetails(int id)
        {
            var model = _supplierService.GetSupplierDetails(id);
            return View(model);
        }
        public IActionResult DeleteSupplier(int id)
        {
            _supplierService.DeleteSupplier(id);
            return RedirectToAction("SuppliersList");

            //TODO: Supplier cannot be deleted if his items are in DB.d
        }
        #endregion
    }
}
