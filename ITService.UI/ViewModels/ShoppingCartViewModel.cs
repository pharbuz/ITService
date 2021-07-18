using ITService.Domain.Entities;
using ITService.Domain.Query.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITService.UI.ViewModels
{
    public class ShoppingCartViewModel
    {
        public IEnumerable<ShoppingCartDto> Shoppings { get; set; }
        public UserDto User { get; set; }

        public OrderDto Order { get; set; }

        public int Count { get; set; }
    }
}
