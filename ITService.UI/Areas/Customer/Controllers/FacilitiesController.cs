using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITService.Domain;
using ITService.Domain.Command.Facility;
using ITService.Domain.Enums;
using ITService.Domain.Query.Facility;
using ITService.UI.Filters;
using Microsoft.AspNetCore.Authorization;

namespace ITService.UI.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class FacilitiesController : Controller
    {
        private readonly IMediator _mediator;

        public FacilitiesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            var query = new SearchFacilitiesQuery()
            {
                OrderBy = "Name",
                SearchPhrase = null,
                PageNumber = 1,
                PageSize = 10,
                SortDirection = SortDirection.ASC
            };

            var result = await _mediator.QueryAsync(query);

            return View(result);
        }
    }
}
