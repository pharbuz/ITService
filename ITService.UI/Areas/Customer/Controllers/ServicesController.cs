using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITService.Domain;
using ITService.Domain.Command.Service;
using ITService.Domain.Enums;
using ITService.Domain.Query.Service;
using ITService.UI.Filters;
using Microsoft.AspNetCore.Authorization;

namespace ITService.UI.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ServicesController : Controller
    {
        private readonly IMediator _mediator;

        public ServicesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            var query = new SearchServicesQuery()
            {
                SearchPhrase = null,
                OrderBy = "Name",
                PageNumber = 1,
                PageSize = 10,
                SortDirection = SortDirection.ASC
            };

            var result = await _mediator.QueryAsync(query);

            return View(result);
        }
    }
}
