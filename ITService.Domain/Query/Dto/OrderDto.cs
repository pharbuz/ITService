using System;

namespace ITService.Domain.Query.Dto
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public Guid? EmployeeId { get; set; }
        public int? Amount { get; set; }
        public string OrderStatus { get; set; }
        public DateTime? OrderDate { get; set; }
    }
}