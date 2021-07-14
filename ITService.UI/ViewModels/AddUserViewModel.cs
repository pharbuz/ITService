using ITService.Domain.Command.User;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITService.UI.ViewModels
{
    public class AddUserViewModel
    {
        public AddUserCommand User { get; set; }

        public List<SelectListItem> Roles { get; set; }
    }
}
