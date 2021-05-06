using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LogProxyAPI_HeidelbergCement.Configuration.ApiFilters.ActionFilters
{
    public class ValidateModelStateFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult("model is not valid");
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }
    }
}
