using Common.Entities;
using Common.Models;
using Common.Models.Enums;
using Common.Models.InputModels;
using Common.Models.Readers;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Services
{
    public class BrowserService
    {
        private WebDriver driver;
        private State currentState;

        private BrowserService()
        {
            currentState = (State) 0;
            driver = new FirefoxDriver();
        }

        public static async Task<BrowserService> getService()
        {
            BrowserService s = new BrowserService();
            await s.initState((State) 0);
            return s;
        }

        public async Task nextState()
        {
            currentState = currentState++;
            await initState(currentState);
        }
        public async Task initState(State state)
        {
            await driver.Navigate().GoToUrlAsync(state.getBaseLink());
        }
        public List<JobLink> getJobLinks()
        {
            StateLinkReader reader = currentState.getLinkReader();
            ReadOnlyCollection<IWebElement> cards = driver.FindElements(reader.JobCard);
            List<JobLink> jobLinks = new List<JobLink>();
            foreach (IWebElement card in cards)
            {
                IWebElement titleElement = card.FindElement(reader.Title);
                IWebElement urlElement = card.FindElement(reader.Url);

                string title = titleElement.Text;
                string url = urlElement.GetAttribute("href");
                jobLinks.Add(new JobLink(title,url,currentState));
            }
            return jobLinks;
        }

        public List<Job> GetJobs(List<JobLink> jobLinks)
        {
            for
        }

        public async Task<Job> ReadPage(JobLink link)
        {
            StateJobReader reader = currentState.GetJobReader();
            IWebElement? title;
            IWebElement? location;
            IWebElement? organisation;
            IWebElement? closureDate;
            IWebElement? salary;
            IWebElement? table;

            await driver.Navigate().GoToUrlAsync(link.Url);
            if (reader.Title != null) title = driver.FindElement(reader.Title);
            if (reader.Location != null) location = driver.FindElement(reader.Location);
            if (reader.Organization != null) organisation = driver.FindElement(reader.Organization);
            if (reader.ClosureDate != null) closureDate = driver.FindElement(reader.ClosureDate);
            if (reader.Salary != null) salary = driver.FindElement(reader.Salary);
            if (reader.Table != null) table = driver.FindElement(reader.Table);



        }
    }
}
