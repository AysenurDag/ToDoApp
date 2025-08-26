using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using todo_app.Data;
using todo_app.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;


namespace todo_app.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodosController : ControllerBase
    {
        private readonly AppDbContext _context;
        public TodosController(AppDbContext context) => _context = context;

        // GET api/todos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodos()
            => await _context.Todos.AsNoTracking().ToListAsync();

        // GET api/todos/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<TodoItem>> GetTodo(int id)
        {
            var todo = await _context.Todos.FindAsync(id);
            return todo is null ? NotFound() : todo;
        }

        // POST api/todos
        // Neden CreatedAtAction? -> REST'te kaynak oluşturunca 201 + Location header döneriz.
        [HttpPost]
        public async Task<ActionResult<TodoItem>> CreateTodo([FromBody] TodoItem todo)
        {
            todo.Id = 0;                           // client id gönderse bile sıfırla
            todo.CreatedAt = DateTime.UtcNow;      // server tarafında set et
            _context.Todos.Add(todo);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTodo), new { id = todo.Id }, todo);
        }


        // PUT api/todos/5
        // Neden NoContent? -> Başarılı güncellemenin gövdesiz 204 döndürmesi yaygın pratik.
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateTodo(int id, TodoItem todo)
        {
            if (id != todo.Id) return BadRequest();
            _context.Entry(todo).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE api/todos/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo is null) return NotFound();
            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
