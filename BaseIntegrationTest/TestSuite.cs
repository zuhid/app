using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zuhid.Tools;

namespace Zuhid.BaseIntegrationTest;

public class TestSuite {
  private readonly string baseUrl;
  private readonly string resultFolder;
  private readonly string tfsBasePath;
  private readonly StringBuilder content = new StringBuilder();
  private int totalTestCases = 0;
  private int totalSteps = 0;
  private int passedSteps = 0;

  public TestSuite(string baseUrl, string resultFolder, string tfsBasePath) {
    this.baseUrl = baseUrl;
    this.resultFolder = resultFolder;
    this.tfsBasePath = tfsBasePath;
  }

  public void GenerateReport() {
    var outputFile = Path.Combine(resultFolder, $"{baseUrl.Replace("/", ".")}.html");
    FileExtension.CreateReplace(outputFile, $@"<!DOCTYPE html>
<html lang='en'>
<head><link rel='stylesheet' type='text/css' href='style.css'></head>
<body>
<header class='{(passedSteps == totalSteps ? "success" : "failure")}'>
	<span>{baseUrl}</span>
	<span>Number of Test cases: {totalTestCases}</span>
	<span>Test steps result: {passedSteps}/{totalSteps} </span>
</header>
<table>
{content.ToString()}
</table>
</body>
</html>");
    Assert.AreEqual(totalSteps, passedSteps, $"file:///{outputFile}");
  }

  public async Task RunTestCase(string number, string description, RunTestCase runTestCase) {
    totalTestCases++;
    var userStory = (number == "Prerequisite") ? "Prerequisite" : $"<a href='{tfsBasePath}{number}' target='_blank'>User Story {number}</a> {description}";
    content.AppendLine($@"<thead>
  <tr><th colspan='5'>{userStory}</th><tr>
  <tr>
    <th>#</th>
    <th>Step Details</th>
    <th>Input Data</th>
    <th>Expected Result</th>
    <th>Actual Result</th>
  </tr>
</thead>
<tbody>");
    await runTestCase();
    content.AppendLine($@"<tr class='gap'><td colspan='5'></td></tr>
</tbody>");
  }

  public void AddTestStep(
    string stepDetail,

    HttpMethod httpMethod,
    string url,
    object inputBody,

    HttpStatusCode expectedStatus,
    object expectedBody,

    HttpStatusCode actualStatus,
    object actualBody,

    bool result
    ) {
    totalSteps++;
    if (result) {
      passedSteps++;
    }
    content.AppendLine($@"    <tr class='{(result ? "success" : "failure")}'>
      <td></td>
      <td>{stepDetail}</td>
      <td>
        <b>{httpMethod.Method.ToUpperCaseFirst()}</b>: {url}<br/>
        <b>Body</b>: {JsonString(inputBody)}<br>
      </td>
      <td>
        <b>Status</b>: {expectedStatus}<br/>
        <b>Body</b>: <pre>{JsonString(expectedBody)}</pre>
      </td>
      <td>
        <b>Status</b>: {actualStatus}<br/>
        <b>Body</b>: <pre>{JsonString(actualBody)}</pre>
      </td>
    </tr>");
  }

  private string JsonString(object input) {
    var jsonSerializerOptions = new JsonSerializerOptions {
      PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
      DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
      WriteIndented = true,
    };
    return (input == null) ? string.Empty : $"<pre>{JsonSerializer.Serialize(input, jsonSerializerOptions)}</pre>";
  }
}
