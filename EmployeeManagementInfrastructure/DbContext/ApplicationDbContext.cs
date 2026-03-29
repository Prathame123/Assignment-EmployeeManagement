using EmployeeManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Infrastructure.DbContext
{
    /// <summary>
    /// EF Core DbContext supporting SQL Server, PostgreSQL, and MySQL.
    /// The database provider is selected at startup via appsettings.json.
    /// </summary>
    public class ApplicationDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.EmployeeID);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasIndex(e => e.Email)
                    .IsUnique();

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Department)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.JobTitle)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Salary)
                    .HasColumnType("decimal(18,2)");

                entity.Property(e => e.IsActive)
                    .HasDefaultValue(true);
            });

            // Seed initial data
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    EmployeeID = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john.doe@company.com",
                    Phone = "+1-555-0101",
                    Department = "Engineering",
                    JobTitle = "Senior Developer",
                    DateOfBirth = new DateTime(1990, 5, 15),
                    DateOfJoining = new DateTime(2020, 1, 10),
                    Salary = 85000,
                    IsActive = true,
                    CreatedAt = new DateTime(2020, 1, 10)
                },
                new Employee
                {
                    EmployeeID = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    FirstName = "Jane",
                    LastName = "Smith",
                    Email = "jane.smith@company.com",
                    Phone = "+1-555-0102",
                    Department = "Human Resources",
                    JobTitle = "HR Manager",
                    DateOfBirth = new DateTime(1985, 8, 22),
                    DateOfJoining = new DateTime(2018, 3, 5),
                    Salary = 70000,
                    IsActive = true,
                    CreatedAt = new DateTime(2018, 3, 5)
                },
                new Employee
                {
                    EmployeeID = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    FirstName = "Michael",
                    LastName = "Johnson",
                    Email = "michael.johnson@company.com",
                    Phone = "+1-555-0103",
                    Department = "Finance",
                    JobTitle = "Financial Analyst",
                    DateOfBirth = new DateTime(1993, 11, 30),
                    DateOfJoining = new DateTime(2021, 6, 15),
                    Salary = 62000,
                    IsActive = true,
                    CreatedAt = new DateTime(2021, 6, 15)
                }
            );
        }
    }
}
