using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
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
            int supplierId = 0;
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {
                    supplierId = _supplierService.AddSupplier(model, "testUserId");
                }
                else
                {
                    supplierId = _supplierService.EditSupplier(model, "testUserId");
                }
                return RedirectToAction("SupplierDetails", new { id = supplierId });
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
            _supplierService.SetIsDeleted(id, "testUserId");
            return RedirectToAction("SuppliersList");
        }
    }
}
