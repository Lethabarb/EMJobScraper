using Common.Models;
using Common.Models.Enums;
using Common.Services;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

BrowserService browser = await BrowserService.getService();

await browser.nextState(State.NSW);

List<JobLink> links = browser.getJobLinks();
foreach (JobLink link in links)
{
    Console.WriteLine($"{link.Title} - {link.Url}");
}
