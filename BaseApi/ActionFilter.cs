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
      logger.LogInformation(new EventId(context.HttpContext.Response.StatusCode), $"{path} -> {JsonSerializer.Serialize(objectResult.Value, jsonSerializerOptions)}");
    }
  }

  private static JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions {
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
  };

}
