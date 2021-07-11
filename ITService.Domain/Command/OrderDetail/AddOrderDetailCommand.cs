using System;

namespace ITService.Domain.Command.OrderDetail
{
    public sealed class AddOrderDetailCommand : ICommand
    {
        public Guid? OrderId { get; set; }
        public Guid? ProductId { get; set; }
        public Guid? ServiceId { get; set; }
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }
    }
}