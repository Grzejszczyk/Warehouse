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
    public class CheckInController : ControllerBase
    {
        private readonly ICheckInOutService _checkInService;
        public CheckInController(ICheckInOutService checkIn)
        {
            _checkInService = checkIn;
        }

        [HttpGet]
        public IActionResult GetCheckIns()
        {
            var checkIns = _checkInService.GetCheckIns(0);
            return Ok(checkIns);
        }

        [HttpGet("item/{itemId}")]
        public IActionResult GetCheckInsByItem(int itemId)
        {
            var checkIns = _checkInService.GetCheckIns(itemId);
            return Ok(checkIns);
        }

        [HttpPost]
        public IActionResult CheckInItem([FromBody]CheckInOutVMForCreate checkInOutVMForCreate)
        {
            if (checkInOutVMForCreate.Quantity > 0)
            {
                string user = User.Identity.Name;
                _checkInService.CheckIn(checkInOutVMForCreate, user);
                return CreatedAtAction(nameof(GetCheckIns), null);
            }
            return BadRequest("Quantity invalid value.");
        }
    }
}
