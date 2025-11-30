
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoManagerAPI.Data;
using ToDoManagerAPI.Models;    

namespace ToDoManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoItemsController : ControllerBase
    {
        private readonly ToDoDbContext _context;
        public ToDoItemsController(ToDoDbContext context)
        {
            _context = context;
        }

        // Action methods for CRUD operations go here
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDoItem>>> GetToDoItems()
        {
            return await _context.ToDoItems.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoItem>> GetToDoItem(long id)
        {
            var toDoItem = await _context.ToDoItems.FindAsync(id);

            if (toDoItem == null)
            {
                return NotFound();
            }

            return toDoItem;
        }

        [HttpPost]
        public async Task<ActionResult<ToDoItem>> CreateToDoItem(ToDoItem toDoItem)
        {
            // Assign Id = (current max Id) + 1 to ensure a unique sequential Id
            // Use DefaultIfEmpty(0) so MaxAsync returns 0 when there are no rows yet.
            var maxId = (await _context.ToDoItems
                .OrderByDescending(i => i.Id)
                .Select(i => (long?)i.Id)
                .FirstOrDefaultAsync()) ?? 0L;

            toDoItem.Id = maxId + 1;

            _context.ToDoItems.Add(toDoItem);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetToDoItem), new { id = toDoItem.Id }, toDoItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateToDoItem(long id, ToDoItem toDoItem)
        {
            if (id != toDoItem.Id)
            {
                return BadRequest();
            }
            _context.Entry(toDoItem).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteToDoItem(long id)
        {
            var toDoItem = await _context.ToDoItems.FindAsync(id);
            if (toDoItem == null)
            {
                return NotFound();
            }
            _context.ToDoItems.Remove(toDoItem);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool ToDoItemExists(int id)
        {
            return _context.ToDoItems.Any(e => e.Id == id);
        }


    }
}
