using ITService.Domain.Entities;
using ITService.Domain.Query.Dto;
using ITService.UI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ITService.Domain;
using ITService.Domain.Command.Order;
using ITService.Domain.Command.OrderDetail;
using ITService.Domain.Command.ShoppingCart;
using ITService.Domain.Query.ShoppingCart;
using ITService.Domain.Query.User;
using ITService.Domain.Utilities;
using ITService.Infrastructure;
using ITService.UI.Filters;
using Microsoft.AspNetCore.Authorization;
using Stripe;

namespace ITService.UI.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    [ServiceFilter(typeof(JwtAuthFilter))]
    public class ShoppingCartController : Controller
    {
        private readonly IMediator _mediator;

        public ShoppingCartController(IMediator mediator)
        {
            _mediator = mediator;
        }

        private Guid GetCurrentUserId()
        {
            return Guid.Parse(HttpContext.User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
        }

        public async Task<IActionResult> Index()
        {
            var shoppings = await _mediator.QueryAsync(new SearchShoppingCartsQuery()
            {
                SearchPhrase = null,
                PageNumber = 1,
                PageSize = 1000,
                OrderBy = "Count",
                UserId = GetCurrentUserId()
            });
            ShoppingCartViewModel svm = new ShoppingCartViewModel()
            {
                Shoppings = shoppings.Items,
                Count = shoppings.Items.Count,
                Order = new OrderDto() { OrderTotal = (double)shoppings.Items.Sum(x => x.Product.Price * x.Count)}
            };
            return View(svm);
        }

        public async Task<IActionResult> Summary()
        {
            var shoppings = await _mediator.QueryAsync(new SearchShoppingCartsQuery()
            {
                SearchPhrase = null,
                PageNumber = 1,
                PageSize = 10,
                OrderBy = "Count",
                UserId = GetCurrentUserId()
            });
            ShoppingCartViewModel svm = new ShoppingCartViewModel()
            {
                Shoppings = shoppings.Items,
                Count = shoppings.Items.Count,
                Order = new OrderDto() { OrderTotal = (double)shoppings.Items.Sum(x => x.Product.Price * x.Count) },
                User = await _mediator.QueryAsync(new GetUserQuery(GetCurrentUserId()))
            };
            return View(svm);
        }

        public async Task<IActionResult> Remove(Guid id)
        {
            var command = new DeleteShoppingCartCommand(id);

            var result = await _mediator.CommandAsync(command);

            if (result.IsFailure)
            {
                ModelState.PopulateValidation(result.Errors);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> More(Guid id)
        {
            var queryGet = new GetShoppingCartQuery(id);

            var queryResult = await _mediator.QueryAsync(queryGet);

            var command = new EditShoppingCartCommand()
            {
                Count = queryResult.Count + 1,
                Id = queryResult.Id,
                ProductId = queryResult.ProductId,
                UserId = queryResult.UserId
            };

            var result = await _mediator.CommandAsync(command);

            if (result.IsFailure)
            {
                ModelState.PopulateValidation(result.Errors);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Less(Guid id)
        {
            var queryGet = new GetShoppingCartQuery(id);

            var queryResult = await _mediator.QueryAsync(queryGet);

            if (queryResult.Count > 1)
            {
                var command = new EditShoppingCartCommand()
                {
                    Count = queryResult.Count - 1,
                    Id = queryResult.Id,
                    ProductId = queryResult.ProductId,
                    UserId = queryResult.UserId
                };

                var result = await _mediator.CommandAsync(command);

                if (result.IsFailure)
                {
                    ModelState.PopulateValidation(result.Errors);
                }
            }
            else
            {
                return RedirectToAction("Remove", new {id = id});
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ActionName("Summary")]
        public async Task<IActionResult> Pay(string stripeToken)
        {
            var currentUserId = GetCurrentUserId();
            var currentUser = await _mediator.QueryAsync(new GetUserQuery(currentUserId));
            var shoppings = (await _mediator.QueryAsync(new SearchShoppingCartsQuery()
            {
                SearchPhrase = null,
                PageNumber = 1,
                PageSize = 1000,
                OrderBy = "Count",
                UserId = currentUserId
            })).Items;

            decimal priceTotal = 0m;

            foreach (var shopping in shoppings)
            {
                priceTotal += shopping.Product.Price * shopping.Count;
            }

            var command = new AddOrderCommand()
            {
                UserId = currentUserId,
                OrderDate = DateTime.Now,
                ShippingDate = DateTime.Now.AddDays(7),
                OrderTotal = (double) priceTotal,
                TrackingNumber = null,
                Carrier = null,
                OrderStatus = null,
                PaymentStatus = null,
                PaymentDate = DateTime.Now,
                PaymentDueDate = DateTime.Now.AddDays(3),
                TransactionId = null,
                Street = currentUser.Street,
                City = currentUser.City,
                PhoneNumber = currentUser.PhoneNumber,
                PostalCode = currentUser.PostalCode
            };

            if (stripeToken != null)
            {
                var options = new ChargeCreateOptions
                {
                    Amount = Convert.ToInt32((double) priceTotal * 100),
                    Currency = "usd",
                    Description = "Order ID: " + command.TransactionId,
                    Source = stripeToken
                };
                var service = new ChargeService();
                Charge charge = service.Create(options);

                command.PaymentStatus = OrderStatuses.PaymentStatusPending;
                command.OrderStatus = OrderStatuses.StatusPending;

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

            var result = await _mediator.CommandAsync(command);
            var orderId = Guid.Parse(result.Message);

            foreach (var cart in shoppings)
            {
                var orderDetail = new AddOrderDetailCommand()
                {
                    OrderId = orderId,
                    Price = priceTotal,
                    ProductId = cart.ProductId,
                    Quantity = cart.Count
                };

                await _mediator.CommandAsync(orderDetail);

                await _mediator.CommandAsync(new DeleteShoppingCartCommand(cart.Id));
            }

            return RedirectToAction("OrderConfirm", "ShoppingCart", new { id = orderId });
        }

        public async Task<IActionResult> OrderConfirm(Guid id)
        {
            return View(id);
        }
    }
}
