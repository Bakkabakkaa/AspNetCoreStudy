using Microsoft.Extensions.Primitives;

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
        if (context.Request.Method == "POST" && context.Request.Path == "/")
        {
            StreamReader reader = new StreamReader(context.Request.Body);
            var body = await reader.ReadToEndAsync();

            Dictionary<string, StringValues> queryDictionary =
                Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(body);

            bool hasEmail = queryDictionary.ContainsKey("email");
            bool hasPassword = queryDictionary.ContainsKey("password");

            if (!hasEmail && !hasPassword)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Неверный ввод логина\n");
                await context.Response.WriteAsync("Неверный ввод пароля");
                return;
            }
            else if (!hasEmail)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Неверный логин");
                return;
            }
            else if (!hasPassword)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Неверный пароль");
                return;
            }


            var email = queryDictionary["email"];
            var password = queryDictionary["password"];
            
            if (email != userEmail)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Неверный логин");
                return;
            }
            else if (password != userPassword)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Неверный пароль");
                return;
            }
            else
            {
                context.Response.StatusCode = 200;
                await context.Response.WriteAsync("Успешный вход");
                return;
            }
        }
        else
        {
            await _next(context);
        }
    }
}