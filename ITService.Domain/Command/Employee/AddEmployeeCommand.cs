using System;

namespace ITService.Domain.Command.Employee
{
    public sealed class AddEmployeeCommand : ICommand
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int? Salary { get; set; }
        public Guid? RoleId { get; set; }
    }
}