using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskApi.Context;
using TaskApi.Models;

namespace TaskApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly TaskContext _context;

        public TaskController(TaskContext context)
        {
            _context = context;
        }

        //  Obtenter tareas Get
        [HttpGet("Get")]
        public async Task<ActionResult<IEnumerable<TaskItem>>> GetTodoItems()
        {
            return await _context.TaskItems.ToListAsync();
        }

        // Obtener tarea por Id
        [HttpGet("Get/{id}")]
        public async Task<ActionResult<TaskItem>> GetTaskItem(int id)
        {
            var taskItem = await _context.TaskItems.FindAsync(id);

            if (taskItem == null)
            {
                return NotFound();
            }
            return taskItem;
        }

        // Post Crear nueva tarea
        [HttpPost("Create")]
        public async Task<ActionResult<TaskItem>> PostTaskItem([FromBody] TaskItem taskItem)
        {
            _context.TaskItems.Add(taskItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTaskItem), new { id = taskItem.Id }, taskItem);
        }

        // Put actualizacion de tareas
        [HttpPut("Update/{id}")]
        public async Task<ActionResult<TaskItem>> PutTaskItem(int id, [FromBody] TaskItem taskItem)
        {
            if (id != taskItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(taskItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        // Delet eliminar
        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<TaskItem>> DeleteTaskItem(int id)
        {
            var taskItem = await _context.TaskItems.FindAsync(id);

            if (taskItem == null)
            {
                return NotFound();
            }

            _context.TaskItems.Remove(taskItem);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TaskItemExists(int id)
        {
            return _context.TaskItems.Any(e => e.Id == id);
        }
    }
}
