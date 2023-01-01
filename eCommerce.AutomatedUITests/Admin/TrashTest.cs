using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace eCommerce.AutomatedUITests.Admin
{
    public class TrashTest : IDisposable
    {
        private readonly IWebDriver driver;

        public TrashTest() => driver = new ChromeDriver(Config.DriverDirectory);

        [Fact]
        public void RestoreBrand()
        {
            var random = new Random();
            driver.LoginAsAdmin();
            driver.Navigate().GoToUrl($"{Config.AppUrl}/Admin/Trash/Brands");
            new SelectElement(driver.FindElement(By.Name("brands_length"))).SelectByIndex(3);
            var buttons = driver.FindElements(By.CssSelector("a[class='btn btn-warning']"));
            buttons.ElementAt(random.Next(0, buttons.Count)).Click();
        }

        [Fact]
        public void RestoreCategory()
        {
            var random = new Random();
            driver.LoginAsAdmin();
            driver.Navigate().GoToUrl($"{Config.AppUrl}/Admin/Trash/Categories");
            new SelectElement(driver.FindElement(By.Name("categories_length"))).SelectByIndex(3);
            var buttons = driver.FindElements(By.CssSelector("a[class='btn btn-warning']"));
            buttons.ElementAt(random.Next(0, buttons.Count)).Click();
        }

        [Fact]
        public void RestoreComments()
        {
            var random = new Random();
            driver.LoginAsAdmin();
            driver.Navigate().GoToUrl($"{Config.AppUrl}/Admin/Trash/Comments");
            new SelectElement(driver.FindElement(By.Name("comments_length"))).SelectByIndex(3);
            var buttons = driver.FindElements(By.CssSelector("a[class='btn btn-warning']"));
            buttons.ElementAt(random.Next(0, buttons.Count)).Click();
        }

        [Fact]
        public void RestoreProducts()
        {
            var random = new Random();
            driver.LoginAsAdmin();
            driver.Navigate().GoToUrl($"{Config.AppUrl}/Admin/Trash/Products");
            new SelectElement(driver.FindElement(By.Name("products_length"))).SelectByIndex(3);
            var buttons = driver.FindElements(By.CssSelector("a[class='btn btn-warning']"));
            buttons.ElementAt(random.Next(0, buttons.Count)).Click();
        }

        [Fact]
        public void RestoreUsers()
        {
            var random = new Random();
            driver.LoginAsAdmin();
            driver.Navigate().GoToUrl($"{Config.AppUrl}/Admin/Trash/Users");
            new SelectElement(driver.FindElement(By.Name("users_length"))).SelectByIndex(3);
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
