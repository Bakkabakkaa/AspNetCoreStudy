
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


app.Run();