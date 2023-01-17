using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace eCommerce.AutomatedUITests.Admin
{
    public class TrashTest : IDisposable
    {
        private readonly IWebDriver driver;

        public TrashTest() => driver = Config.ChromeDriver;

        [Fact]
        public void RestoreBrand()
        {
            driver.LoginAsAdmin();
            driver.Navigate().GoToUrl($"{Config.AppUrl}/Admin/Trash/Brands");
            new SelectElement(driver.FindElement(By.Name("brands_length"))).SelectByIndex(3);
            driver.ClickElementRandomly("a[class='btn btn-warning']");
        }

        [Fact]
        public void RestoreCategory()
        {
            driver.LoginAsAdmin();
            driver.Navigate().GoToUrl($"{Config.AppUrl}/Admin/Trash/Categories");
            new SelectElement(driver.FindElement(By.Name("categories_length"))).SelectByIndex(3);
            driver.ClickElementRandomly("a[class='btn btn-warning']");
        }

        [Fact]
        public void RestoreComments()
        {
            driver.LoginAsAdmin();
            driver.Navigate().GoToUrl($"{Config.AppUrl}/Admin/Trash/Comments");
            new SelectElement(driver.FindElement(By.Name("comments_length"))).SelectByIndex(3);
            driver.ClickElementRandomly("a[class='btn btn-warning']");
        }

        [Fact]
        public void RestoreProducts()
        {
            driver.LoginAsAdmin();
            driver.Navigate().GoToUrl($"{Config.AppUrl}/Admin/Trash/Products");
            new SelectElement(driver.FindElement(By.Name("products_length"))).SelectByIndex(3);
            driver.ClickElementRandomly("a[class='btn btn-warning']");
        }

        [Fact]
        public void RestoreUsers()
        {
            driver.LoginAsAdmin();
            driver.Navigate().GoToUrl($"{Config.AppUrl}/Admin/Trash/Users");
            new SelectElement(driver.FindElement(By.Name("users_length"))).SelectByIndex(3);
            driver.ClickElementRandomly("a[class='btn btn-warning']");
        }

        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}
