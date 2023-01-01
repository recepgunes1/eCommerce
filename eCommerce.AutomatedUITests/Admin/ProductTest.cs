using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace eCommerce.AutomatedUITests.Admin
{
    public class ProductTest : IDisposable
    {
        private readonly IWebDriver driver;

        public ProductTest() => driver = new ChromeDriver(Config.DriverDirectory);

        [Fact]
        public void Add()
        {
            var random = new Random();
            driver.LoginAsAdmin();
            driver.Navigate().GoToUrl($"{Config.AppUrl}/Admin/Product/Add");
            driver.FindElement(By.Id("Name")).ClearAndSendValue($"test product {DateTime.Now.Millisecond}");
            driver.FindElement(By.Id("Description")).ClearAndSendValue("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam tempus venenatis viverra." +
                "Mauris commodo diam at malesuada fermentum. Donec interdum erat odio, sed hendrerit dolor fringilla quis. Vestibulum volutpat ullamcorper.");
            driver.FindElement(By.Id("Quantity")).ClearAndSendValue(random.Next(0, 20).ToString());
            driver.FindElement(By.Id("Price")).ClearAndSendValue(random.Next(1000, 20000).ToString());
            var category = new SelectElement(driver.FindElement(By.Id("CategoryId")));
            category.SelectByIndex(random.Next(1, category.Options.Count));
            var brand = new SelectElement(driver.FindElement(By.Id("BrandId")));
            brand.SelectByIndex(random.Next(1, brand.Options.Count));
            driver.FindElement(By.Id("Photo")).ClearAndSendValue(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "phone.png"));
            driver.FindElement(By.Id("Add")).Click();
        }

        [Fact]
        public void Delete()
        {
            var random = new Random();
            driver.LoginAsAdmin();
            driver.Navigate().GoToUrl($"{Config.AppUrl}/Admin/Product/Index");
            new SelectElement(driver.FindElement(By.Name("products_length"))).SelectByIndex(3);
            var buttons = driver.FindElements(By.CssSelector("a[class='btn btn-danger']"));
            buttons.ElementAt(random.Next(0, buttons.Count)).Click();
        }

        [Fact]
        public void Update()
        {
            var random = new Random();
            driver.LoginAsAdmin();
            driver.Navigate().GoToUrl($"{Config.AppUrl}/Admin/Product/Index");
            new SelectElement(driver.FindElement(By.Name("products_length"))).SelectByIndex(3);
            var buttons = driver.FindElements(By.CssSelector("a[class='btn btn-success']"));
            buttons.ElementAt(random.Next(0, buttons.Count)).Click();
            driver.FindElement(By.Id("Name")).ClearAndSendValue($"update product {DateTime.Now.Millisecond}");
            driver.FindElement(By.Id("Description")).ClearAndSendValue("It seemed like it should have been so simple. There was nothing inherently difficult with getting the project done." +
                "It was simple and straightforward enough that even a child should have been able to complete it on time, but that wasn't the case. ");
            driver.FindElement(By.Id("Quantity")).ClearAndSendValue(random.Next(0, 20).ToString());
            driver.FindElement(By.Id("Price")).ClearAndSendValue(random.Next(1000, 20000).ToString());
            var category = new SelectElement(driver.FindElement(By.Id("CategoryId")));
            category.SelectByIndex(random.Next(1, category.Options.Count));
            var brand = new SelectElement(driver.FindElement(By.Id("BrandId")));
            brand.SelectByIndex(random.Next(1, brand.Options.Count));
            driver.FindElement(By.Id("Photo")).ClearAndSendValue(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "phone.png"));
            driver.FindElement(By.Id("Update")).Click();
        }

        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}
