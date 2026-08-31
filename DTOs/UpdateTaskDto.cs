using System.ComponentModel.DataAnnotations;
namespace TaskManager.DTOs
{
    public class UpdateTaskDto
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        [Required]
        public string Status { get; set; } = "Pending";

        public DateTime? DueDate { get; set; }

    }
}
