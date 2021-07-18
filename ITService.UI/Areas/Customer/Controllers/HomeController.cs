using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITService.Domain;
using ITService.Domain.Enums;
using ITService.Domain.Query.Product;
using ITService.UI.Filters;
using Microsoft.AspNetCore.Authorization;

namespace ITService.UI.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    [ServiceFilter(typeof(JwtAuthFilter))]
    public class HomeController : Controller
    {
        private readonly IMediator _mediator;

        public HomeController(IMediator mediator)
        {
            _mediator = mediator;
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

        public async Task<IActionResult> Details()
        {
            return View();
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
    }
}
