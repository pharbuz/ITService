using System;

namespace ITService.Domain.Command.Order
{
    public sealed class AddOrderCommand : ICommand
    {
        public Guid? UserId { get; set; }
        public Guid? EmployeeId { get; set; }
        public int? Amount { get; set; }
        public string OrderStatus { get; set; }
        public DateTime? OrderDate { get; set; }
    }
}