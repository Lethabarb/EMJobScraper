using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NswLinkExtractor.Services
{
    public class BrowserService
    {
        private WebDriver driver;

        public BrowserService()
        {
            driver = new FirefoxDriver();
        }
    }
}
