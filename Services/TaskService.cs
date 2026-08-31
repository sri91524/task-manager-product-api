using System.Xml;
using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.DTOs;
using TaskManager.Models;

namespace TaskManager.Services
{
    public class TaskService: iTaskService
    {

        private readonly AppDbContext _context;
        private readonly ILogger<TaskService> _logger;

        public TaskService(AppDbContext context, ILogger<TaskService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<TaskResponseDto>> GetAllTasksAsync()
        {
            return await _context.Tasks
                .Select(task => new TaskResponseDto
                {
                    Id = task.Id,
                    Title = task.Title,
                    Description = task.Description,
                    Status = task.Status,
                    DueDate = task.DueDate,
                    CreatedAt = task.CreatedAt,
                    UpdatedAt = task.UpdatedAt
                }).ToListAsync();
        }

        public async Task<TaskResponseDto> GetTaskByIdAsync(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if(task == null)
            {
                return null;
            }

            return new TaskResponseDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status,
                DueDate = task.DueDate,
                CreatedAt = task.CreatedAt,
                UpdatedAt = task.UpdatedAt
            };
        }

        public async Task<TaskResponseDto> CreateTaskAsync(CreateTaskDto dto)
        {
            _logger.LogInformation("Creating new task {Title}", dto.Title);
            ValidateTask(dto.Status, dto.DueDate);
            var task = new TaskItem
            {
                Title = dto.Title,
                Description = dto.Description,
                Status = dto.Status,
                DueDate = dto.DueDate,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Task created successfully {Id}", task.Id);
            return new TaskResponseDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status,
                DueDate = task.DueDate,
                CreatedAt = task.CreatedAt,
                UpdatedAt = task.UpdatedAt
            };
        }

        public async Task<bool> UpdateTaskAsync(int id, UpdateTaskDto dto)
        {
            ValidateTask(dto.Status, dto.DueDate);
            var task = await _context.Tasks.FindAsync(id);
            if(task == null)
            {
                return false;
            }
            task.Title = dto.Title;
            task.Description = dto.Description;
            task.Status = dto.Status;
            task.DueDate = dto.DueDate;

            task.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if(task == null)
            {
                return false;
            }
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return true;
        }

        private void ValidateTask(string status, DateTime? duedate)
        {
            if(status != "Pending" && status != "InProgress" && status != "Completed")
            {
                throw new ArgumentException("Status must be Pending, InProgress or Completed.");
            }

            if(duedate.HasValue && duedate < DateTime.Now)
            {
                throw new ArgumentException("Due date cannot bein the past");
            }
        }


    }
}
