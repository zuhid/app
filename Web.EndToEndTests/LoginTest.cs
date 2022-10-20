using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace Web.EndToEndTests;

[TestClass]
public class LoginTest {

  public WebDriver Driver() {
    return new OpenQA.Selenium.Firefox.FirefoxDriver();
    // return new OpenQA.Selenium.Chrome.ChromeDriver();
    // return new OpenQA.Selenium.Edge.EdgeDriver();
  }

  [TestMethod]
  public void TestMethod1() {
    using (var driver = Driver()) {
      driver.Manage().Timeouts().ImplicitWait = new TimeSpan(0, 0, 5);
      driver.Navigate().GoToUrl("http://localhost:18000/");
      var email = driver.FindElement(By.Id("email"));
      var password = driver.FindElement(By.Id("password"));
      var rememberMe = driver.FindElement(By.Id("rememberMe"));
      var submit = driver.FindElement(By.Id("submit"));

      email.SendKeys("admin@company.com");
      password.SendKeys("P@ssw0rd");
      rememberMe.Click();
      submit.Click();


      Thread.Sleep(5000); // give the ui some time to unload the page
    }
  }

}
