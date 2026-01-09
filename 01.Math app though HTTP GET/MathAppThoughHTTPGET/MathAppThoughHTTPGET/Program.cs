var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run(async (HttpContext context)=>
{
    if (context.Request.Method == "GET" && context.Request.Path == "/")
    {
        var firstNumber = 0;
        var secondNumber = 0;
        var operation = "";
        float result = 0;

        if (context.Request.Query.ContainsKey("firstNumber"))
        {
            string stringFirstNumber = context.Request.Query["firstNumber"][0];
            if (!string.IsNullOrEmpty(stringFirstNumber))
            {
                firstNumber = Convert.ToInt32(stringFirstNumber);
            }
            else
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Неверный ввод первого числа");
            }
        }

    }
});