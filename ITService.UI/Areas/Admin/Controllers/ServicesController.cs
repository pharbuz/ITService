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

namespace ITService.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ServicesController : Controller
    {
        private readonly IWebHostEnvironment _hostEnviroment;
        public ServicesController(IWebHostEnvironment hostEnviroment)
        {
            _hostEnviroment = hostEnviroment;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var items = new List<ServiceDto>() 
            { new ServiceDto() { Id = Guid.NewGuid(), 
                Name = "System instalation", EstimatedServicePrice=20,  
                Description= "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. " }, new ServiceDto() { Id = Guid.NewGuid(), Name = "Graphic Card" } };
            var obj = new ServicePageResult<ServiceDto>(items, 5, 5, 5);
            return View(obj);
        }

        [HttpGet]
        public IActionResult Add()
        {
            
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(AddServiceCommand command)
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
                command.Image = @"\images\products\" + fileName + extension;




                var items = new List<ServiceDto>()
            { new ServiceDto() { Id = Guid.NewGuid(),
                Name = "System instalation", EstimatedServicePrice=20,
                Description= "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. " }, new ServiceDto() { Id = Guid.NewGuid(), Name = "Graphic Card" } };
                var obj = new ServicePageResult<ServiceDto>(items, 5, 5, 5);
                return View(nameof(Index), items);

            }
            return View(command);





         
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
