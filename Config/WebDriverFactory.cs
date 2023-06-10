using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace TestJamaTestMgmt.Config
{
    public static class WebDriverFactory
    {
        public static IWebDriver CreateWebDriver(Browser browser, string caminhoDriver, bool headless = true) 
        {
            IWebDriver webDriver = null;

            switch (browser)
            {
                case Browser.Chrome:
                    var optionsChrome = new ChromeOptions();
                    if (headless)
                        optionsChrome.AddArgument("--headless");

                    webDriver = new ChromeDriver(caminhoDriver, optionsChrome);
                    break;

                case Browser.Firefox:
                    var optionsFirefox = new FirefoxOptions();
                    if (headless)
                        optionsFirefox.AddArgument("--headless");

                    webDriver = new FirefoxDriver(caminhoDriver, optionsFirefox);

                    break;
            }

            return webDriver;
        }
    }
}
