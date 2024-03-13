using APICatalog.Repositories;
using GraphQL.Types;
using GraphQL;
using Microsoft.AspNetCore.Razor.Language.CodeGeneration;
using System.Text.Json;

namespace APICatalog.GraphQL
{
    public class GraphQLMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly IUnitOfWork _context;

        public GraphQLMiddleware(RequestDelegate next, IUnitOfWork contexto)
        {
            _next = next;
            _context = contexto;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Request.Path.StartsWithSegments("/graphql"))
            {
                using (var stream = new StreamReader(httpContext.Request.Body))
                {
                    var query = await stream.ReadToEndAsync();
                    if (!String.IsNullOrWhiteSpace(query))
                    {
                        var schema = new Schema
                        {
                            Query = new CategoryQuery(_context)
                        };
                        var result = await new DocumentExecuter().ExecuteAsync(options =>
                        {
                            options.Schema = schema;
                            options.Query = query;
                        });
                        await WriteResult(httpContext, result);
                    }
                }
            }
            else
            {
                await _next(httpContext);
            }
        }

        private async Task WriteResult(HttpContext httpContext,
           ExecutionResult result)
        {
            var json = JsonSerializer.Serialize(result, new JsonSerializerOptions { WriteIndented = true });
            httpContext.Response.StatusCode = 200;
            httpContext.Response.ContentType = "application/json";
            await httpContext.Response.WriteAsync(json);
        }

    }
}
