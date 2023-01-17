using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace eCommerce.AutomatedUITests.Admin
{
    public class CategoryTest : IDisposable
    {
        private readonly IWebDriver driver;

        public CategoryTest() => driver = Config.ChromeDriver;

        [Fact]
        public void Add()
        {
            var random = new Random();
            driver.LoginAsAdmin();
            driver.Navigate().GoToUrl($"{Config.AppUrl}/Admin/Category/Add");
            if (random.Next(0, 2) % 2 == 0)
            {
                var category = new SelectElement(driver.FindElement(By.Id("ParentCategoryId")));
                category.SelectByIndex(random.Next(1, category.Options.Count));
            }
            driver.FindElement(By.Id("Name")).ClearAndSendValue($"test category {DateTime.Now.Millisecond}");
            driver.FindElement(By.Id("Add")).Click();
        }

        [Fact]
        public void Delete()
        {
            driver.LoginAsAdmin();
            driver.Navigate().GoToUrl($"{Config.AppUrl}/Admin/Category/Index");
            new SelectElement(driver.FindElement(By.Name("categories_length"))).SelectByIndex(3);
            driver.ClickElementRandomly("a[class='btn btn-danger']");
        }

        [Fact]
        public void Update()
        {
            var random = new Random();
            driver.LoginAsAdmin();
            driver.Navigate().GoToUrl($"{Config.AppUrl}/Admin/Category/Index");
            new SelectElement(driver.FindElement(By.Name("categories_length"))).SelectByIndex(3);
            driver.ClickElementRandomly("a[class='btn btn-success']");
            if (random.Next(0, 2) % 2 == 0)
            {
                var element = driver.FindElement(By.Id("NewParentCategoryId"));
                if (element.Enabled)
                {
                    var category = new SelectElement(element);
                    category.SelectByIndex(random.Next(1, category.Options.Count));
                }
            }
            driver.FindElement(By.Id("Name")).ClearAndSendValue($"update category {DateTime.Now.Millisecond}");
            driver.FindElement(By.Id("Update")).Click();
        }

        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}
