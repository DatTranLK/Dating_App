using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using API.Errors;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); // if there is no exception, then the request will be passed on to the next piece of middleware
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message); // log the exception
                context.Response.ContentType = "application/json"; // set the content type of the response to be JSON
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError; // set the status code of the response to be 500

                var response = _env.IsDevelopment()
                    ? new ApiException(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString())
                    : new ApiException(context.Response.StatusCode, "Internal Server Error");

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                var json = JsonSerializer.Serialize(response, options); // serialize the response object to JSON

                await context.Response.WriteAsync(json); // write the JSON to the HTTP response
            }
        }
    }
}