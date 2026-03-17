using Microsoft.AspNetCore.Mvc.Filters;

namespace ProductManagement.Filters
{
    public class LogActionFilter
    {
		public void OnActionExecuting(ActionExecutingContext context)
		{
			var actionName = context.ActionDescriptor.DisplayName;
			var time = DateTime.Now;

			Console.WriteLine($"Action Executing: {actionName} at {time}");
		}

		public void OnActionExecuted(ActionExecutedContext context)
		{
			var actionName = context.ActionDescriptor.DisplayName;
			var time = DateTime.Now;

			Console.WriteLine($"Action Executed: {actionName} at {time}");
		}
	}
}
