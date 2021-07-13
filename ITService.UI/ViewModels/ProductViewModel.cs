using ITService.Domain.Entities;
using ITService.Domain.Query.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITService.UI.ViewModels
{
    public class ProductViewModel
    {
        public ProductDto Product { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
        public IEnumerable<SelectListItem> Manufacturers { get; set; }

        
    }
}
