using ITService.Domain.Query.Dto;
using ITService.Domain.Query.Dto.Pagination.PageResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITService.UI.Filters;
using Microsoft.AspNetCore.Authorization;

namespace ITService.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [ServiceFilter(typeof(JwtAuthFilter))]
    [Authorize]
    public class UsersController : Controller
    {
        public IActionResult Index()
        {

            var items = new List<UserDto>() { new UserDto() { Id = Guid.NewGuid(), Login="Olrb15", Email="s3Olegon@g2.pl", PhoneNumber="422532363", Street="Worna 20", City="Rzeszów", PostalCode="39-127" }, new UserDto() { Id = Guid.NewGuid(), Login = "Olrb15", Email = "s3Olegon@g2.pl" } };
            var obj = new UserPageResult<UserDto>(items, 5, 5, 5);
            return View(obj);

            
        }


        [HttpGet]
        public IActionResult Lock(Guid id)
        {
            var items = new List<UserDto>() { new UserDto() { Id = Guid.NewGuid(), Login = "Olrb15", Email = "s3Olegon@g2.pl", PhoneNumber = "422532363", Street = "Worna 20", City = "Rzeszów", PostalCode = "39-127" }, new UserDto() { Id = Guid.NewGuid(), Login = "Olrb15", Email = "s3Olegon@g2.pl" } };
            var obj = new UserPageResult<UserDto>(items, 5, 5, 5);
            ///Tu można blokować usera na jakąś hardcodowaną wartość :)
            return View(nameof(Index), obj);
        }


        [HttpGet]
        public IActionResult Unlock(Guid id)
        {
            return View(nameof(Index));
        }

    }
}
