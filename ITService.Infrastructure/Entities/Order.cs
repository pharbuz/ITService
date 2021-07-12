using System;
using System.Collections.Generic;

#nullable disable

namespace ITService.Infrastructure.Entities
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string OrderStatus { get; set; }
        public DateTime OrderDate { get; set; }
        public string Carrier { get; set; }
        public string City { get; set; }
        public double OrderTotal { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateTime PaymentDueDate { get; set; }
        public string PaymentStatus { get; set; }
        public string PhoneNumber { get; set; }
        public string PostalCode { get; set; }
        public DateTime ShippingDate { get; set; }
        public string Street { get; set; }
        public string TrackingNumber { get; set; }
        public string TransactionId { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
