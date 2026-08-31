using TaskManager.DTOs;

namespace TaskManager.Services
{
    public interface iTaskService
    {
        Task<IEnumerable<TaskResponseDto>> GetAllTasksAsync();

        Task<TaskResponseDto> GetTaskByIdAsync(int id);

        Task<TaskResponseDto> CreateTaskAsync(CreateTaskDto dto);

        Task<bool> UpdateTaskAsync(int id, UpdateTaskDto dto);

        Task<bool> DeleteTaskAsync(int id);


    }
}
