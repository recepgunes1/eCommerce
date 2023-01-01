using OpenQA.Selenium;

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
    }
}
