﻿namespace ITService.Domain.Command.Category
{
    public sealed class AddCategoryCommand : ICommand
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}