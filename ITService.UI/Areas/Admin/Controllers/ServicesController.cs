using ITService.Domain.Command.Service;
using ITService.Domain.Query.Dto;
using ITService.Domain.Query.Dto.Pagination.PageResults;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ITService.Domain;
using ITService.Domain.Enums;
using ITService.Domain.Query.Service;
using ITService.Infrastructure;
using ITService.UI.Filters;
using Microsoft.AspNetCore.Authorization;

namespace ITService.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [ServiceFilter(typeof(JwtAuthFilter))]
    [Authorize]
    public class ServicesController : Controller
    {
        private readonly IWebHostEnvironment _hostEnviroment;
        private readonly IMediator _mediator;

        public ServicesController(IWebHostEnvironment hostEnviroment, IMediator mediator)
        {
            _hostEnviroment = hostEnviroment;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var query = new SearchServicesQuery()
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

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AddServiceCommand command)
        {
            string rootPath = _hostEnviroment.WebRootPath;
            var files = HttpContext.Request.Form.Files;
            if(files.Count > 0)
            {
                string fileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(rootPath, @"images\services");
                var extension = Path.GetExtension(files[0].FileName);

                using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                {
                    files[0].CopyTo(fileStreams);
                }
                command.Image = @"\images\services\" + fileName + extension;
            }

            var result = await _mediator.CommandAsync(command);

            if (result.IsFailure)
            {
                ModelState.PopulateValidation(result.Errors);
                return View();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var query = new GetServiceQuery(id);

            var result = await _mediator.QueryAsync(query);

            var serviceCommand = new EditServiceCommand() { Name=result.Name, Id=result.Id, Description=result.Description, EstimatedServicePrice=result.EstimatedServicePrice, Image=result.Image };


            return View(serviceCommand);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(EditServiceCommand command)
        {
            string rootPath = _hostEnviroment.WebRootPath;
            var files = HttpContext.Request.Form.Files;
            if (files.Count > 0)
            {
                string fileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(rootPath, @"images\services");
                var extension = Path.GetExtension(files[0].FileName);

                using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                {
                    files[0].CopyTo(fileStreams);
                }

                command.Image = @"\images\services\" + fileName + extension;
            }

            var result = await _mediator.CommandAsync(command);

            if (result.IsFailure)
            {
                ModelState.PopulateValidation(result.Errors);
                return View();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteServiceCommand(id);

            var result = await _mediator.CommandAsync(command);

            if (result.IsFailure)
            {
                ModelState.PopulateValidation(result.Errors);
            }

            return RedirectToAction("Index");
        }
    }
}
