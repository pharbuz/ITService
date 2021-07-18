using ITService.Domain.Query.Dto;
using ITService.Domain.Query.Dto.Pagination.PageResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITService.Domain;
using ITService.Domain.Command.Facility;
using ITService.Domain.Command.Manufacturer;
using ITService.Domain.Enums;
using ITService.Domain.Query.Category;
using ITService.Domain.Query.Facility;
using ITService.Domain.Query.Manufacturer;
using ITService.Infrastructure;
using ITService.UI.Filters;
using Microsoft.AspNetCore.Authorization;

namespace ITService.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [ServiceFilter(typeof(JwtAuthFilter))]
    [Authorize]
    public class ManufacturersController : Controller
    {
        private readonly IMediator _mediator;

        public ManufacturersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var query = new SearchManufacturersQuery()
            {
                OrderBy = "Name",
                PageNumber = 1,
                PageSize = 100,
                SearchPhrase = null,
                SortDirection = SortDirection.ASC
            };

            var result = await _mediator.QueryAsync(query);

            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddManufacturerCommand command)
        {
            var result = await _mediator.CommandAsync(command);

            if (result.IsFailure)
            {
                ModelState.PopulateValidation(result.Errors);
                return View();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var query = new GetManufacturerQuery(id);

            var result = await _mediator.QueryAsync(query);

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Update(EditManufacturerCommand command)
        {
            var result = await _mediator.CommandAsync(command);

            if (result.IsFailure)
            {
                ModelState.PopulateValidation(result.Errors);
                return View();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteManufacturerCommand(id);

            var result = await _mediator.CommandAsync(command);

            if (result.IsFailure)
            {
                ModelState.PopulateValidation(result.Errors);
            }

            return RedirectToAction("Index");
        }
    }
}
