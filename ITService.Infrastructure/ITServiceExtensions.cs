using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ITService.Domain;
using ITService.Domain.Query.Dto.Auth;
using ITService.Domain.Repositories;
using ITService.Infrastructure.Repositories;

namespace ITService.Infrastructure
{
    public static class ITServiceExtensions
    {
        public static void PopulateValidation(this ModelStateDictionary modelState, IEnumerable<Result.Error> errors)
        {
            foreach (var error in errors)
            {
                modelState.AddModelError(error.PropertyName, error.Message);
            }
        }
    }
}
