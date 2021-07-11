using System;

namespace ITService.Domain.Command.Employee
{
    public sealed class EditEmployeeCommand : ICommand
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public decimal? Salary { get; set; }
        public Guid? RoleId { get; set; }
    }
}
