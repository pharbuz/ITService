using ITService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITService.Domain.Query.Dto;

namespace ITService.UI.ViewModels
{
    public class OrderDetailsViewModel
    {
        public OrderDto Header { get; set; }

        public IEnumerable<OrderDetailDto> Details { get; set; }
    }
}
