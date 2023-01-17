using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace eCommerce.AutomatedUITests.Admin
{
    public class BrandTest : IDisposable
    {
        private readonly IWebDriver driver;

        public BrandTest() => driver = Config.ChromeDriver;

        [Fact]
        public void Add()
        {
            driver.LoginAsAdmin();
            driver.Navigate().GoToUrl($"{Config.AppUrl}/Admin/Brand/Add");
            driver.FindElement(By.Id("Name")).ClearAndSendValue($"test brand {DateTime.Now.Millisecond}");
            driver.FindElement(By.Id("Photo")).ClearAndSendValue(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "brand.png"));
            driver.FindElement(By.Id("Add")).Click();
        }

        [Fact]
        public void Delete()
        {
            driver.LoginAsAdmin();
            driver.Navigate().GoToUrl($"{Config.AppUrl}/Admin/Brand/Index");
            new SelectElement(driver.FindElement(By.Name("brands_length"))).SelectByIndex(3);
            driver.ClickElementRandomly("a[class='btn btn-danger']");
        }

        [Fact]
        public void Update()
        {
            driver.LoginAsAdmin();
            driver.Navigate().GoToUrl($"{Config.AppUrl}/Admin/Brand/Index");
            new SelectElement(driver.FindElement(By.Name("brands_length"))).SelectByIndex(3);
            driver.ClickElementRandomly("a[class='btn btn-success']");
            driver.FindElement(By.Id("Name")).ClearAndSendValue($"update brand {DateTime.Now.Millisecond}");
            driver.FindElement(By.Id("Photo")).ClearAndSendValue(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "brand.png"));
            driver.FindElement(By.Id("Update")).Click();
        }

        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}
