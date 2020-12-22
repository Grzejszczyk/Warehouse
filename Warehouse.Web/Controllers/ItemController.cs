using System;
using System.Collections.Generic;
using System.IO;
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
        private int pageSizeStd;

        public ItemController(ILogger<ItemController> logger, IItemService itemService, ISupplierService supplierService)
        {
            _logger = logger;
            _itemService = itemService;
        }

        [Authorize(Roles = "Admin, SuperUser, User, Operator, Viewer")]
        [HttpGet]
        public IActionResult ItemsList(int pageSize = 5, int pageNo = 1)
        {
            //TODO: Implement Logger.
            _logger.LogInformation("Jestem w Warehousecontroller ItemsList - to logger.");
            pageSizeStd = pageSize;
            var model = _itemService.GetAllItemsForList(pageSizeStd, pageNo, "");
            return View(model);
        }

        [Authorize(Roles = "Admin, SuperUser, User, Operator, Viewer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ItemsList(int pageSize = 5, int pageNo = 1, string searchString = "")
        {
            pageSizeStd = pageSize;
            if (searchString is null) { searchString = String.Empty; }
            var model = _itemService.GetAllItemsForList(pageSizeStd, pageNo, searchString);
            return View(model);
        }

        [Authorize(Roles = "Admin, SuperUser, User, Operator, Viewer")]
        [HttpGet]
        public IActionResult ItemsListFromSupplier(int supplierId, int pageSize = 7, int pageNo = 1)
        {
            pageSizeStd = pageSize;
            var model = _itemService.GetItemsBySupplier(supplierId, pageSizeStd, pageNo, "");
            return View("ItemsList", model);
        }

        [Authorize(Roles = "Admin, SuperUser, User, Operator, Viewer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ItemsListFromSupplier(int supplierId, int pageSize = 7, int pageNo = 1, string searchString = "")
        {
            pageSizeStd = pageSize;
            if (searchString is null) { searchString = String.Empty; }
            var model = _itemService.GetItemsBySupplier(supplierId, pageSizeStd, pageNo, searchString);
            return View("ItemsList", model);
        }

        [Authorize(Roles = "Admin, SuperUser, User, Operator, Viewer")]
        [HttpGet]
        public IActionResult ItemsListFromStructure(int structureId, int pageSize = 7, int pageNo = 1)
        {
            pageSizeStd = pageSize;
            var model = _itemService.GetItemsByStructure(structureId, pageSizeStd, pageNo, "");
            return View("ItemsList", model);
        }

        [Authorize(Roles = "Admin, SuperUser, User, Operator, Viewer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ItemsListFromStructure(int structureId, int pageSize = 7, int pageNo = 1, string searchString = "")
        {
            pageSizeStd = pageSize;
            if (searchString is null) { searchString = String.Empty; }
            var model = _itemService.GetItemsByStructure(structureId, pageSizeStd, pageNo, searchString);
            return View("ItemsList", model);
        }

        [Authorize(Roles = "Admin, SuperUser, User, Operator, Viewer")]
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

        [Authorize(Roles = "Admin, SuperUser, User, Operator, Viewer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditItem(EditItemVM model)
        {
            if (model.ImageFormFile != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    model.ImageFormFile.CopyTo(memoryStream);

                    // Upload the file if less than 2 MB
                    if (memoryStream.Length < 2097152)
                    {
                        var content = memoryStream.ToArray();
                        model.ImageFile = content;
                    }
                    else
                    {
                        ModelState.AddModelError("File", "The file is too large.");
                    }
                }
            }

            int newItemId = 0;
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {
                    newItemId = _itemService.AddItem(model, User.Identity.Name);
                }
                else
                {
                    newItemId = _itemService.EditItem(model, User.Identity.Name);
                }
                return RedirectToAction("ItemDetails", new { id = newItemId });
            }
            return View(model);
        }

        [Authorize(Roles = "Admin, SuperUser, User, Operator, Viewer")]
        [HttpGet]
        public IActionResult ItemDetails(int id)
        {
            var model = _itemService.GetItemDetails(id);
            if (model.ImageFile.Length > 0)
            {
                string imreBase64Data = Convert.ToBase64String(model.ImageFile);
                string imgDataURL = string.Format("data:image/png;base64,{0}", imreBase64Data);
                ViewBag.ImageData = imgDataURL;
            }
            else
            {
                ViewBag.ImageData = "noImage";
            }

            return View(model);
        }

        [Authorize(Roles = "Admin, SuperUser, User, Operator, Viewer")]
        public IActionResult SetIsDeletedItem(int id)
        {

            _itemService.SetIsDeleted(id, User.Identity.Name);
            return RedirectToAction("ItemsList");
        }

        [Authorize(Roles = "Admin, SuperUser, User, Operator, Viewer")]
        [HttpGet]
        public IActionResult AssignItemToSupplier(int itemId)
        {
            var itemSuppliers = _itemService.GetItemForSuppliersList(itemId);

            return View(itemSuppliers);
        }

        [Authorize(Roles = "Admin, SuperUser, User, Operator, Viewer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AssignItemToSupplier(int itemId, int supplierId)
        {
            var assignedItem = _itemService.AssignItemToSupplier(
                itemId,
                supplierId,
                User.Identity.Name);

            return RedirectToAction("ItemDetails", new { id = assignedItem });
        }

        [Authorize(Roles = "Admin, SuperUser, User, Operator, Viewer")]
        [HttpGet]
        public IActionResult AssignItemToStructures(int itemId)
        {
            var itemsStructuresListVM = _itemService.GetItemStructuresForAssign(itemId);
            return View(itemsStructuresListVM);
        }

        [Authorize(Roles = "Admin, SuperUser, User, Operator, Viewer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AssignItemToStructures(ItemsStructuresListVM itemsStructuresListVM, int itemId)
        {
            var assignToStructures = _itemService.AssignItemToStructures(itemsStructuresListVM, User.Identity.Name);

            return RedirectToAction("ItemDetails", new { id = assignToStructures });
        }
    }
}
