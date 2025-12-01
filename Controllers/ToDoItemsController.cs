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

        // GET: api/ToDoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDoItem>>> GetToDoItems()
        {
            return Ok(await _context.ToDoItems.AsNoTracking().ToListAsync());
        }

        // GET: api/ToDoItems/5
        [HttpGet("{id:long}")]
        public async Task<ActionResult<ToDoItem>> GetToDoItem(long id)
        {
            var toDoItem = await _context.ToDoItems.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
            return toDoItem is null ? NotFound() : Ok(toDoItem);
        }

        // POST: api/ToDoItems
        [HttpPost]
        public async Task<ActionResult<ToDoItem>> CreateToDoItem([FromBody] ToDoItem toDoItem)
        {
            if (toDoItem is null)
                return BadRequest();

            // Assign Id = (current max Id) + 1 to ensure a unique sequential Id
            // Use DefaultIfEmpty(0) so MaxAsync returns 0 when there are no rows yet.
            var maxId = (await _context.ToDoItems
                .OrderByDescending(i => i.Id)
                .Select(i => (long?)i.Id)
                .FirstOrDefaultAsync()) ?? 0L;

            toDoItem.Id = maxId + 1;

            // Let the database handle Id assignment if possible (recommended)
            _context.ToDoItems.Add(toDoItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetToDoItem), new { id = toDoItem.Id }, toDoItem);
        }

        // PUT: api/ToDoItems/5
        [HttpPut("{id:long}")]
        public async Task<IActionResult> UpdateToDoItem(long id, [FromBody] ToDoItem toDoItem)
        {
            if (toDoItem is null || id != toDoItem.Id)
                return BadRequest();

            if (!await _context.ToDoItems.AnyAsync(e => e.Id == id))
                return NotFound();

            _context.Entry(toDoItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.ToDoItems.AnyAsync(e => e.Id == id))
                    return NotFound();
                throw;
            }

            return new OkResult();
        }

        // DELETE: api/ToDoItems/5
        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteToDoItem(long id)
        {
            var toDoItem = await _context.ToDoItems.FindAsync(id);
            if (toDoItem is null)
                return NotFound();

            _context.ToDoItems.Remove(toDoItem);
            await _context.SaveChangesAsync();

            return new OkResult();
        }
    }
}
