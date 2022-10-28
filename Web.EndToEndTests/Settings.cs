using System;
using OpenQA.Selenium;

namespace Web.EndToEndTests;

public class Settings {
  public static WebDriver WebDriver {
    get {
      var webDriver = new OpenQA.Selenium.Firefox.FirefoxDriver();
      // var webDriver = new OpenQA.Selenium.Chrome.ChromeDriver();
      // var webDriver = new OpenQA.Selenium.Edge.EdgeDriver();
      webDriver.Manage().Timeouts().ImplicitWait = new TimeSpan(0, 0, 5);
      return webDriver;
    }
  }

  public static string BaseUrl => "http://localhost:18000/";
}
