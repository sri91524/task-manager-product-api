using Microsoft.EntityFrameworkCore;
using TaskManager.Models;
using TaskManager.Data;

namespace TaskManager.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<TaskItem> Tasks { get; set; }
    }
}
