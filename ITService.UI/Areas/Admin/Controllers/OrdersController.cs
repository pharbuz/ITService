using ITService.Domain.Query.Dto;
using ITService.Domain.Query.Dto.Pagination.PageResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITService.UI.Filters;
using Microsoft.AspNetCore.Authorization;

namespace ITService.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [ServiceFilter(typeof(JwtAuthFilter))]
    [Authorize]
    public class OrdersController : Controller
    {
        public IActionResult Index()
        {
            var items = new List<OrderDto>() { new OrderDto() { Id = Guid.NewGuid(), OrderTotal = 220, PhoneNumber = "321125644", OrderStatus = "Pending", PaymentStatus = "Pending", OrderDate = DateTime.Now, } };
            var obj = new OrderPageResult<OrderDto>(items, 5, 5, 5);
            return View(obj);
        }
        
    }
}
