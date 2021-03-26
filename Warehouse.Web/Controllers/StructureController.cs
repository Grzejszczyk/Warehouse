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
        private readonly ILogger<StructureController> _logger;
        private readonly IStructureService _structureService;
        private int pageSizeStd;
        public StructureController(ILogger<StructureController> logger, IStructureService structureService)
        {
            _logger = logger;
            _structureService = structureService;
        }

        [Authorize(Roles = "Admin, SuperUser, User, Operator, Viewer")]
        [HttpGet]
        public IActionResult StructuresList(int pageSize = 5, int pageNo = 1)
        {
            pageSizeStd = pageSize;
            var model = _structureService.StructuresList(pageSizeStd, pageNo, "");
            return View(model);
        }

        [Authorize(Roles = "Admin, SuperUser, User, Operator, Viewer")]
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

        [Authorize(Roles = "Admin, SuperUser, User, Operator, Viewer")]
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

        [Authorize(Roles = "Admin, SuperUser, User, Operator, Viewer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditStructure(StructureDetailsVM model)
        {
            int structureId = 0;
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {
                    structureId = _structureService.AddStructure(model, User.Identity.Name);
                }
                else
                {
                    structureId = _structureService.EditStructure(model, User.Identity.Name);
                }
                return RedirectToAction("StructureDetails", new { id = structureId });
            }
            return View(model);
        }

        [Authorize(Roles = "Admin, SuperUser, User, Operator, Viewer")]
        public IActionResult StructureDetails(int id)
        {
            var model = _structureService.GetStructureDetails(id);
            return View(model);
        }

        [Authorize(Roles = "Admin, SuperUser, User, Operator, Viewer")]
        public IActionResult SetIsDeletedStructure(int id)
        {
            _structureService.SetIsDeleted(id, User.Identity.Name);
            return RedirectToAction("StructuresList");
        }
    }
}
