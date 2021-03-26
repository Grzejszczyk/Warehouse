using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warehouse.Application.Interfaces;
using Warehouse.Application.ViewModels.CheckInOut;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Warehouse.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckOutController : ControllerBase
    {
        //TODO: Implement CheckOutController

        private readonly ICheckInOutService _checkOutService;
        public CheckOutController(ICheckInOutService checkOut)
        {
            _checkOutService = checkOut;
        }

        [HttpGet]
        public IActionResult GetCheckOuts()
        {
            var checkIns = _checkOutService.GetCheckIns(0);
            return Ok(checkIns);
        }

        [HttpGet("item/{itemId}")]
        public IActionResult GetCheckOutsByItem(int itemId)
        {
            var checkIns = _checkOutService.GetCheckOuts(itemId);
            return Ok(checkIns);
        }

        [HttpPost]
        public IActionResult CheckOutItem([FromBody] CheckInOutVMForCreate checkInOutVMForCreate)
        {
            if (checkInOutVMForCreate.Quantity > 0)
            {
                string user = User.Identity.Name;
                _checkOutService.CheckOut(checkInOutVMForCreate, user);
                return Ok(checkInOutVMForCreate.ItemId);
            }
            return BadRequest("Quantity invalid value.");
        }
        [HttpPost("structure")]
        public IActionResult CheckOutItemsByStructure([FromForm] int structureId)
        {
            string user = User.Identity.Name;
            var structure = _checkOutService.CheckOutByStructure(structureId, user);

            if (structure != 0)
            {
                return Ok(structure);
            }
            return BadRequest("invalid structure id.");
        }
    }
}
