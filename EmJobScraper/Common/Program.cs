using Common.Models;
using Common.Services;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

BrowserService browser = await BrowserService.getService();
List<JobLink> links = browser.getJobLinks();
foreach (JobLink link in links)
{
    Console.WriteLine($"{link.Title} - {link.Url}");
}
