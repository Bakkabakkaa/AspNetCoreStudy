namespace LoginUsingMiddleware.Middleware;

public class MyMiddleware
{
    private readonly RequestDelegate _next;
    private string userEmail = "admin@example.com";
    private string userPassword = "admin1234";

    public MyMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Method == "Post" && context.Request.Path == "/")
        {
            var form = await context.Request.ReadFormAsync();

            var email = form["email"][0];
            var password = form["password"][0];

            if (string.IsNullOrEmpty(password) || password != userPassword)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Неверный пароль");
            }
            
            if (string.IsNullOrEmpty(email) || email != userEmail)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Неверный логин");
            }
            else
            {
                await context.Response.WriteAsync("Успешный вход");
            }
        }
        else
        {
            await _next(context);
        }
    }
}