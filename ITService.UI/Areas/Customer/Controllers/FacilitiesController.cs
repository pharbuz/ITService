﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITService.UI.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class FacilitiesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
