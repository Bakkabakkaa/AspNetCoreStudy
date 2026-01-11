namespace LoginUsingMiddleware.Middleware;

public static class LoginMiddlewareExtensions
{
    public static IApplicationBuilder UseLoginMiddleware(this IApplicationBuilder app)
    {
        return app.UseMiddleware<MyMiddleware>();
    }
}