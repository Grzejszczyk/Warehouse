using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warehouse.Application.Interfaces;

namespace Warehouse.Web.Controllers
{
    public class CheckInOutController : Controller
    {
        private readonly ILogger<ItemController> _logger;
        private readonly ICheckInOutService _checkInOutService;
        private int pageSizeStd;
        public CheckInOutController(ILogger<ItemController> logger, ICheckInOutService checkInOutRepo)
        {
            _logger = logger;
            _checkInOutService = checkInOutRepo;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
