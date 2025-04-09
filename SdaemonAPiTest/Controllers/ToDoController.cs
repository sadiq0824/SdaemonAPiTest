using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SdaemonAPiTest.Models;

namespace SdaemonAPiTest.Controllers
{
    [Route("api/[controller]")] // Sets the route prefix to api/ToDo
    [ApiController] // Enables automatic model validation and API-specific behaviors
    public class ToDoController : ControllerBase
    {
        private readonly SdaemonTestContext _db; // EF Core DbContext for accessing the database

        // Constructor injection of the DbContext
        public ToDoController(SdaemonTestContext db)
        {
            _db = db;
        }

        // GET: api/ToDo
        // Retrieves all ToDo items from the database
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDo>>> GetTodo()
        {
            return await _db.ToDos.ToListAsync(); // Fetches all ToDos from DB
        }

        // GET: api/ToDo/5
        // Retrieves a single ToDo item by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<ToDo>> GetById(int id)
        {
            var item = await _db.ToDos.FindAsync(id); // Look up item by ID

            if (item == null)
                return NotFound(); // Return 404 if not found

            return Ok(item); // Return 200 with item
        }

        // POST: api/ToDo
        // Adds a new ToDo item to the database
        [HttpPost]
        public async Task<ActionResult<ToDo>> Create(ToDo t)
        {
            await _db.ToDos.AddAsync(t); // Add new item to DbContext
            await _db.SaveChangesAsync(); // Save changes to DB

            // Return 201 Created with location header and new item
            return CreatedAtAction(nameof(GetById), new { id = t.Id }, t);
        }

        // PUT: api/ToDo/5
        // Updates an existing ToDo item
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProd(int id, ToDo t)
        {
            if (id != t.Id)
                return BadRequest(); // ID mismatch, return 400

            _db.Entry(t).State = EntityState.Modified; // Mark entity as modified

            // Note: Only works if the full object is passed, otherwise properties should be updated manually

            await _db.SaveChangesAsync(); // Save changes to DB

            return NoContent(); // Return 204 No Content (standard for successful PUT)
        }

        // DELETE: api/ToDo/5
        // Deletes a ToDo item by ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DelProd(int id)
        {
            var task = await _db.ToDos.FindAsync(id); // Find task by ID

            if (task == null)
                return NotFound(); // Return 404 if not found

            _db.ToDos.Remove(task); // Remove from DbContext
            await _db.SaveChangesAsync(); // Commit delete

            return NoContent(); // Return 204 No Content
        }
    }
}
