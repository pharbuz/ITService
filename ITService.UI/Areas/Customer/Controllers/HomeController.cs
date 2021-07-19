using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ITService.Domain;
using ITService.Domain.Command.ShoppingCart;
using ITService.Domain.Enums;
using ITService.Domain.Query.Order;
using ITService.Domain.Query.OrderDetail;
using ITService.Domain.Query.Product;
using ITService.Domain.Query.ShoppingCart;
using ITService.Infrastructure;
using ITService.UI.Filters;
using ITService.UI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace ITService.UI.Areas.Customer.Controllers
{
    [Area("Customer")]

   
    public class HomeController : Controller
    {
        private readonly IMediator _mediator;


        public HomeController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [ServiceFilter(typeof(JwtAuthFilter))]
        private Guid GetCurrentUserId()
        {
            return Guid.Parse(HttpContext.User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
        }


        public async Task<IActionResult> Index()
        {
            var query = new SearchProductsQuery()
            {
                SearchPhrase = null,
                PageNumber = 1,
                PageSize = 10,
                OrderBy = "Name",
                SortDirection = SortDirection.ASC
            };
            var result = await _mediator.QueryAsync(query);
            return View(result);
        }


        
        public async Task<IActionResult> Details(Guid productId)
        {
            var product = await _mediator.QueryAsync(new GetProductQuery(productId));
            var command = new AddShoppingCartCommand()
            {
                Product = product,
                ProductId = productId,
                Count = 1,
            
            };
            return View(command);
        }

        [HttpPost]
        public async Task<IActionResult> Search(string searchPhrase)
        {
            var query = new SearchProductsQuery()
            {
                SearchPhrase = searchPhrase,
                PageNumber = 1,
                PageSize = 10,
                OrderBy = "Name",
                SortDirection = SortDirection.ASC
            };
            var result = await _mediator.QueryAsync(query);
            return View("Index", result);
        }


        [ServiceFilter(typeof(JwtAuthFilter))]
        public async Task<IActionResult> AddToShoppingCart(Guid productId, int count)
        {
      


            if (HttpContext.User.Claims.FirstOrDefault()==null)
            {
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }
            var searchQuery = new SearchShoppingCartsQuery()
            {
                OrderBy = "Count",
                PageNumber = 1,
                PageSize = 1000,
                SortDirection = SortDirection.DESC,
                SearchPhrase = null,
                UserId = GetCurrentUserId()
            };

            var usersShoppingCarts = (await _mediator.QueryAsync(searchQuery)).Items.AsQueryable();

            var query = new GetProductQuery(productId);

            var queryResult = await _mediator.QueryAsync(query);

            var cart = usersShoppingCarts.FirstOrDefault(c => c.Product.Id == queryResult.Id);

            if (cart != null)
            {
                cart.Count += count;
                var cmd = new EditShoppingCartCommand()
                {
                    Count = cart.Count,
                    Id = cart.Id,
                    ProductId = cart.ProductId,
                    UserId = cart.UserId
                };

                var cmdResult = await _mediator.CommandAsync(cmd);

                if (cmdResult.IsFailure)
                {
                    ModelState.PopulateValidation(cmdResult.Errors);
                    return View("Details");
                }

                return RedirectToAction("Index", "ShoppingCart");
            }

            var command = new AddShoppingCartCommand()
            {
                UserId = GetCurrentUserId(),
                ProductId = productId,
                Count = count,
                Product = queryResult
            };

            var result = await _mediator.CommandAsync(command);

            if (result.IsFailure)
            {
                ModelState.PopulateValidation(result.Errors);
                return View("Details");
            }

            return RedirectToAction("Index", "ShoppingCart");
        }
    }
}
