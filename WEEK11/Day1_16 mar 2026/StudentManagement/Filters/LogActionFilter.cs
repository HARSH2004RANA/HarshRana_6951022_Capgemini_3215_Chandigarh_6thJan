using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace StudentManagement.Filters
{
	public class LogActionFilter : IActionFilter
	{
		public void OnActionExecuting(ActionExecutingContext context)
		{
			Console.WriteLine($"Executing: {context.ActionDescriptor.DisplayName} at {DateTime.Now}");
		}

		public void OnActionExecuted(ActionExecutedContext context)
		{
			Console.WriteLine($"Executed: {context.ActionDescriptor.DisplayName} at {DateTime.Now}");
		}
	}
}