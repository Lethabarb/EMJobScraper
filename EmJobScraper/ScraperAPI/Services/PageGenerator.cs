using OpenQA.Selenium.Chrome;

namespace ScraperAPI.Services
{
    public class PageGenerator
    {
        private static ChromeDriver ChromeDriver { get; set; }

        public PageGenerator(ChromeDriver driver)
        {
            if (driver == null) return;
            ChromeDriver = driver;
        }

        public static Page NewPage(string uri)
        {
            var handles = ChromeDriver.WindowHandles;
            for (int i = 1; i < handles.Count; i++)
            {
                string handle = handles[i];
                ChromeDriver.SwitchTo().Window(handle);
                ChromeDriver.Close();

            }
            ChromeDriver.SwitchTo().Window(ChromeDriver.WindowHandles.First());
            return new Page(ChromeDriver, uri);
        }
        public static void newDriver()
        {
            ChromeDriver.Quit();

            ChromeOptions opts = new ChromeOptions();
            opts.AddArgument("--headless");
            opts.AddArgument("--no-sandbox");
            opts.AddArgument("--disable-gpu");
            opts.AddArgument("--disable-dev-shm-usage");

            ChromeDriverService chromeService = ChromeDriverService.CreateDefaultService();
            chromeService.SuppressInitialDiagnosticInformation = true;

            ChromeDriver = new ChromeDriver(chromeService, opts);
        }
    }
}
