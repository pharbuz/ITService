using ITService.Domain.Entities;
using ITService.Domain.Query.Dto;
using ITService.Domain.Query.Dto.Pagination.PageResults;
using ITService.UI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ITService.Domain;
using ITService.Domain.Command.Facility;
using ITService.Domain.Command.Product;
using ITService.Domain.Enums;
using ITService.Domain.Query.Category;
using ITService.Domain.Query.Facility;
using ITService.Domain.Query.Manufacturer;
using ITService.Domain.Query.Product;
using ITService.Infrastructure;
using ITService.UI.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace ITService.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [ServiceFilter(typeof(JwtAuthFilter))]
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IWebHostEnvironment _environment;
        private readonly IMapper _mapper;

        public ProductsController(IMediator mediator, IWebHostEnvironment environment, IMapper mapper)
        {
            _mediator = mediator;
            _environment = environment;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var query = new SearchProductsQuery()
            {
                OrderBy = "Name",
                PageNumber = 1,
                PageSize = 100,
                SearchPhrase = null,
                SortDirection = SortDirection.ASC
            };

            var result = await _mediator.QueryAsync(query);

            return View(result);
        }

        private async Task<ProductViewModel> GetProductViewModel()
        {
            var categories = (await _mediator.QueryAsync(new SearchCategoriesQuery()
            {
                PageNumber = 1,
                PageSize = 100,
                SearchPhrase = null,
                SortDirection = SortDirection.ASC
            })).Items.AsQueryable();

            var manufacturers = (await _mediator.QueryAsync(new SearchManufacturersQuery()
            {
                OrderBy = "Name",
                PageNumber = 1,
                PageSize = 100,
                SearchPhrase = null,
                SortDirection = SortDirection.ASC
            })).Items.AsQueryable();

            var model = new ProductViewModel()
            {
                Product = new ProductDto(),
                Categories = categories.Select(s => new SelectListItem(s.Name, s.Id.ToString())),
                Manufacturers = manufacturers.Select(s => new SelectListItem(s.Name, s.Id.ToString()))
            };
            return model;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = await GetProductViewModel();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(ProductViewModel viewModel)
        {
            var model = await GetProductViewModel();
            model.Product = viewModel.Product;
            string rootPath = _environment.WebRootPath;
            var files = HttpContext.Request.Form.Files;
            if (files.Count > 0)
            {
                string fileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(rootPath, @"images\products");
                var extension = Path.GetExtension(files[0].FileName);

                using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                {
                    files[0].CopyTo(fileStreams);
                }
                viewModel.Product.Image = @"\images\products\" + fileName + extension;
            }

            var result = await _mediator.CommandAsync(_mapper.Map<AddProductCommand>(model.Product));

            if (result.IsFailure)
            {
                ModelState.PopulateValidation(result.Errors);
                return View(model);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var query = new GetProductQuery(id);

            var result = await _mediator.QueryAsync(query);

            var categories = (await _mediator.QueryAsync(new SearchCategoriesQuery()
            {
                PageNumber = 1,
                PageSize = 100,
                SearchPhrase = null,
                SortDirection = SortDirection.ASC
            })).Items.AsQueryable();

            var manufacturers = (await _mediator.QueryAsync(new SearchManufacturersQuery()
            {
                OrderBy = "Name",
                PageNumber = 1,
                PageSize = 100,
                SearchPhrase = null,
                SortDirection = SortDirection.ASC
            })).Items.AsQueryable();

            var model = new ProductViewModel()
            {
                Product = result,
                Categories = categories.Select(s => new SelectListItem(s.Name, s.Id.ToString())),
                Manufacturers = manufacturers.Select(s => new SelectListItem(s.Name, s.Id.ToString()))
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(ProductViewModel viewModel)
        {
            var model = await GetProductViewModel();
            model.Product = viewModel.Product;
            string rootPath = _environment.WebRootPath;
            var files = HttpContext.Request.Form.Files;
            if (files.Count > 0)
            {
                string fileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(rootPath, @"images\products");
                var extension = Path.GetExtension(files[0].FileName);

                using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                {
                    files[0].CopyTo(fileStreams);
                }

                viewModel.Product.Image = @"\images\products\" + fileName + extension;
            }

            var result = await _mediator.CommandAsync(_mapper.Map<EditProductCommand>(viewModel.Product));

            if (result.IsFailure)
            {
                ModelState.PopulateValidation(result.Errors);
                return View(model);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteProductCommand(id);

            var result = await _mediator.CommandAsync(command);

            if (result.IsFailure)
            {
                ModelState.PopulateValidation(result.Errors);
            }

            return RedirectToAction("Index");
        }
    }
}
