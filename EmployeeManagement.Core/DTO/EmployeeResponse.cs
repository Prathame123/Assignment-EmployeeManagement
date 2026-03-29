namespace EmployeeManagement.Core.DTO
{
    /// <summary>
    /// DTO returned to the client after employee operations.
    /// </summary>
    public class EmployeeResponse
    {
        public Guid EmployeeID { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName => $"{FirstName} {LastName}";
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public int Age => (int)((DateTime.UtcNow - DateOfBirth).TotalDays / 365.25);
        public DateTime DateOfJoining { get; set; }
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
