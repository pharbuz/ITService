using System;

namespace ITService.Domain.Command.Manufacturer
{
    public sealed class EditManufacturerCommand : ICommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
