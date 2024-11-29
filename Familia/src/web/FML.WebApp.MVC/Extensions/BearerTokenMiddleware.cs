
using Familia.WebApp.MVC.Extensions;

public class BearerTokenMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IAspNetUser _user;

    public BearerTokenMiddleware(RequestDelegate next, IAspNetUser user)
    {
        _next = next;
        _user = user;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var token = _user.ObterUserToken();
        if (!string.IsNullOrEmpty(token))
        {
            context.Request.Headers.Add("Authorization", $"Bearer {token}");
        }

        await _next(context);
    }

}
