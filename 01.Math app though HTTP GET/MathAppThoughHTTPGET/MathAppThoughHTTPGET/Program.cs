var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Run(async (HttpContext context)=>
{
    if (context.Request.Method == "GET" && context.Request.Path == "/")
    {
        var firstNumber = 0;
        var secondNumber = 0;
        string? operation = null;
        float? result = null;

        if (context.Request.Query.ContainsKey("firstNumber"))
        {
            var stringFirstNumber = context.Request.Query["firstNumber"][0];
            
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
        else
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync("Неверный ввод первого числа");
        }

        if (context.Request.Query.ContainsKey("secondNumber"))
        {
            var stringSecondNumber = context.Request.Query["secondNumber"][0];
            
            if (!string.IsNullOrEmpty(stringSecondNumber))
            {
                secondNumber = Convert.ToInt32(stringSecondNumber);
            }
            else
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Неверный ввод второго числа");
            }
        }
        else
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync("Неверный ввод второго числа");
        }

        if (context.Request.Query.ContainsKey("operation"))
        {
            operation = context.Request.Query["operation"][0];

            if (!string.IsNullOrEmpty(operation))
            {
                switch (operation)
                {
                    case "add":
                        result = firstNumber + secondNumber;
                        break;
                    case "multiply":
                        result = firstNumber * secondNumber;
                        break;
                    case "subtract":
                        result = firstNumber - secondNumber;
                        break;
                    case "divide":
                        result = secondNumber == 0 ? 0 : firstNumber / secondNumber;
                        break;
                    case "mod":
                        result = secondNumber == 0 ? 0 : firstNumber % secondNumber;
                        break;
                    default:
                        context.Response.StatusCode = 400;
                        await context.Response.WriteAsync("Неверный ввод операции над числами");
                        return;
                }
            }
            else
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Неверный ввод операции над числами");
            }
        }
        else
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync("Неверный ввод операции над числами");
        }

        if (result.HasValue)
        {
            await context.Response.WriteAsync(result.ToString());
        }
    }
});

app.Run();