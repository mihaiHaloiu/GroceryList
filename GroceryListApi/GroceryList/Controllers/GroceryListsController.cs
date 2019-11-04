using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroceryList.Bll.Models;
using GroceryList.Bll.Models.Reponses;
using GroceryList.Bll.Models.Requests;
using GroceryList.Bll.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GroceryList.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class GroceryListsController : ControllerBase
    {
        private readonly IGroceryListService _groceryListService;

        public GroceryListsController(IGroceryListService groceryListService)
        {
            _groceryListService = groceryListService;
        }

        [HttpGet("{ID}")]
        public async Task<IActionResult> Get(string ID)
        {
            var groceryList = await _groceryListService.GetGroceryListByID(ID);

            return Ok(groceryList);
        }


        [HttpGet]        
        public async Task<IActionResult> GetAll()
        {
            var groceryLists = await _groceryListService.GetAllGroceryLists();

            return Ok(groceryLists);
        }

        [HttpPost]
        public async Task<MessageResponse> AddGroceryList([FromBody]GroceryListRequest groceryListRequest)
        {
            await _groceryListService.AddGroceryList(groceryListRequest);

            return new MessageResponse { Message = "Grocery list added" };
        }

        [HttpPatch("{ID}/AddItem")]
        public async Task<MessageResponse> AddItem(string ID,[FromBody]string item)
        {
            await _groceryListService.GroceryListAddItem(ID, item);

            return new MessageResponse { Message = "Item added" };
        }

        [HttpPatch("{ID}/RemoveItem")]
        public async Task<MessageResponse> RemoveItem(string ID, [FromBody]string item)
        {
            await _groceryListService.GroceryListRemovedItem(ID, item);

            return new MessageResponse { Message="Item removed"};
        }

        [HttpDelete("{ID}")]
        public async Task<MessageResponse> Delete(string ID)
        {
            await _groceryListService.DeleteGroceryList(ID);

            return new MessageResponse { Message = "Grocery list deleted" };
        }
    }
}