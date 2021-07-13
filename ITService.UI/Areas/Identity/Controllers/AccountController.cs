using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITService.UI.Areas.Identity.Controllers
{
    [Area("Identity")]
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Logout()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
    }
}
