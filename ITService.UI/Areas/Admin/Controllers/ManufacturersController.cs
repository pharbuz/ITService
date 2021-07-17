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
    public class ManufacturersController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var items = new List<ManufacturerDto>() { new ManufacturerDto() { Id = Guid.NewGuid(), Name = "Amd" }, new ManufacturerDto() { Id = Guid.NewGuid(), Name = "Intel" } };
            var obj = new ManufacturerPageResult<ManufacturerDto>(items, 5, 5, 5);
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
