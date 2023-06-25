using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Play.Catalog.Service.DataTransferObjects;
using Play.Catalog.Service.Repositories;
using Play.Catalog.Service.Entities;

namespace Play.Catalog.Service.Controllers
{
    // https://localhost:5001/items 
    [ApiController]
    [Route("items")]
    public class ItemController : ControllerBase
    {
        private readonly ItemsRepository itemsRepository = new();

        [HttpGet]
        public async Task<IEnumerable<ItemDataTransferObject>> GetAsync()
        {
            var items = (await itemsRepository.GetAllAsync()).Select(item => item.AsDataTransferObject());
            return items;
        }

        // e.g. GET /items/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDataTransferObject>> GetByIdAsync(Guid id)
        {
            var item = await itemsRepository.GetAsync(id);

            if (item == null)
            {
                return NotFound();
            }
            return item.AsDataTransferObject();
        }

        // Create Action to ADD ITEMS
        [HttpPost]
        public async Task<ActionResult<ItemDataTransferObject>> PostAsync(CreateItemDataTransferObject createItem)
        {
            var item = new Item
            {
                Name = createItem.Name,
                Description = createItem.Description,
                Price = createItem.Price,
                CreateDate = DateTimeOffset.UtcNow
            };

            await itemsRepository.CreateAsync(item);

            return CreatedAtAction(nameof(GetByIdAsync), new { id = item.Id }, item);
        }

        // UPDATE ITEMS
        [HttpPut("item")]
        public async Task<IActionResult> PutAsync(Guid id, UpdateItemDataTransferObject updateItem)
        {
            var existingItem = await itemsRepository.GetAsync(id);

            if (existingItem == null)
            {
                return NotFound();
            }

            existingItem.Name = updateItem.Name;
            existingItem.Description = updateItem.Description;
            existingItem.Price = updateItem.Price;

            await itemsRepository.UpdateAsync(existingItem);

            return NoContent();
        }

        // DELETE ITEMS
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var item = await itemsRepository.GetAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            await itemsRepository.RemoveAsync(item.Id);

            return NoContent();
        }
    }
}