using System;

namespace ITService.Domain.Command.Manufacturer
{
    public class DeleteManufacturerCommand : ICommand
    {
        public DeleteManufacturerCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
