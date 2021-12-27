using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReportsApp.DAL.Entities;
using ReportsApp.DAL.Enum;

namespace ReportsApp.DAL.Context
{
    public class ReportsDbContext : DbContext
    {
        public ReportsDbContext(DbContextOptions<ReportsDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<MyTask> Tasks { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Commentary> Commentaries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var dungeonMaster = new Employee()
            {
                FirstName = "Fredi",
                SecondName = "Cats",
                Id = Guid.NewGuid(),
            };
            
            modelBuilder.Entity<MyTask>().ToTable("Tasks");
            modelBuilder.Entity<Employee>().ToTable("Employees");
            modelBuilder.Entity<Report>().ToTable("Reports");
            modelBuilder.Entity<Commentary>().ToTable("Commentaries");

            modelBuilder.Entity<MyTask>().HasData(
                new MyTask()
                {
                    Id = Guid.NewGuid(),
                    Name = "Заассаймить ассаймент бекапной джобы",
                    State = 1,
                    AssignedDate = DateTime.Now,
                    CreationDate = DateTime.Now,
                    AssignedEmployeeId = dungeonMaster.Id,
                });
            
            modelBuilder.Entity<Employee>().HasData(
                dungeonMaster,
                new Employee { Id = Guid.NewGuid(), FirstName= "Bibletoon", MasterId = dungeonMaster.Id },
                new Employee { Id = Guid.NewGuid(), FirstName= "Black", SecondName = "Man", MasterId = dungeonMaster.Id },
                new Employee { Id = Guid.NewGuid(), FirstName= "Sam", MasterId = dungeonMaster.Id });
            
            base.OnModelCreating(modelBuilder);
        }
    }
}