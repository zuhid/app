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
    // logger.LogError(new EventId(10, context.HttpContext.Request.Path.Value), context.Exception, JsonSerializer.Serialize(new {
    //   context.HttpContext.User.Identity.Name,
    //   context.Exception.Message,
    //   context.Exception.StackTrace,
    // }));

    //logger.LogError(new EventId(10), JsonSerializer.Serialize(new
    //{
    //	context.HttpContext.User.Identity.Name,
    //	context.Exception.Message,
    //	context.Exception.StackTrace,
    //}));

    // Return the exception message without the stacktrace to the user
    var response = context.HttpContext.Response;
    response.StatusCode = (int)HttpStatusCode.BadRequest;
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
