using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Web.Data;

namespace Warehouse.Web.Controllers
{
    public class WarehouseController : Controller
    {
        private IItemRepository itemRepository;
        public WarehouseController(IItemRepository repo)
        {
            itemRepository = repo;
        }
        [Route("Warehouse")]
        public IActionResult Index()
        {
            return View(itemRepository.Items);
        }
        public ViewResult List() => View(itemRepository.Items);
    }
}
