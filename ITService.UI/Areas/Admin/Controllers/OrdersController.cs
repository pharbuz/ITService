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
using System.Security.Claims;
using Stripe;

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
            var identity = (ClaimsIdentity)User.Identity;
            var claim = identity.FindFirst(ClaimTypes.NameIdentifier);


            var query = new SearchOrdersQuery()
            {
                OrderBy = "OrderStatus",
                PageNumber = 1,
                PageSize = 100,
                SearchPhrase = null,
                SortDirection = SortDirection.ASC
            };

            var result = await _mediator.QueryAsync(query);


            if (User.IsInRole(Roles.AdminUserRole) == false  && User.IsInRole(Roles.AdminUserRole)==false)
            {
                result.Items = result.Items.Where(n => n.UserId.ToString() == claim.Value).ToList();
     
            }
            return View(result);


        }

        [BindProperty]
        public OrderDetailsViewModel OrderDetails { get; set; }

        public async Task<IActionResult> Search(string status)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var claim = identity.FindFirst(ClaimTypes.NameIdentifier);

            var query = new SearchOrdersQuery()
            {
                OrderBy = "OrderStatus",
                PageNumber = 1,
                PageSize = 100,
                SearchPhrase = status,
                SortDirection = SortDirection.ASC
            };

            var result = await _mediator.QueryAsync(query);


            if (User.IsInRole(Roles.AdminUserRole) == false && User.IsInRole(Roles.AdminUserRole) == false)
            {
                result.Items = result.Items.Where(n => n.UserId.ToString() == claim.Value).ToList();

            }

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

        [HttpPost]
        public async Task<IActionResult> Cancel()
        {
            var orderChange = await _mediator.QueryAsync(new GetOrderQuery(OrderDetails.Header.Id));

            var command = _mapper.Map<EditOrderCommand>(orderChange);

            command.OrderStatus = OrderStatuses.StatusCancelled;

            var result = await _mediator.CommandAsync(command);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> HandleOrder()
        {
            
            var orderChange = await _mediator.QueryAsync(new GetOrderQuery(OrderDetails.Header.Id));
            var command = _mapper.Map<EditOrderCommand>(orderChange);
            command.OrderStatus = OrderStatuses.StatusInProcess;
            var result = await _mediator.CommandAsync(command);
            return RedirectToAction("Index");

        }

        [HttpPost]

        public async  Task<IActionResult> Shipment()
        {

            var orderChange = await _mediator.QueryAsync(new GetOrderQuery(OrderDetails.Header.Id));

            var command = _mapper.Map<EditOrderCommand>(orderChange);

            command.TrackingNumber = OrderDetails.Header.TrackingNumber;
            command.Carrier = OrderDetails.Header.Carrier;
            command.OrderStatus = OrderStatuses.StatusShipped;
            command.ShippingDate = DateTime.Now;

            command.City = OrderDetails.Header.City;
            command.Street = OrderDetails.Header.Street;
            command.PostalCode = OrderDetails.Header.PostalCode;
            command.PhoneNumber = OrderDetails.Header.PhoneNumber;

            var result = await _mediator.CommandAsync(command);
            return RedirectToAction("Index");

           
        }


        [HttpPost]
        [ActionName("Summary")]
        public async Task<IActionResult> Pay(string stripeToken)
        {
            var orderChange = await _mediator.QueryAsync(new GetOrderQuery(OrderDetails.Header.Id));
            var command = _mapper.Map<EditOrderCommand>(orderChange);

            if (stripeToken != null)
            {
                var options = new ChargeCreateOptions
                {
                    Amount = (long?)OrderDetails.Header.OrderTotal,
                    Currency = "usd",
                    Description = "Order ID: " ,
                    Source = stripeToken
                };
                var service = new ChargeService();
                Charge charge = service.Create(options);

                if (charge.Id == null)
                {
                    command.PaymentStatus = OrderStatuses.PaymentStatusRejected;
                    command.OrderStatus = OrderStatuses.StatusPending;
                }
                else
                {
                    command.TransactionId = charge.Id;
                }
                if (charge.Status.ToLower() == "succeeded")
                {
                    command.PaymentStatus = OrderStatuses.PaymentStatusApproved;
                    command.OrderStatus = OrderStatuses.StatusApproved;
                    command.PaymentDate = DateTime.Now;
                }
            }
            return RedirectToAction("Index");
          

        }








    }
}
