using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace eCommerce.AutomatedUITests.Shopping
{
    public class ShoppingTest : IDisposable
    {
        private readonly IWebDriver driver;
        public ShoppingTest() => driver = new ChromeDriver(Config.DriverDirectory);

        [Fact]
        public void BuyProduct()
        {
            var random = new Random();
            driver.LoginAsAdmin();
            var products = driver.FindElement(By.Id("products")).FindElements(By.ClassName("list-group-item"));
            while(true)
            {
                var product = products.ElementAt(random.Next(0, products.Count()));
                product.SendKeys(Keys.Enter);
                var button = driver.FindElement(By.Id("Buy"));
                if (button.Enabled)
                {
                    button.SendKeys(Keys.Enter);
                    break;
                }
                driver.Navigate().Back();
            }
            driver.Navigate().GoToUrl($"{Config.AppUrl}/Cart/Home/Cart");
            driver.FindElement(By.Id("Payment")).Click();
            driver.FindElement(By.Id("HolderName")).ClearAndSendValue("test method");
            driver.FindElement(By.Id("CardNumber")).ClearAndSendValue("123456789123");
            driver.FindElement(By.Id("CVV")).ClearAndSendValue("123");
            var month = new SelectElement(driver.FindElement(By.Id("Month")));
            month.SelectByIndex(random.Next(1, month.Options.Count));
            var year = new SelectElement(driver.FindElement(By.Id("Year")));
            year.SelectByIndex(random.Next(1, year.Options.Count));
            driver.FindElement(By.Id("Buy")).Click();
        }

        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}
