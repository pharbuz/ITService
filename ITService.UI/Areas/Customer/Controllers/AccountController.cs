using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITService.UI.Filters;
using Microsoft.AspNetCore.Authorization;

namespace ITService.UI.Areas.Customer.Controllers
{
    [Area("Customer")]
    [ServiceFilter(typeof(JwtAuthFilter))]
    [Authorize]
    public class AccountController : Controller
    {
        public IActionResult ChangePassword()
        {
            return View();
        }
        public IActionResult ChangeDetails()
        {
            return View();
        }

        

    }
}
