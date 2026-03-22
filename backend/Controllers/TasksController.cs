using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Data;
using Backend.Models;
using Backend.DTOs;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TasksController(AppDbContext context)
        {
            _context = context;
        }

        // Uaktualniona metoda GetAll z mapowaniem na TaskReadDto
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskReadDto>>> GetTasks()
        {
            var tasks = await _context.Tasks
                .Select(t => new TaskReadDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    IsCompleted = t.IsCompleted
                })
                .ToListAsync();

            return Ok(tasks);
        }

        // Uaktualniona metoda GetById zwracająca DTO
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskReadDto>> GetTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            var taskDto = new TaskReadDto
            {
                Id = task.Id,
                Title = task.Title,
                IsCompleted = task.IsCompleted
            };

            return Ok(taskDto);
        }

        [HttpPost]
        public async Task<ActionResult<TaskReadDto>> PostTask(TaskCreateDto taskDto)
        {
            var task = new TaskItem
            {
                Title = taskDto.Title,
                IsCompleted = false
            };

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            var readDto = new TaskReadDto
            {
                Id = task.Id,
                Title = task.Title,
                IsCompleted = task.IsCompleted
            };

            return CreatedAtAction(nameof(GetTask), new { id = task.Id }, readDto);
        }
    }
}