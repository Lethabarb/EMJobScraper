using Amazon.Lambda.Core;
using NswLinkExtractor.Models.Enums;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;

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
            ChromeOptions opts = new ChromeOptions();
            opts.AddArguments("--headless", "--disable-gpu",
                "--no-sandbox", "--disable-dev-shm-usage");

            var chromeService = ChromeDriverService.CreateDefaultService();
            chromeService.SuppressInitialDiagnosticInformation = true;

            WebDriver web = new ChromeDriver(chromeService, opts);

            await web.Navigate().GoToUrlAsync("https://www.google.com");

            return web.Title;
        }
    }

}