using OpenQA.Selenium.Chrome;

namespace eCommerce.AutomatedUITests
{
    public static class Config
    {
        public readonly static string AppUrl = "https://localhost:7109";
        public readonly static string DriverDirectory = "http://172.22.80.1:4444/wd/hub";
        public static ChromeDriver ChromeDriver
        {
            get
            {
                var driver = new ChromeDriver(DriverDirectory);
                driver.Manage().Window.Maximize();
                return driver;
            }
        }
    }
}
