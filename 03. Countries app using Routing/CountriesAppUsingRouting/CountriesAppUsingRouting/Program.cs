
var countries = new Dictionary<int, string>
{
    { 1, "United States" },
    { 2, "Canada" },
    { 3, "United Kingdom" },
    { 4, "India" },
    { 5, "Japan" }
};

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/countries", async context =>
{
    foreach (var country in countries)
    {
        await context.Response.WriteAsync($"{country.Key}. {country.Value}\n");
    }
});

app.MapGet("/countries/{id:int:range(0, 100)}", async context =>
{
    int id = Convert.ToInt32(context.Request.RouteValues["id"]);

    if (id < 1 || id > 5)
    {
        context.Response.StatusCode = 404;
        await context.Response.WriteAsync("[No Country]");
        return;
    }

    context.Response.StatusCode = 200;
    await context.Response.WriteAsync(countries[id]);
});

app.Run();