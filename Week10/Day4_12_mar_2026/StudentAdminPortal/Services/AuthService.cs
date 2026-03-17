public class AuthService : IAuthService
{
	public bool IsAuthenticated(HttpContext context)
	{
		var user = context.Session.GetString("User");

		if (user != null)
		{
			return true;
		}

		return false;
	}
}