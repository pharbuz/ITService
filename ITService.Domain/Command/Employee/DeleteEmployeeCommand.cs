using System;

namespace ITService.Domain.Command.Employee
{
    public class DeleteEmployeeCommand : ICommand
    {
        public DeleteEmployeeCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
