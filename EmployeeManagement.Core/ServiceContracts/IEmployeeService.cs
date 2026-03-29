using EmployeeManagement.Core.DTO;

namespace EmployeeManagement.Core.ServiceContracts
{
    /// <summary>
    /// Service contract for Employee business logic operations.
    /// </summary>
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeResponse>> GetAllEmployeesAsync();
        Task<EmployeeResponse?> GetEmployeeByIdAsync(Guid employeeId);
        Task<EmployeeResponse> AddEmployeeAsync(EmployeeAddRequest request);
        Task<EmployeeResponse> UpdateEmployeeAsync(EmployeeUpdateRequest request);
        Task<bool> DeleteEmployeeAsync(Guid employeeId);
    }
}
