using FluentValidation;
using EmployeeManagement.Core.DTO;

namespace EmployeeManagement.Core.Validators
{
    /// <summary>
    /// FluentValidation rules for updating an existing employee.
    /// </summary>
    public class EmployeeUpdateRequestValidator : AbstractValidator<EmployeeUpdateRequest>
    {
        public EmployeeUpdateRequestValidator()
        {
            RuleFor(e => e.EmployeeID)
                .NotEmpty().WithMessage("Employee ID is required.");

            RuleFor(e => e.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(50).WithMessage("First name cannot exceed 50 characters.");

            RuleFor(e => e.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters.");

            RuleFor(e => e.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Please provide a valid email address.")
                .MaximumLength(100).WithMessage("Email cannot exceed 100 characters.");

            RuleFor(e => e.Phone)
                .NotEmpty().WithMessage("Phone is required.")
                .Matches(@"^\+?[0-9\s\-\(\)]{7,20}$").WithMessage("Please provide a valid phone number.");

            RuleFor(e => e.Department)
                .NotEmpty().WithMessage("Department is required.")
                .MaximumLength(100).WithMessage("Department cannot exceed 100 characters.");

            RuleFor(e => e.JobTitle)
                .NotEmpty().WithMessage("Job title is required.")
                .MaximumLength(100).WithMessage("Job title cannot exceed 100 characters.");

            RuleFor(e => e.DateOfBirth)
                .NotEmpty().WithMessage("Date of birth is required.")
                .LessThan(DateTime.UtcNow.AddYears(-18))
                .WithMessage("Employee must be at least 18 years old.");

            RuleFor(e => e.DateOfJoining)
                .NotEmpty().WithMessage("Date of joining is required.")
                .LessThanOrEqualTo(DateTime.UtcNow)
                .WithMessage("Date of joining cannot be in the future.");

            RuleFor(e => e.Salary)
                .GreaterThan(0).WithMessage("Salary must be greater than 0.");
        }
    }
}
