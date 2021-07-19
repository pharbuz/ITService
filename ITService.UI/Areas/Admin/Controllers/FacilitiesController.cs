using ITService.Domain.Query.Dto;
using ITService.Domain.Query.Dto.Pagination.PageResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ITService.Domain;
using ITService.Domain.Command.Facility;
using ITService.Domain.Enums;
using ITService.Domain.Query.Facility;
using ITService.Infrastructure;
using ITService.UI.Filters;
using Microsoft.AspNetCore.Authorization;

namespace ITService.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [ServiceFilter(typeof(JwtAuthFilter))]
    [Authorize]
    public class FacilitiesController : Controller
    {
        private readonly IMediator _mediator;

        public FacilitiesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var query = new SearchFacilitiesQuery()
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
        public async Task<IActionResult> Add(AddFacilityCommand command)
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
            var query = new GetFacilityQuery(id);

            var result = await _mediator.QueryAsync(query);

            var facilitiyCommand = new EditFacilityCommand() { Name = result.Name, Id = result.Id,  City=result.City, 
                MapUrl=result.MapUrl, OpenedSaturday=result.OpenedSaturday, 
                OpenedWeek=result.OpenedWeek, PhoneNumber=result.PhoneNumber, PostalCode=result.PostalCode, StreetAdress=result.StreetAdress};

            return View(facilitiyCommand);
        }

        [HttpPost]
        public async Task<IActionResult> Update(EditFacilityCommand command)
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
            var command = new DeleteFacilityCommand(id);

            var result = await _mediator.CommandAsync(command);

            if (result.IsFailure)
            {
                ModelState.PopulateValidation(result.Errors);
            }

            return RedirectToAction("Index");
        }
    }
}
