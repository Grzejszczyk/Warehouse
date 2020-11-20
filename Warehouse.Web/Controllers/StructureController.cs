using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warehouse.Application.Interfaces;
using Warehouse.Application.ViewModels.Structure;

namespace Warehouse.Web.Controllers
{
    public class StructureController : Controller
    {
        private readonly ILogger<ItemController> _logger;
        private readonly IStructureService _structureService;
        private int pageSizeStd;
        public StructureController(ILogger<ItemController> logger, IStructureService structureRepo)
        {
            _logger = logger;
            _structureService = structureRepo;
        }

        [HttpGet]
        public IActionResult StructuresList(int pageSize = 5, int pageNo = 1)
        {
            pageSizeStd = pageSize;
            var model = _structureService.StructuresList(pageSizeStd, pageNo, "");
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult StructuresList(int pageSize = 5, int pageNo = 1, string searchString = "")
        {
            pageSizeStd = pageSize;
            if (searchString is null)
            {
                searchString = String.Empty;
            }
            var model = _structureService.StructuresList(pageSizeStd, pageNo, searchString);
            return View(model);
        }
        [HttpGet]
        public IActionResult EditStructure(int id = 0)
        {
            if (id != 0)
            {
                var structure = _structureService.GetStructureDetails(id);
                return View(structure);
            }
            else
            {
                return View(new StructureDetailsVM());
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditStructure(StructureDetailsVM model)
        {
            if (ModelState.IsValid)
            {
                if (model.StructureId == 0)
                {
                    var id = _structureService.AddStructure(model);
                }
                else
                {
                    var id = _structureService.EditStructure(model);
                }
                return RedirectToAction("StructureList");
            }
            return View(model);
        }
        public IActionResult StructureDetails(int id)
        {
            var model = _structureService.GetStructureDetails(id);
            return View(model);
        }

        public IActionResult SetIsDeletedStructure(int id)
        {
            _structureService.SetIsDeleted(id);
            return RedirectToAction("StructureList");
        }
        public IActionResult DeleteSupplier(int id)
        {
            _structureService.DeleteStructure(id);
            return RedirectToAction("StructureList");

            //TODO: Structure cannot be deleted if his items are in DB.
        }
    }
}
