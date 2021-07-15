using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITService.Domain;
using ITService.Domain.Command.Role;
using ITService.Domain.Command.User;
using ITService.Domain.Enums;
using ITService.Domain.Query.Role;
using ITService.Infrastructure;

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
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginCommand command)
        {
            AddRoleCommand role = new AddRoleCommand();
            role.Name = "Administrator";
            AddRoleCommand role1 = new AddRoleCommand();
            role1.Name = "Moderator";
            AddRoleCommand role2 = new AddRoleCommand();
            role2.Name = "User";

            await _mediator.CommandAsync(role);
            await _mediator.CommandAsync(role1);
            await _mediator.CommandAsync(role2);

            var result = await _mediator.CommandAsync(command);
            if (result.IsFailure)
            {
                ModelState.PopulateValidation(result.Errors);
                return View();
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<ActionResult> Logout()
        {
            await _mediator.CommandAsync(new LogoutCommand());

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<ActionResult> Register(AddUserCommand command)
        {
            //var role = await _mediator.QueryAsync(new SearchRolesQuery()
            //{
            //    SearchPhrase = "Admin",
            //    PageNumber = 1,
            //    PageSize = 10,
            //    OrderBy = "Name",
            //    SortDirection = SortDirection.DESC
            //});

            //command.RoleId = role.Items.FirstOrDefault(r => r != null).Id;

            var result = await _mediator.CommandAsync(command);

            if (result.IsFailure)
            {
                ModelState.PopulateValidation(result.Errors);
                return View();
            }

            return RedirectToAction("Login");
        }
    }
}
