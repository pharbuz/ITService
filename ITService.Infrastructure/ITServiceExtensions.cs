using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ITService.Domain;

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
