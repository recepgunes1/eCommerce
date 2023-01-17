using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace eCommerce.AutomatedUITests.Admin
{
    public class CommentTest : IDisposable
    {
        private readonly IWebDriver driver;

        public CommentTest() => driver = Config.ChromeDriver;

        [Fact]
        public void Delete()
        {
            driver.LoginAsAdmin();
            driver.Navigate().GoToUrl($"{Config.AppUrl}/Admin/Comment/Index");
            new SelectElement(driver.FindElement(By.Name("comments_length"))).SelectByIndex(3);
            driver.ClickElementRandomly("a[class='btn btn-danger']");
        }

        [Fact]
        public void Block()
        {
            driver.LoginAsAdmin();
            driver.Navigate().GoToUrl($"{Config.AppUrl}/Admin/Comment/Index");
            new SelectElement(driver.FindElement(By.Name("comments_length"))).SelectByIndex(3);
            driver.ClickElementRandomly("a[class='btn btn-warning']");
        }

        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}
