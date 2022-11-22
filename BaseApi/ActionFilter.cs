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

      logger.LogWarning(new EventId(400), path);
      context.Result = new BadRequestObjectResult(context.ModelState);
    } else if (objectResult != null) {
      // log the valid response
      logger.LogInformation(
        GetActionEventId(context.ActionDescriptor.RouteValues["action"]),
        $"{path} -> f{JsonSerializer.Serialize(objectResult.Value, jsonSerializerOptions)}f",
        "QQQQQQQQQQQQQQQQQQQQQQQQ",
        "HHHHHHHHHHHHHHHHHHHHHHHH",
        "EEEEEEEEEEEEEEEEEEEEEEEE"
        );
    }
  }

  private static JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions {
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
  };

  private static EventId GetActionEventId(string route) {
    switch (route.ToUpper()) {
      case "GET": return new EventId(100001);
      case "ADD": return new EventId(100002);
      case "UPDATE": return new EventId(100003);
      case "DELETE": return new EventId(100004);
      default: return new EventId(100000, route.ToUpper());
    }
  }

}
