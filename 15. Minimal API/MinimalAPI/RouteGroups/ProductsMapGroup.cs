using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using MinimalAPI.Models;

namespace MinimalAPI.RouteGroups;

public static class ProductsMapGroup
{
    public static RouteGroupBuilder ProductsAPI(this RouteGroupBuilder group)
    {
        List<Product> products = new List<Product>()
        {
            new Product() { Id = 1, ProductName = "Smart Phone" },
            new Product() { Id = 2, ProductName = "Smart TV" }
        };

        //GET /products
        group.MapGet("/", async (HttpContext context) =>
        {
            // Sample Response:
            // [{ "Id": 1, "ProductName": "Smart Phone" }, { "Id": 2, "ProductName": "Smart TV" }]

            return Results.Ok(products);
        });

        // GET /products/{id}
        group.MapGet("/{id:int}", async (HttpContext context, int id) =>
        {
            Product? product = products.FirstOrDefault(temp => temp.Id == id);
            if (product == null)
            {
                return Results.BadRequest(new { error = "Incorrect Product ID" });
            }

            return Results.Ok(product);
        });

        // POST /products
        group.MapPost("/", async (HttpContext context, Product product) =>
        {
            products.Add(product);
            return Results.Ok(new { message = "Product Added" });
        }).AddEndpointFilter(async (EndpointFilterInvocationContext context, EndpointFilterDelegate next) =>
        {
            var product = context.Arguments.OfType<Product>().FirstOrDefault();

            if (product == null)
            {
                return Results.BadRequest("Product  details are not found in the request");
            }

            var validationContext = new ValidationContext(product);
            List<ValidationResult> errors = new List<ValidationResult>();
            
            bool isValid = Validator.TryValidateObject(product, validationContext, errors, true);

            if (!isValid)
            {
                return Results.BadRequest(errors.FirstOrDefault()?.ErrorMessage);
            }

            var result = await next(context); // Invokes the subsequent endpoint filter or endpoint's request delegate
            
            // After logic here

            return result;
        });

        // PUT /products/{id}
        group.MapPut("/{id}", async (HttpContext context, int id, [FromBody] Product product) =>
        {
            Product? productFromCollection = products.FirstOrDefault(temp => temp.Id == id);

            if (productFromCollection == null)
            {
                return Results.BadRequest(new {error = "Incorrect Product ID"});
            }

            productFromCollection.ProductName = product.ProductName;

            return Results.Ok(new { message = "Product Updated" });
        });

        // DELETE /products/{id}
        group.MapDelete("/{id}", async (HttpContext context, int id) =>
        {
            Product? productFromCollection = products.FirstOrDefault(temp => temp.Id == id);
            if (productFromCollection == null)
            {
                /*
                Sample Response:
                {
                 "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                 "title": "One or more validation errors occurred.",
                 "status": 400,
                 "errors": {
                     "id": [
                         "Incorrect Product ID"
                     ]
                  }
                }
                */

                return Results.ValidationProblem(
                    new Dictionary<string, string[]>
                    {
                        { "id", new string[] { "Incorrect Product ID" } }
                    });
            }

            products.Remove(productFromCollection);

            // await context.Response.WriteAsync("Product Deleted");

            return Results.Ok(new
            {
                message = "Product Deleted"
            });
        });

        return group;
    }
}