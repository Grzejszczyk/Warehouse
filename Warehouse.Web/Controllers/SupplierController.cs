using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warehouse.Application.Interfaces;
using Warehouse.Application.ViewModels.Supplier;

namespace Warehouse.Web.Controllers
{
    public class SupplierController : Controller
    {
        private readonly ILogger<ItemController> _logger;
        private readonly ISupplierService _supplierService;
        private int pageSizeStd;

        public SupplierController(ILogger<ItemController> logger, ISupplierService supplierRepo)
        {
            _logger = logger;
            _supplierService = supplierRepo;
        }

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

        public IActionResult SetIsDeletedSupplier(int id)
        {
            _supplierService.SetIsDeleted(id);
            return RedirectToAction("SuppliersList");
        }
        public IActionResult DeleteSupplier(int id)
        {
            _supplierService.DeleteSupplier(id);
            return RedirectToAction("SuppliersList");

            //TODO: Supplier cannot be deleted if his items are in DB.d
        }
    }
}
