using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warehouse.Application.Interfaces;
using Warehouse.Application.ViewModels.Structure;
using Warehouse.Web.Filters;

namespace Warehouse.Web.Controllers
{
    [Authorize]
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

        [CheckPermissions("ViewStructures")]
        [HttpGet]
        public IActionResult StructuresList(int pageSize = 5, int pageNo = 1)
        {
            pageSizeStd = pageSize;
            var model = _structureService.StructuresList(pageSizeStd, pageNo, "");
            return View(model);
        }

        [CheckPermissions("ViewStructures")]
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

        [Authorize(Policy = "CanManageStructures")]
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

        [Authorize(Policy = "CanManageStructures")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditStructure(StructureDetailsVM model)
        {
            int structureId = 0;
            if (ModelState.IsValid)
            {
                if (model.StructureId == 0)
                {
                    structureId = _structureService.AddStructure(model, "testUserId");
                }
                else
                {
                    structureId = _structureService.EditStructure(model, "testUserId");
                }
                return RedirectToAction("StructureDetails", new { id = structureId });
            }
            return View(model);
        }

        [CheckPermissions("ViewStructures")]
        public IActionResult StructureDetails(int id)
        {
            var model = _structureService.GetStructureDetails(id);
            return View(model);
        }

        [Authorize(Policy = "CanManageStructures")]
        public IActionResult SetIsDeletedStructure(int id)
        {
            _structureService.SetIsDeleted(id, "testUserId");
            return RedirectToAction("StructuresList");
        }
    }
}
