using OpenQA.Selenium;

namespace Web.EndToEndTests.Page;

public class LoginPage : BasePage {
  private readonly By _email = By.Id("email");
  private readonly By _password = By.Id("password");
  private readonly By _rememberMe = By.Id("rememberMe");
  private readonly By _submit = By.Id("submit");

  public LoginPage(WebDriver webDriver) : base(webDriver) { }

  public void Set(string email, string passowrd, bool rememberMe) {
    Email(email);
    Password(passowrd);
    RememberMe(rememberMe);
  }
  public void Verify(string email, string password, bool rememberMe) {
    Text_Verify(_email, email);
    Text_Verify(_password, password);
    Checkbox_Verify(_rememberMe, rememberMe);
  }

  public void Email(string value) => Text_Set(_email, value);
  public void Password(string value) => Text_Set(_password, value);
  public void RememberMe(bool value) => Checkbox_Set(_rememberMe, value);
  public void Submit() => Button_Click(_submit);
}
