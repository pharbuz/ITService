using System;

namespace ITService.Domain.Command.Order
{
    public class EditOrderCommand : ICommand
    {
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public Guid? EmployeeId { get; set; }
        public decimal? Amount { get; set; }
        public string OrderStatus { get; set; }
        public DateTime? OrderDate { get; set; }
    }
}
