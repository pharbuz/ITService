using ITService.Domain.Query.Dto;
using ITService.Domain.Query.Dto.Pagination.PageResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ITService.Domain;
using ITService.Domain.Command.Order;
using ITService.Domain.Enums;
using ITService.Domain.Query.Order;
using ITService.Domain.Query.OrderDetail;
using ITService.Domain.Utilities;
using ITService.UI.Filters;
using ITService.UI.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace ITService.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [ServiceFilter(typeof(JwtAuthFilter))]
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public OrdersController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
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

        public async Task<IActionResult> Details(Guid id)
        {
            var query = new SearchOrderDetailsQuery()
            {
                OrderBy = "Quantity",
                PageNumber = 1,
                PageSize = 10000,
                SearchPhrase = null,
                SortDirection = SortDirection.DESC
            };

            var result = await _mediator.QueryAsync(query);

            var queryOrder = new GetOrderQuery(id);

            var resultOrder = await _mediator.QueryAsync(queryOrder);

            var model = new OrderDetailsViewModel()
            {
                Details = result.Items,
                Header = resultOrder
            };

            return View(model);
        }

        public async Task<IActionResult> Cancel(Guid id)
        {
            var order = await _mediator.QueryAsync(new GetOrderQuery(id));

            var command = _mapper.Map<EditOrderCommand>(order);

            command.OrderStatus = OrderStatuses.StatusCancelled;

            var result = await _mediator.CommandAsync(command);

            return RedirectToAction("Details", new { id = order.Id });
        }
    }
}
