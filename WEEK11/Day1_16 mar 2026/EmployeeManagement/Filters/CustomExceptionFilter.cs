using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EmployeeManagement.Filters
{
    public class CustomExceptionFilter : IExceptionFilter
    {
		public void OnException(ExceptionContext context)
		{
			context.Result = new ViewResult
			{
				ViewName = "Error"
			};

			context.ExceptionHandled = true;
		}
	}
}
