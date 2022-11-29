using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Zuhid.BaseApi;

public class ActionFilter : IActionFilter {
  private readonly ILogger logger;

  public ActionFilter(ILogger<ActionFilter> logger) => this.logger = logger;

  public void OnActionExecuting(ActionExecutingContext context) { }

  public void OnActionExecuted(ActionExecutedContext context) {
    var path = $"{context.ActionDescriptor.RouteValues["action"]}: {context.HttpContext.Request.Path}";

    var objectResult = context.Result as ObjectResult;
    if (!context.ModelState.IsValid) {
      // log the bad request
      logger.LogWarning(GetActionEventId(
        context.HttpContext.Request.Method, context.HttpContext.Request.Path),
        JsonSerializer.Serialize(context.ModelState, jsonSerializerOptions)
      );
      context.Result = new BadRequestObjectResult(context.ModelState);
    } else if (objectResult != null) {
      // log the valid response
      logger.LogInformation(
        GetActionEventId(context.HttpContext.Request.Method, context.HttpContext.Request.Path),
        JsonSerializer.Serialize(objectResult.Value, jsonSerializerOptions)
      );
    }
  }

  private static JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions {
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
  };

  private static EventId GetActionEventId(string action, string path) {
    switch (action.ToUpper()) {
      case "GET": return new EventId(100001, path);
      case "POST": return new EventId(100002, path);
      case "PUT": return new EventId(100003, path);
      case "DELETE": return new EventId(100004, path);
      default: return new EventId(100000, $"{action}:{path}");
    }
  }

}
