using System;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TvMazeScraper.Core.Exceptions;
using TvMazeScraper.Core.Model;

namespace TvMazeScraper.Core.MiddleWare.ExceptionMiddleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Something went wrong.");
                await HandleException(httpContext, exception);
            }
        }
        private static Task HandleException(HttpContext context, Exception exception)
        {
            context.Response.ContentType = MediaTypeNames.Application.Json;
            context.Response.StatusCode = exception is BaseException baseException
                ? (int) baseException.StatusCode
                : (int) HttpStatusCode.InternalServerError;
            var errorDetails = new ErrorDetailsModel
            {
                StatusCode = context.Response.StatusCode,
                ErrorCode = exception.GetType().Name,
                ErrorMessage = "An unexpected error occurred.",
                ErrorDetails = exception.Message
            };
            var settings = new JsonSerializerSettings {ContractResolver = new CamelCasePropertyNamesContractResolver()};
            var json = JsonConvert.SerializeObject(errorDetails, settings);
            return context.Response.WriteAsync(json);
        }
    }
}