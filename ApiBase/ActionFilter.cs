using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Zuhid.ApiBase;

public class ActionFilter : IActionFilter {
  private readonly ILogger logger;

  public ActionFilter(ILogger<ActionFilter> logger) => this.logger = logger;

  public void OnActionExecuting(ActionExecutingContext context) { }

  public void OnActionExecuted(ActionExecutedContext context) {
    var objectResult = context.Result as ObjectResult;
    if (objectResult != null) {
      var controllerMethod = $"{context.ActionDescriptor.RouteValues["controller"]}/{context.ActionDescriptor.RouteValues["action"]}";
      var queryString = context.HttpContext.Request.QueryString.Value;
      logger.LogInformation($"{controllerMethod}{queryString} -> {JsonSerializer.Serialize(objectResult.Value, jsonSerializerOptions)}");
    } else if (context.Exception != null) {
      logger.LogError(new EventId(11, context.Controller.ToString()), JsonSerializer.Serialize(new {
        context.Exception.Message,
        context.Exception.StackTrace,
      }));
    }
    // If ModelState is invalid then return a badrequest
    if (!context.ModelState.IsValid) {
      context.Result = new BadRequestObjectResult(context.ModelState);
    }
  }

  private static JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions {
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
  };

}

// public void OnActionExecuting(ActionExecutingContext context) {
//   var hashtable = new Hashtable();
//   hashtable.Add($"{context.ActionDescriptor.RouteValues["controller"]}/{context.ActionDescriptor.RouteValues["action"]}", context.ActionArguments);
//   logger.LogInformation(null, JsonSerializer.Serialize(hashtable, jsonSerializerOptions));
// }

// public void OnActionExecuting(ActionExecutingContext context) {
//   foreach (var item in context.ActionArguments) {
//     _logger.LogInformation(new EventId(0, context.HttpContext.User.Identity.Name), $"{context.ActionDescriptor.DisplayName} ({item.Key}) : {JsonConvert.SerializeObject(item.Value)}");
//   }
// }

// public void OnActionExecuted(ActionExecutedContext context) {
//   _logger.LogInformation(new EventId(0, context.HttpContext.User.Identity.Name), $"{context.ActionDescriptor.DisplayName}: ActionExecutedContext {JsonConvert.SerializeObject(context.Result)}");
// }

