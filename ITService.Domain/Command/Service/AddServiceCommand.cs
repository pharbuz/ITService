using System;

namespace ITService.Domain.Command.Service
{
    public sealed class AddServiceCommand : ICommand
    {
        public string Name { get; set; }
        public int? Price { get; set; }
        public string Image { get; set; }
        public Guid? CategryId { get; set; }
        public string Description { get; set; }
    }
}