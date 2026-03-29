using AutoMapper;
using EmployeeManagement.Core.DTO;
using EmployeeManagement.Core.Entities;
using EmployeeManagement.Core.RepositoryContracts;
using EmployeeManagement.Core.ServiceContracts;

namespace EmployeeManagement.Core.Services
{
    /// <summary>
    /// Employee service implementing business logic using Clean Architecture patterns.
    /// </summary>
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EmployeeResponse>> GetAllEmployeesAsync()
        {
            var employees = await _employeeRepository.GetAllEmployeesAsync();
            return _mapper.Map<IEnumerable<EmployeeResponse>>(employees);
        }

        public async Task<EmployeeResponse?> GetEmployeeByIdAsync(Guid employeeId)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(employeeId);
            if (employee == null)
                return null;

            return _mapper.Map<EmployeeResponse>(employee);
        }

        public async Task<EmployeeResponse> AddEmployeeAsync(EmployeeAddRequest request)
        {
            // Check for duplicate email
            var existingEmployee = await _employeeRepository.GetEmployeeByEmailAsync(request.Email);
            if (existingEmployee != null)
                throw new InvalidOperationException($"An employee with email '{request.Email}' already exists.");

            var employee = _mapper.Map<Employee>(request);
            employee.EmployeeID = Guid.NewGuid();
            employee.CreatedAt = DateTime.UtcNow;
            employee.IsActive = true;

            var addedEmployee = await _employeeRepository.AddEmployeeAsync(employee);
            return _mapper.Map<EmployeeResponse>(addedEmployee);
        }

        public async Task<EmployeeResponse> UpdateEmployeeAsync(EmployeeUpdateRequest request)
        {
            var existingEmployee = await _employeeRepository.GetEmployeeByIdAsync(request.EmployeeID);
            if (existingEmployee == null)
                throw new KeyNotFoundException($"Employee with ID '{request.EmployeeID}' not found.");

            // Check for email conflict with another employee
            var emailConflict = await _employeeRepository.GetEmployeeByEmailAsync(request.Email);
            if (emailConflict != null && emailConflict.EmployeeID != request.EmployeeID)
                throw new InvalidOperationException($"Email '{request.Email}' is already used by another employee.");

            _mapper.Map(request, existingEmployee);
            existingEmployee.UpdatedAt = DateTime.UtcNow;

            var updatedEmployee = await _employeeRepository.UpdateEmployeeAsync(existingEmployee);
            return _mapper.Map<EmployeeResponse>(updatedEmployee);
        }

        public async Task<bool> DeleteEmployeeAsync(Guid employeeId)
        {
            var existingEmployee = await _employeeRepository.GetEmployeeByIdAsync(employeeId);
            if (existingEmployee == null)
                throw new KeyNotFoundException($"Employee with ID '{employeeId}' not found.");

            return await _employeeRepository.DeleteEmployeeAsync(employeeId);
        }
    }
}
