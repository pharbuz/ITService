using System;

namespace ITService.Domain.Command.Service
{
    public sealed class AddServiceCommand : ICommand
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public double EstimatedServicePrice { get; set; }
        public string Description { get; set; }
    }
}