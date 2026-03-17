using Microsoft.AspNetCore.Mvc.Filters;

namespace EmployeeManagement.Filters
{
    public class LogActionFilter : IActionFilter
    {
		public void OnActionExecuting(ActionExecutingContext context)
		{
			var actionName = context.ActionDescriptor.DisplayName;
			var time = DateTime.Now;

			Console.WriteLine($"[LOG] Action: {actionName}, Time: {time}");
		}

		public void OnActionExecuted(ActionExecutedContext context)
		{
		}
	}
}
