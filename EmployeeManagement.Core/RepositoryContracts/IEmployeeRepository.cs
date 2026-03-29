using EmployeeManagement.Core.Entities;

namespace EmployeeManagement.Core.RepositoryContracts
{
    /// <summary>
    /// Repository contract for Employee data access operations.
    /// </summary>
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();
        Task<Employee?> GetEmployeeByIdAsync(Guid employeeId);
        Task<Employee?> GetEmployeeByEmailAsync(string email);
        Task<Employee> AddEmployeeAsync(Employee employee);
        Task<Employee> UpdateEmployeeAsync(Employee employee);
        Task<bool> DeleteEmployeeAsync(Guid employeeId);
    }
}
