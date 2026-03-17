public interface IAuthService
{
	bool IsAuthenticated(HttpContext context);
}