using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warehouse.Application.Interfaces;
using Warehouse.Application.ViewModels.Supplier;
using Warehouse.Web.Filters;

namespace Warehouse.Web.Controllers
{
    [Authorize]
    public class SupplierController : Controller
    {
        private readonly ILogger<SupplierController> _logger;
        private readonly ISupplierService _supplierService;
        private int pageSizeStd;

        public SupplierController(ILogger<SupplierController> logger, ISupplierService supplierService)
        {
            _logger = logger;
            _supplierService = supplierService;
        }

        [Authorize(Roles = "Admin, SuperUser, User, Operator, Viewer")]
        [HttpGet]
        public IActionResult SuppliersList(int pageSize = 5, int pageNo = 1)
        {
            pageSizeStd = pageSize;
            var model = _supplierService.GetAllSuppliers(pageSizeStd, pageNo, "");
            return View(model);
        }

        [Authorize(Roles = "Admin, SuperUser, User, Operator, Viewer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuppliersList(int pageSize = 5, int pageNo = 1, string searchString = "")
        {
            pageSizeStd = pageSize;
            if (searchString is null)
            {
                searchString = String.Empty;
            }
            var model = _supplierService.GetAllSuppliers(pageSizeStd, pageNo, searchString);
            return View(model);
        }

        [Authorize(Roles = "Admin, SuperUser, User, Operator, Viewer")]
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

        [Authorize(Roles = "Admin, SuperUser, User, Operator, Viewer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditSupplier(SupplierDetailsVM model)
        {
            int supplierId = 0;
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {
                    supplierId = _supplierService.AddSupplier(model, User.Identity.Name);
                }
                else
                {
                    supplierId = _supplierService.EditSupplier(model, User.Identity.Name);
                }
                return RedirectToAction("SupplierDetails", new { id = supplierId });
            }
            return View(model);
        }

        [Authorize(Roles = "Admin, SuperUser, User, Operator, Viewer")]
        public IActionResult SupplierDetails(int id)
        {
            var model = _supplierService.GetSupplierDetails(id);
            return View(model);
        }

        [Authorize(Roles = "Admin, SuperUser, User, Operator, Viewer")]
        public IActionResult SetIsDeletedSupplier(int id)
        {
            _supplierService.SetIsDeleted(id, User.Identity.Name);
            return RedirectToAction("SuppliersList");
        }
    }
}
