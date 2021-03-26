using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warehouse.Application.Interfaces;
using Warehouse.Application.ViewModels.Structure;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Warehouse.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StructureController : ControllerBase
    {
        private readonly IStructureService _structureService;
        public StructureController(IStructureService structureService)
        {
            _structureService = structureService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetStructures(int pageSize = 25, int pageNo = 1, string searchString = "")
        {
            var result = _structureService.StructuresList(pageSize, pageNo, searchString);
            return Ok(result.Structures.ToArray());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetDetails(int id)
        {
            var result = _structureService.GetStructureDetails(id);
            return Ok(result);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Create([FromBody] StructureDetailsVM model)
        {
            if (ModelState.IsValid)
            {
                string user = User.Identity.Name;
                int newStructureId = _structureService.AddStructure(model, user);
                return CreatedAtAction(nameof(GetDetails), new { id = newStructureId }, model);
            }
            return BadRequest();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Edit(int id, [FromBody] StructureDetailsVM model)
        {
            if (ModelState.IsValid)
            {
                string user = User.Identity.Name;
                model.Id = id;
                int newStructureId = _structureService.EditStructure(model, user);
                return CreatedAtAction(nameof(GetDetails), new { id = newStructureId }, model);
            }
            return BadRequest();
        }
    }
}
