using System;

namespace ITService.Domain.Query.Dto
{
    public class EmployeeDto
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public decimal? Salary { get; set; }
        public Guid? RoleId { get; set; }
    }
}
