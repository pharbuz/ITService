using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ITService.Domain;
using ITService.Domain.Command.User;
using ITService.Domain.Query.User;
using ITService.Domain.Repositories;
using ITService.Infrastructure;
using ITService.UI.Filters;
using Microsoft.AspNetCore.Authorization;

namespace ITService.UI.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    [ServiceFilter(typeof(JwtAuthFilter))]
    public class AccountController : Controller
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IActionResult ChangePassword()
        {
            var command = new EditUserPasswordCommand()
            {
                Id = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value)
            };
            return View(command);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(EditUserPasswordCommand command)
        {
            var result = await _mediator.CommandAsync(command);
            if (result.IsFailure)
            {
                ModelState.PopulateValidation(result.Errors);
                return View(command);
            }

            return RedirectToAction("Index", "Home", new { area = "Customer" });
        }

        public async Task<IActionResult> ChangeDetails()
        {
            var query = await _mediator.QueryAsync(new GetUserQuery(Guid.Parse(HttpContext.User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value)));
            
            return View(query);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeDetails(EditUserDetailsCommand command)
        {
            var result = await _mediator.CommandAsync(command);
            if (result.IsFailure)
            {
                ModelState.PopulateValidation(result.Errors);
                return View(command);
            }

            return RedirectToAction("Index", "Home", new { area = "Customer" });
        }
    }
}
