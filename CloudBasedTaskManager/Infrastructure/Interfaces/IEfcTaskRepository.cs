﻿using Core.DTOs;
using Core.IBase;
using Microsoft.EntityFrameworkCore;

..
namespace Infrastructure.Interfaces
{
    public interface IEfcTaskRepository : IBaseRepository<TaskDTO>
    {
        //Additional Methods Can be specified
    }

    public class AppDbContext : DbContext
    { 
        public DbSet<TaskDTO> Tasks { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Replace with your actual SQL Server connection string
                optionsBuilder.UseSqlServer("Server=LAPTOP-DKP990JS;Database=CloudBasedTaskManagement;User Id=YourUsername;Password=YourPassword;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskDTO>(entity =>
            {
                entity.HasKey(e => e.Id); 

                entity.Property(e => e.Title)
                      .IsRequired()
                      .HasMaxLength(100); 

                entity.Property(e => e.Description)
                      .HasMaxLength(500); 

                entity.Property(e => e.DueDate)
                      .IsRequired();

                entity.Property(e => e.Priority)
                      .IsRequired(); 

                entity.Property(e => e.Status)
                      .IsRequired();
            });
        }
    }
}
