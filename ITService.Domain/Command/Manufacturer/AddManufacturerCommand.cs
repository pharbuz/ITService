namespace ITService.Domain.Command.Manufacturer
{
    public sealed class AddManufacturerCommand : ICommand
    {
        public string Name { get; set; }
    }
}