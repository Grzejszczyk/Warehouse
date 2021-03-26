using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warehouse.Application.Interfaces;
using Warehouse.Application.ViewModels.Item;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Warehouse.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;
        public ItemController(IItemService itemRepo)
        {
            _itemService = itemRepo;
        }
 
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetItems(int pageSize = 25, int pageNo = 1, string searchString = "")
        {
            var result = _itemService.GetAllItemsForList(pageSize, pageNo, searchString);
            return Ok(result.Items.ToArray());
        }

        [HttpGet("itemById/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetDetails(int id)
        {
            var result = _itemService.GetItemDetails(id);
            if (result == null)
            {
                return NotFound("Sorry, item with this id does not exist");
            }
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Create([FromBody] EditItemVM model)
        {
            if (ModelState.IsValid)
            {
                string user = User.Identity.Name;
                int newItemId = _itemService.AddItem(model, user);
                return CreatedAtAction(nameof(GetDetails), new { id = newItemId }, model);
            }
            return BadRequest();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Edit(int id, [FromBody] EditItemVM model)
        {
            if (ModelState.IsValid)
            {
                string user = User.Identity.Name;
                model.Id = id;
                int newItemId = _itemService.EditItem(model, user);
                return CreatedAtAction(nameof(GetDetails), new { id = newItemId }, model);
            }
            return BadRequest();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult DeleteItem(int id)
        {
            string user = User.Identity.Name;
            _itemService.SetIsDeleted(id, user);
            return NoContent();
        }

        [HttpGet("structure/{structureId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetItemsByStructure(int structureid, int pageSize = 25, int pageNo = 1, string searchString = "")
        {
            var result = _itemService.GetItemsByStructure(structureid, pageSize, pageNo, searchString);
            if (result.ItemStructures.Any())
            {
                return Ok(result.ItemStructures.ToArray());
            }
            return BadRequest();
        }

        [HttpGet("supplier/{supplierId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetItemsBySupplier(int supplierId, int pageSize = 25, int pageNo = 1, string searchString = "")
        {
            var result = _itemService.GetItemsBySupplier(supplierId, pageSize, pageNo, searchString);
            if (result.Items.Any())
            {
                return Ok(result.Items.ToArray());
            }
            return BadRequest();
        }
    }
}
