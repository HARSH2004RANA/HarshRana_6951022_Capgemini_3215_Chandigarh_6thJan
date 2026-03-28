using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
namespace ECommerceApp.filters
{
    

    public class AdminAuthorizationFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var isAdmin = context.HttpContext.Request.Query["admin"];

            if (isAdmin != "true")
            {
                context.Result = new ContentResult
                {
                    Content = "Access Denied! Admin only."
                };
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
