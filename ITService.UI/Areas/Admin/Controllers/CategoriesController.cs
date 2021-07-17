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
    public class CategoriesController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var items = new List<CategoryDto>() { new CategoryDto() { Id = Guid.NewGuid(), Name = "Graphic Card" } , new CategoryDto() { Id = Guid.NewGuid(), Name = "Graphic Card" } };
            var obj = new CategoryPageResult<CategoryDto>(items,5,5,5);
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
