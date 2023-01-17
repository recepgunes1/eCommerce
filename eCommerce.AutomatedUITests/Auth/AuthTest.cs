using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace eCommerce.AutomatedUITests.Auth
{
    public class AuthTest : IDisposable
    {
        private readonly IWebDriver driver;

        public AuthTest() => driver = new ChromeDriver(Config.DriverDirectory);

        [Fact]
        public void SignIn()
        {
            driver.Navigate().GoToUrl($"{Config.AppUrl}/Auth/Home/Login");
            driver.FindElement(By.Id("Email")).ClearAndSendValue("admin@system.com");
            driver.FindElement(By.Id("Password")).ClearAndSendValue("123456");
            driver.FindElement(By.Id("SignIn")).Click();
            Assert.Equal("Index - eCommerce", driver.Title);
        }

        [Fact]
        public void SignUp()
        {
            var index = DateTime.Now.Millisecond;
            driver.Navigate().GoToUrl($"{Config.AppUrl}/Auth/Home/Register");
            driver.FindElement(By.Id("FirstName")).ClearAndSendValue("test");
            driver.FindElement(By.Id("LastName")).ClearAndSendValue("user");
            driver.FindElement(By.Id("Email")).ClearAndSendValue($"test{index}@user.com");
            driver.FindElement(By.Id("Password")).ClearAndSendValue("Recep123.");
            driver.FindElement(By.Id("PasswordConfirm")).ClearAndSendValue("Recep123.");
            driver.FindElement(By.Id("DateBirth")).ClearAndSendValue("02/02/1993");
            driver.FindElement(By.Id("Address")).ClearAndSendValue("Lorem ipsum dolor sit amet, consectetur adipiscing elit.");
            driver.FindElement(By.Id("SignUp")).Click();
            Assert.Equal("Index - eCommerce", driver.Title);
        }

        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}
