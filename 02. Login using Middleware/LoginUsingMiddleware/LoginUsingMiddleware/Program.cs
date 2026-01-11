using LoginUsingMiddleware.Middleware;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseLoginMiddleware();

app.Run(async context =>
{
    context.Response.StatusCode = 200;
    await context.Response.WriteAsync("Нет ответа");
});

app.Run();