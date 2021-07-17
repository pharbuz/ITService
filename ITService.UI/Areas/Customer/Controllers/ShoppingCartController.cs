using ITService.Domain.Entities;
using ITService.Domain.Query.Dto;
using ITService.UI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITService.UI.Filters;
using Microsoft.AspNetCore.Authorization;

namespace ITService.UI.Areas.Customer.Controllers
{
    [Area("Customer")]
    [ServiceFilter(typeof(JwtAuthFilter))]
    [Authorize]
    public class ShoppingCartController : Controller
    {
        public IActionResult Index()
        {
            ShoppingCartViewModel shoppingCart = new ShoppingCartViewModel()
            {
                Order = new OrderDto() { OrderTotal = 22 },
                Shoppings = new List<ShoppingCartDto>() { new ShoppingCartDto() { Product = new ProductDto() { Price = 22, Name="Fajny" } } },
                Count = 1

            };
            return View(shoppingCart);
        }
    }
}
