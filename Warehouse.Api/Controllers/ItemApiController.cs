using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class ItemApiController : ControllerBase
    {
        private readonly IItemService _itemService;
        public ItemApiController(IItemService itemRepo)
        {
            _itemService = itemRepo;
        }


        // GET: <ItemApiController>
        [HttpGet]
        //public IEnumerable<string> Get()
        public IActionResult Get()
        {
            var result = _itemService.GetAllItemsForList(100, 1, "");
            return Ok(result.Items.ToArray());
        }

        // GET <ItemApiController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = _itemService.GetItemDetails(id);
            return Ok(result);
        }

        // POST <ItemApiController>
        [HttpPost]
        public void Post([FromBody] EditItemVM model)
        {
            //Body for post method from postman:
            //            {
            //                "id": 0,
            //    "name": "Śruba M8x10 TEST API",
            //    "description": "DIN912 TEST API",
            //    "lowQuantityValue": 5,
            //    "supplierId": 1,
            //    "quantity": 7
            //}

            int newItemId = 0;
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {
                    newItemId = _itemService.AddItem(model, "testAPIUserId");
                }
                else
                {
                    newItemId = _itemService.EditItem(model, "testAPIUserId");
                }
            }
        }

        // PUT <ItemApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE <ItemApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
