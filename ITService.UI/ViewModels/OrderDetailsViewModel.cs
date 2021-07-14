using ITService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITService.UI.ViewModels
{
    public class OrderDetailsViewModel
    {
        public Order Header { get; set; }

        public IEnumerable<OrderDetail> Details { get; set; }
    }
}
