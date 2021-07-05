using System;

namespace ITService.Domain.Command.Category
{
    public class DeleteCategoryCommand : ICommand
    {
        public DeleteCategoryCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
