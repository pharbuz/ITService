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
using ITService.Domain.Command.ShoppingCart;
using ITService.Domain.Query.ShoppingCart;
using ITService.Infrastructure;
using ITService.UI.Filters;
using Microsoft.AspNetCore.Authorization;

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
                PageSize = 10,
                OrderBy = "Count",
                UserId = GetCurrentUserId()
            });
            ShoppingCartViewModel svm = new ShoppingCartViewModel()
            {
                Shoppings = shoppings.Items,
                Count = shoppings.Items.Count,
                Order = new OrderDto() { OrderTotal = (double)shoppings.Items.Sum(x => x.Product.Price)}
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
                Order = new OrderDto() { OrderTotal = (double)shoppings.Items.Sum(x => x.Product.Price) }
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

        public async Task<IActionResult> Pay()
        {
            return RedirectToAction("Index");
        }
    }
}
