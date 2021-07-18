using ITService.Domain.Query.Dto;
using ITService.Domain.Query.Dto.Pagination.PageResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITService.Domain;
using ITService.Domain.Enums;
using ITService.Domain.Query.Order;
using ITService.UI.Filters;
using Microsoft.AspNetCore.Authorization;

namespace ITService.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [ServiceFilter(typeof(JwtAuthFilter))]
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            var query = new SearchOrdersQuery()
            {
                OrderBy = "OrderStatus",
                PageNumber = 1,
                PageSize = 100,
                SearchPhrase = null,
                SortDirection = SortDirection.ASC
            };

            var result = await _mediator.QueryAsync(query);
            return View(result);
        }

        public async Task<IActionResult> Search(string status)
        {
            var query = new SearchOrdersQuery()
            {
                OrderBy = "OrderStatus",
                PageNumber = 1,
                PageSize = 100,
                SearchPhrase = status,
                SortDirection = SortDirection.ASC
            };

            var result = await _mediator.QueryAsync(query);
            return View("Index", result);
        }
    }
}
