using Microsoft.AspNetCore.Mvc;
using TaskManager.DTOs;
using TaskManager.Services;

namespace TaskManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly iTaskService _taskservice;

        public TaskController(iTaskService taskService)
        {
            _taskservice = taskService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskResponseDto>>> GetTask()
        {
            var task= await _taskservice.GetAllTasksAsync();
       
            return Ok(task);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskResponseDto>> GetTask(int id)
        {
            var task = await _taskservice.GetTaskByIdAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        [HttpPost]
        public async Task<ActionResult<TaskResponseDto>> CreateTask(CreateTaskDto dto)
        {
            
            var task = await _taskservice.CreateTaskAsync(dto);
            return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, UpdateTaskDto dto)
        {        
          
            var updated = await _taskservice.UpdateTaskAsync(id, dto);
            if (!updated)
            {
                return NotFound();
            }
            return NoContent();
           
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var deleted = await _taskservice.DeleteTaskAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
           
            return NoContent();
        }



  
    }
 }

