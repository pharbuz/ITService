using ITService.Domain.Query.Dto;
using ITService.Domain.Query.Dto.Pagination.PageResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITService.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FacilitiesController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var items = new List<FacilityDto>() { new FacilityDto() { Id = Guid.NewGuid(),
                Name = "Service4Now Warszawa", City="Warszawa", OpenedSaturday="8:00-16:00", 
                OpenedWeek = "8:00-12:00", PhoneNumber="432642355", PostalCode="39-127", StreetAdress="Malinowa 2/5",
                MapUrl= "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d20502.376814732215!2d21.978487029171365!3d50.033897632869156!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x0%3A0x3bec5584cbe783b7!2zR2FsZXJpYSBoYW5kbG93YSDigJ5QYXNhxbwgUnplc3rDs3figJ0!5e0!3m2!1spl!2spl!4v1626184518901!5m2!1spl!2spl" } };
            var obj = new FacilityPageResult<FacilityDto>(items, 5, 5, 5);
            return View(obj);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Update(Guid id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            return View();
        }
    }
}
