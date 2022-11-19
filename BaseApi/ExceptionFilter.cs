using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Zuhid.BaseApi;

public class ExceptionFilter : IExceptionFilter {
  private readonly ILogger logger;

  public ExceptionFilter(ILogger<ExceptionFilter> logger) {
    this.logger = logger;
  }

  public void OnException(ExceptionContext context) {
    // Log the exception
    logger.LogError(new EventId(100, context.HttpContext.Request.Path.Value), context.Exception, JsonSerializer.Serialize(new {
      context.HttpContext.User.Identity.Name,
      context.HttpContext.Request.Path.Value,
      context.Exception.Message
    }));


    // Return the exception message without the stacktrace to the user
    var response = context.HttpContext.Response;
    response.StatusCode = (int)HttpStatusCode.InternalServerError;
    if (context.Exception is ApplicationException) {
      // exception thrown by the application intentionally
      response.WriteAsync(JsonSerializer.Serialize(new { Error = context.Exception.Message }));
    } else {
      // Unhandled Exception
      response.WriteAsync(JsonSerializer.Serialize(new { Error = "Unhandelded Excpetion" }));
    }
    context.ExceptionHandled = true;
  }
}
