using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace eCommerce.AutomatedUITests.Admin
{
    public class CommentTest : IDisposable
    {
        private readonly IWebDriver driver;

        public CommentTest() => driver = new ChromeDriver(Config.DriverDirectory);

        [Fact]
        public void Delete()
        {
            var random = new Random();
            driver.LoginAsAdmin();
            driver.Navigate().GoToUrl($"{Config.AppUrl}/Admin/Comment/Index");
            new SelectElement(driver.FindElement(By.Name("comments_length"))).SelectByIndex(3);
            var buttons = driver.FindElements(By.CssSelector("a[class='btn btn-danger']"));
            buttons.ElementAt(random.Next(0, buttons.Count)).Click();
        }

        [Fact]
        public void Block()
        {
            var random = new Random();
            driver.LoginAsAdmin();
            driver.Navigate().GoToUrl($"{Config.AppUrl}/Admin/Comment/Index");
            new SelectElement(driver.FindElement(By.Name("comments_length"))).SelectByIndex(3);
            var buttons = driver.FindElements(By.CssSelector("a[class='btn btn-warning']"));
            buttons.ElementAt(random.Next(0, buttons.Count)).Click();
        }

        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}
