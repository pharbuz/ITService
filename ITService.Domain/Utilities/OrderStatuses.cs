using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITService.Domain.Utilities
{
    public static class OrderStatuses
    {

        public const string StatusPending = "Waiting";
        public const string StatusApproved = "Approved"; 
        public const string StatusShipped = "Shipped";
        public const string StatusCancelled = "Cancelled";
        public const string StatusRefunded = "Refunded";
        public const string StatusInProcess = "Process";

        public const string PaymentStatusPending = "Waiting";
        public const string PaymentStatusApproved = "Approved";
        public const string PaymentStatusRejected = "Rejected";

    }
}
