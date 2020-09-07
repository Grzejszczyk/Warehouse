using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Warehouse.Web.Models;

namespace Warehouse.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [Route("")]
        public IActionResult Index()
        {
            ViewBag.Message = "This is message from ViewBag (live only for one HTTP request).";
            ViewData["Message"] = "This is message from ViewData (live only for one HTTP request).";
            TempData["Message"] = "This is message from TempData (live as long as sesion).";
            return View();
        }
        [Route("{controller=Home}/{action=Redirect}")]
        public IActionResult Redirect()
        {
            TempData["FromRedirect"] = "Message From Redirect";
            return RedirectToAction("Privacy");
        }

        [Route("{controller=Home}/{action=Privacy}/{s?}")]
        [Route("Home/Privacy")]
        public IActionResult Privacy(string s)
        {
            ViewBag.S = s;
            ViewBag.Td = TempData["Message"];
            ViewBag.Fr = TempData["FromRedirect"];
            var td = TempData["FromRedirect"];
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
