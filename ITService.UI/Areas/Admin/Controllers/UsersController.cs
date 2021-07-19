using ITService.Domain.Query.Dto;
using ITService.Domain.Query.Dto.Pagination.PageResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITService.Domain;
using ITService.Domain.Command.User;
using ITService.Domain.Enums;
using ITService.Domain.Query.Role;
using ITService.Domain.Query.User;
using ITService.Domain.Utilities;
using ITService.Infrastructure;
using ITService.UI.Filters;
using ITService.UI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ITService.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [ServiceFilter(typeof(JwtAuthFilter))]
    [Authorize]
    public class UsersController : Controller
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            var query = new SearchUsersQuery()
            {
                SearchPhrase = null,
                OrderBy = "Login",
                PageNumber = 1,
                PageSize = 10,
                SortDirection = SortDirection.ASC
            };

            var result = await _mediator.QueryAsync(query);

            return View(result);
        }

        public async Task<IActionResult> Add()
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

            //user.RoleId = roles.Items.FirstOrDefault(r => r.Name == Roles.IndividualUserRole).Id;

            var model = new AddUserViewModel()
            {
                User = user,
                Roles = roleItems
            };
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Add(AddUserViewModel model)
        {
            if(model.User.RoleId==null)
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
                model.Roles = roleItems;
                return View();
            }
            var result = await _mediator.CommandAsync(model.User);
            if (result.IsFailure)
            {
                ModelState.PopulateValidation(result.Errors);
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
                model.Roles = roleItems;
                return View();
            }
            return RedirectToAction("Index", "Users", new { area = "Admin" });
        }

        [HttpGet]
        public async Task<IActionResult> Lock(Guid id)
        {
            var query = new GetUserQuery(id);

            var result = await _mediator.QueryAsync(query);

            result.LockoutEnd = DateTime.Now.AddMonths(1);

            var command = new EditUserDetailsCommand()
            {
                City = result.City,
                Id = result.Id,
                LockoutEnd = result.LockoutEnd,
                PhoneNumber = result.PhoneNumber,
                PostalCode = result.PostalCode,
                Street = result.Street
            };

            var commandResult = await _mediator.CommandAsync(command);

            if (commandResult.IsFailure)
            {
                ModelState.PopulateValidation(commandResult.Errors);
            }

            return RedirectToAction("Index", "Users", new { area = "Admin" });
        }


        [HttpGet]
        public async Task<IActionResult> Unlock(Guid id)
        {
            var query = new GetUserQuery(id);

            var result = await _mediator.QueryAsync(query);

            result.LockoutEnd = DateTime.Now;

            var command = new EditUserDetailsCommand()
            {
                City = result.City,
                Id = result.Id,
                LockoutEnd = result.LockoutEnd,
                PhoneNumber = result.PhoneNumber,
                PostalCode = result.PostalCode,
                Street = result.Street
            };

            var commandResult = await _mediator.CommandAsync(command);

            if (commandResult.IsFailure)
            {
                ModelState.PopulateValidation(commandResult.Errors);
            }

            return RedirectToAction("Index", "Users", new { area = "Admin" });
        }

    }
}
