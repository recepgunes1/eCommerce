using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace eCommerce.AutomatedUITests
{
    public static class Extensions
    {
        public static void ClearAndSendValue(this IWebElement element, string text)
        {
            element.Clear();
            element.SendKeys(text);
        }

        public static void LoginAsAdmin(this IWebDriver driver)
        {
            driver.Navigate().GoToUrl($"{Config.AppUrl}/Auth/Home/Login");
            driver.FindElement(By.Id("Email")).ClearAndSendValue("admin@system.com");
            driver.FindElement(By.Id("Password")).ClearAndSendValue("123456");
            driver.FindElement(By.Id("SignIn")).Click();
        }

        public static void ClickElementRandomly(this IWebDriver driver, string cssSelector)
        {
            var random = new Random();
            var buttons = driver.FindElements(By.CssSelector(cssSelector));
            if (buttons.Any())
            {
                var button = buttons.ElementAt(random.Next(0, buttons.Count));
                if (button != null)
                {
                    WebDriverWait wait = new(driver, TimeSpan.FromSeconds(20));
                    wait.Until(ExpectedConditions.ElementToBeClickable(button));
                    button.Click();
                }
            }
        }
    }
}
