using EmployeeManagement.Core.DTO;
using EmployeeManagement.Core.ServiceContracts;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.API.Controllers
{
    /// <summary>
    /// REST API controller for Employee CRUD operations.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IValidator<EmployeeAddRequest> _addValidator;
        private readonly IValidator<EmployeeUpdateRequest> _updateValidator;
        private readonly ILogger<EmployeesController> _logger;

        public EmployeesController(
            IEmployeeService employeeService,
            IValidator<EmployeeAddRequest> addValidator,
            IValidator<EmployeeUpdateRequest> updateValidator,
            ILogger<EmployeesController> logger)
        {
            _employeeService = employeeService;
            _addValidator = addValidator;
            _updateValidator = updateValidator;
            _logger = logger;
        }

        /// <summary>
        /// Gets all employees.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<EmployeeResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllEmployees()
        {
            _logger.LogInformation("Fetching all employees.");
            var employees = await _employeeService.GetAllEmployeesAsync();
            return Ok(employees);
        }

        /// <summary>
        /// Gets an employee by ID.
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(EmployeeResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetEmployeeById(Guid id)
        {
            _logger.LogInformation("Fetching employee with ID: {EmployeeId}", id);
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                _logger.LogWarning("Employee with ID {EmployeeId} not found.", id);
                return NotFound(new { message = $"Employee with ID '{id}' not found." });
            }
            return Ok(employee);
        }

        /// <summary>
        /// Creates a new employee.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(EmployeeResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> AddEmployee([FromBody] EmployeeAddRequest request)
        {
            var validationResult = await _addValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                _logger.LogWarning("Validation failed for AddEmployee request.");
                return BadRequest(new { errors = validationResult.Errors.Select(e => e.ErrorMessage) });
            }

            try
            {
                var addedEmployee = await _employeeService.AddEmployeeAsync(request);
                _logger.LogInformation("Employee created with ID: {EmployeeId}", addedEmployee.EmployeeID);
                return CreatedAtAction(nameof(GetEmployeeById),
                    new { id = addedEmployee.EmployeeID }, addedEmployee);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Conflict creating employee.");
                return Conflict(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Updates an existing employee.
        /// </summary>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(EmployeeResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> UpdateEmployee(Guid id, [FromBody] EmployeeUpdateRequest request)
        {
            if (id != request.EmployeeID)
            {
                return BadRequest(new { message = "Route ID and body EmployeeID must match." });
            }

            var validationResult = await _updateValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                _logger.LogWarning("Validation failed for UpdateEmployee request.");
                return BadRequest(new { errors = validationResult.Errors.Select(e => e.ErrorMessage) });
            }

            try
            {
                var updatedEmployee = await _employeeService.UpdateEmployeeAsync(request);
                _logger.LogInformation("Employee updated with ID: {EmployeeId}", id);
                return Ok(updatedEmployee);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Employee not found for update.");
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Conflict updating employee.");
                return Conflict(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Deletes an employee by ID.
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            try
            {
                var result = await _employeeService.DeleteEmployeeAsync(id);
                if (!result)
                    return NotFound(new { message = $"Employee with ID '{id}' not found." });

                _logger.LogInformation("Employee deleted with ID: {EmployeeId}", id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Employee not found for deletion.");
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
