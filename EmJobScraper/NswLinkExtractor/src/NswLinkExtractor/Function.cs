using Amazon.Lambda.Core;
using NswLinkExtractor.Models.Enums;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace NswLinkExtractor
{
    public class Function
    {

        /// <summary>
        /// A simple function that takes a string and returns both the upper and lower case version of the string.
        /// </summary>
        /// <param name="input">The event for the Lambda function handler to process.</param>
        /// <param name="context">The ILambdaContext that provides methods for logging and describing the Lambda environment.</param>
        /// <returns></returns>
        public async Task<string> FunctionHandler(ILambdaContext context)
        {
            //ChromeOptions opts = new ChromeOptions();
            //opts.AddArgument("--verbose");
            //opts.AddArgument("--headless");
            //opts.AddArgument("--disable-dev-shm-usage");
            //WebDriver web = new ChromeDriver(opts);

            //await web.Navigate().GoToUrlAsync(State.NSW.getBaseLink());

            //IWebElement element = web.FindElement(By.XPath("//*[@id=\"cmsHomeUpperContent\"]/div[2]/div[3]/div[1]/div[1]/div[1]/a/span"));
            //string title = element.Text;
            return "hello world";
        }
    }

}