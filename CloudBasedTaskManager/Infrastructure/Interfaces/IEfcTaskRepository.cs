using Core.DTOs;
using Core.IBase;
using System.Data.Entity;

namespace Infrastructure.Interfaces
{
    public interface IEfcTaskRepository : IBaseRepository<TaskDTO>
    {
        //Additional Methods Can be specified
    }

    public class ApplicationDbContext : DbContext
    {
        /*public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        */

        // Define your DbSets here
        public DbSet<TaskDTO> Tasks { get; set; }

        // You can override OnModelCreating if you need to customize the model creation
        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // You can configure entity properties and relationships here
            modelBuilder.Entity<TaskDTO>(entity =>
            {
                entity.HasKey(e => e.Id); // Assuming Id is the primary key
                entity.Property(e => e.Title).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(500);
                // Configure other properties as needed
            });
        }
        */
    }
}
