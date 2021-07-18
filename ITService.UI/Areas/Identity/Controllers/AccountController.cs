using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITService.Domain;
using ITService.Domain.Command.Role;
using ITService.Domain.Command.User;
using ITService.Domain.Entities;
using ITService.Domain.Enums;
using ITService.Domain.Query.Role;
using ITService.Domain.Utilities;
using ITService.Infrastructure;
using ITService.UI.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ITService.UI.Areas.Identity.Controllers
{
    [Area("Identity")]
    public class AccountController : Controller
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return View();
            }

            return RedirectToAction("Index", "Home", new { area = "Customer" });
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginCommand command)
        {
            var result = await _mediator.CommandAsync(command);
            if (result.IsFailure)
            {
                ModelState.PopulateValidation(result.Errors);
                return View();
            }

            return RedirectToAction("Index", "Home", new { area = "Customer" });
        }

        [HttpGet]
        public async Task<ActionResult> Logout()
        {
            await _mediator.CommandAsync(new LogoutCommand());

            return RedirectToAction("Login", "Account", new { area = "Identity"});
        }

        public async Task<IActionResult> Register()
        {
            if (!User.Identity.IsAuthenticated)
            {
                var roles = await _mediator.QueryAsync(new SearchRolesQuery()
                {
                    PageNumber = 1,
                    PageSize = 10,
                    OrderBy = "Name",
                    SortDirection = SortDirection.DESC
                });

                var roleItems = new List<SelectListItem>();

                foreach (var roleItem in roles.Items)
                {
                    roleItems.Add(new SelectListItem(roleItem.Name, roleItem.Id.ToString()));
                }

                var user = new AddUserCommand();

                user.RoleId = roles.Items.FirstOrDefault(r => r.Name == Roles.IndividualUserRole).Id;

                var model = new AddUserViewModel()
                {
                    User = user,
                    Roles = roleItems
                };
                return View(model);
            }

            return RedirectToAction("Login", "Account", new { area = "Identity" });
        }

        [HttpPost]
        public async Task<ActionResult> Register(AddUserViewModel model)
        {
            var result = await _mediator.CommandAsync(model.User);

            if (result.IsFailure)
            {
                ModelState.PopulateValidation(result.Errors);
                return View();
            }

            return RedirectToAction("Login");
        }
    }
}
