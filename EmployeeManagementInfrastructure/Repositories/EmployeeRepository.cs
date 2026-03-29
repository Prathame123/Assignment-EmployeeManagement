using EmployeeManagement.Core.Entities;
using EmployeeManagement.Core.RepositoryContracts;
using EmployeeManagement.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Infrastructure.Repositories
{
    /// <summary>
    /// Concrete EF Core implementation of IEmployeeRepository.
    /// Works with SQL Server, PostgreSQL, or MySQL depending on the configured provider.
    /// </summary>
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public EmployeeRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            return await _dbContext.Employees
                .OrderBy(e => e.LastName)
                .ThenBy(e => e.FirstName)
                .ToListAsync();
        }

        public async Task<Employee?> GetEmployeeByIdAsync(Guid employeeId)
        {
            return await _dbContext.Employees
                .FirstOrDefaultAsync(e => e.EmployeeID == employeeId);
        }

        public async Task<Employee?> GetEmployeeByEmailAsync(string email)
        {
            return await _dbContext.Employees
                .FirstOrDefaultAsync(e => e.Email.ToLower() == email.ToLower());
        }

        public async Task<Employee> AddEmployeeAsync(Employee employee)
        {
            await _dbContext.Employees.AddAsync(employee);
            await _dbContext.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee> UpdateEmployeeAsync(Employee employee)
        {
            _dbContext.Employees.Update(employee);
            await _dbContext.SaveChangesAsync();
            return employee;
        }

        public async Task<bool> DeleteEmployeeAsync(Guid employeeId)
        {
            var employee = await _dbContext.Employees.FindAsync(employeeId);
            if (employee == null)
                return false;

            _dbContext.Employees.Remove(employee);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
