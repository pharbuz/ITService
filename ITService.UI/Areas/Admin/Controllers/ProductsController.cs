using ITService.Domain.Entities;
using ITService.Domain.Query.Dto;
using ITService.Domain.Query.Dto.Pagination.PageResults;
using ITService.UI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITService.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var items = new List<ProductDto>() { new ProductDto() { Id = Guid.NewGuid(), Name = "Ryzen 5", Price=220 , Category=new CategoryDto() { Name="Karta Graficzna" }, Manufacturer= new ManufacturerDto() { Name="Intel" } }, new ProductDto() { Id = Guid.NewGuid(), Name = "Gtx 1660", Price = 220,  Category = new CategoryDto() { Name = "Karta Graficzna" }, Manufacturer = new ManufacturerDto() { Name = "Intel" } } };
            var obj = new ProductPageResult<ProductDto>(items, 5, 5, 5);
            return View(obj);
        }
        [HttpGet]
        public IActionResult Add()
        {
            ProductViewModel productVM = new ProductViewModel()
            {
                Product = new ProductDto(),
                Categories = new List<CategoryDto>() { new CategoryDto() { Id=Guid.NewGuid(), Name="Karta Graficzna" } }.Select(i => new SelectListItem { Text = i.Name, Value = i.Id.ToString() }),
                Manufacturers = new List<ManufacturerDto>() { new ManufacturerDto() { Id = Guid.NewGuid(), Name = "Intel" } }.Select(i => new SelectListItem { Text = i.Name, Value = i.Id.ToString() }),


            };
            
            return View(productVM);
        }

        [HttpGet]
        public IActionResult Update(Guid id)
        {
            ProductViewModel productVM = new ProductViewModel()
            {
                Product = new ProductDto(),
                Categories = new List<CategoryDto>() { new CategoryDto() { Id = Guid.NewGuid(), Name = "Karta Graficzna" } }.Select(i => new SelectListItem { Text = i.Name, Value = i.Id.ToString() }),
                Manufacturers = new List<ManufacturerDto>() { new ManufacturerDto() { Id = Guid.NewGuid(), Name = "Intel" } }.Select(i => new SelectListItem { Text = i.Name, Value = i.Id.ToString() }),


            };

            return View(productVM);
            return View();
        }

        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            return View();
        }


    }
}
