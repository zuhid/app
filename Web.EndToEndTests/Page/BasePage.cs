using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace Web.EndToEndTests.Page;

public abstract class BasePage {
  private readonly WebDriver webDriver;

  public BasePage(WebDriver webDriver) {
    this.webDriver = webDriver;
  }

  protected void Text_Set(By by, string value) {
    var element = webDriver.FindElement(by);
    element.Clear();
    element.SendKeys(value ?? string.Empty);
  }

  protected void Text_Verify(By by, string value) {
    Assert.AreEqual(value, webDriver.FindElement(by).GetAttribute("value"));
  }

  protected void Checkbox_Set(By by, bool value) {
    var element = webDriver.FindElement(by);
    if (element.Selected && !value) {
      element.Click();
    }
    if (!element.Selected && value) {
      element.Click();
    }
  }

  protected void Checkbox_Verify(By by, bool value) {
    Assert.AreEqual(value, webDriver.FindElement(by).Selected);
  }

  protected void Button_Click(By by) {
    webDriver.FindElement(by).Click();
  }
}
