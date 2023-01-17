using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace eCommerce.AutomatedUITests.Admin
{
    public class UserTest : IDisposable
    {
        private readonly IWebDriver driver;
        public UserTest() => driver = Config.ChromeDriver;

        [Fact]
        public void Add()
        {
            var random = new Random();
            driver.LoginAsAdmin();
            driver.Navigate().GoToUrl($"{Config.AppUrl}/Admin/User/Add");
            driver.FindElement(By.Id("FirstName")).ClearAndSendValue("test");
            driver.FindElement(By.Id("LastName")).ClearAndSendValue("user");
            driver.FindElement(By.Id("Email")).ClearAndSendValue($"test{DateTime.Now.Millisecond}@user.com");
            driver.FindElement(By.Id("Password")).ClearAndSendValue("Recep123.");
            driver.FindElement(By.Id("PasswordConfirm")).ClearAndSendValue("Recep123.");
            driver.FindElement(By.Id("DateBirth")).ClearAndSendValue("01/02/1993");
            driver.FindElement(By.Id("Address")).ClearAndSendValue("Lorem ipsum dolor sit amet, consectetur adipiscing elit.");
            var role = new SelectElement(driver.FindElement(By.Id("RoleId")));
            role.SelectByIndex(random.Next(1, role.Options.Count));
            driver.FindElement(By.Id("Add")).Click();
        }

        [Fact]
        public void Update()
        {
            var random = new Random();
            driver.LoginAsAdmin();
            driver.Navigate().GoToUrl($"{Config.AppUrl}/Admin/User/Index");
            new SelectElement(driver.FindElement(By.Name("users_length"))).SelectByIndex(3);
            Thread.Sleep(2000);
            var buttons = driver.FindElements(By.CssSelector("a[class='btn btn-success']"));
            buttons.ElementAt(random.Next(1, buttons.Count)).Click();
            driver.FindElement(By.Id("FirstName")).ClearAndSendValue("test_v2");
            driver.FindElement(By.Id("LastName")).ClearAndSendValue("user_v2");
            driver.FindElement(By.Id("Email")).ClearAndSendValue($"testv2{DateTime.Now.Millisecond}@userv2.com");
            driver.FindElement(By.Id("DateBirth")).ClearAndSendValue("02/02/1993");
            driver.FindElement(By.Id("Address")).ClearAndSendValue("V2_Lorem ipsum dolor sit amet, consectetur adipiscing elit.");
            var role = new SelectElement(driver.FindElement(By.Id("RoleId")));
            role.SelectByIndex(random.Next(1, role.Options.Count));
            driver.FindElement(By.Id("Update")).Click();
        }

        [Fact]
        public void Lockout()
        {
            var random = new Random();
            driver.LoginAsAdmin();
            driver.Navigate().GoToUrl($"{Config.AppUrl}/Admin/User/Index");
            new SelectElement(driver.FindElement(By.Name("users_length"))).SelectByIndex(3);
            Thread.Sleep(2000);
            var buttons = driver.FindElements(By.CssSelector("a[class='btn btn-warning']"));
            buttons.ElementAt(random.Next(1, buttons.Count)).Click();
            driver.FindElement(By.Id("LockoutEnd")).ClearAndSendValue(DateTime.Now.AddDays(random.Next(3, 20)).ToString("dd/MM/yyyy"));
            driver.FindElement(By.Id("Lockout")).Click();
        }

        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}
