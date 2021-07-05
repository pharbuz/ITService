using System;

namespace ITService.Domain.Command.OrderDetail
{
    public sealed class EditOrderDetailCommand : ICommand
    {
        public Guid Id { get; set; }
        public Guid? OrderId { get; set; }
        public Guid? ProductId { get; set; }
        public Guid? ServiceId { get; set; }
        public int? Price { get; set; }
        public int? Quantity { get; set; }
    }
}
