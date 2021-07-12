using System;

namespace ITService.Domain.Command.Category
{
    public sealed class EditCategoryCommand : ICommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
