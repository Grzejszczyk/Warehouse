using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warehouse.Application.Interfaces;
using Warehouse.Application.ViewModels.Supplier;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Warehouse.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _supplierService;
        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetSuppliers(int pageSize = 25, int pageNo = 1, string searchString = "")
        {
            var result = _supplierService.GetSuppliers(pageSize, pageNo, searchString);
            return Ok(result.Suppliers.ToArray());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetDetails(int id)
        {
            var result = _supplierService.GetSupplierDetails(id);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Create([FromBody] SupplierDetailsVM model)
        {
            if (ModelState.IsValid)
            {
                string user = User.Identity.Name;
                int newSupplierId = _supplierService.AddSupplier(model, user);
                return CreatedAtAction(nameof(GetDetails), new { id = newSupplierId }, model);
            }
            return BadRequest();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Edit(int id, [FromBody] SupplierDetailsVM model)
        {
            if (ModelState.IsValid)
            {
                string user = User.Identity.Name;
                model.Id = id;
                int newSupplierId = _supplierService.EditSupplier(model, user);
                return CreatedAtAction(nameof(GetDetails), new { id = newSupplierId }, model);
            }
            return BadRequest();
        }
    }
}
