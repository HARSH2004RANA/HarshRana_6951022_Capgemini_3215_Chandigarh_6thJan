public class AdminAuthMiddleware
{
	private readonly RequestDelegate _next;

	public AdminAuthMiddleware(RequestDelegate next)
	{
		_next = next;
	}

	public async Task InvokeAsync(HttpContext context, IAuthService authService)
	{
		var path = context.Request.Path.Value;

		if (path.StartsWith("/Admin"))
		{
			if (!authService.IsAuthenticated(context))
			{
				context.Response.Redirect("/Home/Login");
				return;
			}
		}

		await _next(context);
	}
}