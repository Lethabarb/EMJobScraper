using Common.Entities;
using Common.Models;
using Common.Models.Enums;
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
        private StateLinkReader linkReader;
        private StateJobReader jobReader;
        private State state;
        private BrowserService()
        {
            driver = new FirefoxDriver();
        }

        public static async Task<BrowserService> getService()
        {
            BrowserService s = new BrowserService();
            return s;
        }

        public async Task nextState(State s)
        {
            if (driver == null) throw new NullReferenceException();
            state = s;
            await initState(s);
        }
        public async Task initState(State state)
        {
            try
            {
                await driver.Navigate().GoToUrlAsync(state.getBaseLink());
                linkReader = state.GetLinkReader();
                jobReader = state.GetJobReader();
            }
            catch (WebDriverException e) {
                driver.Quit();
                throw new WebDriverException("",e);
            }
        }
        public List<JobLink> getJobLinks()
        {
            JobReader reader = new JobReader(driver);

            JobReader cards = reader.AddChild(linkReader.JobCard);
            cards.AddAttrChild(linkReader.Url, JobAttribute.Link);
            cards.AddTextChild(linkReader.Title, JobAttribute.Title);

            Dictionary<JobAttribute?, List<string>> data = reader.GetData();
            
            List<string> titles = data[JobAttribute.Title];
            List<string> urls = data[JobAttribute.Link];
            List<JobLink> links = new List<JobLink>();

            if (titles.Count != urls.Count) throw new FormatException();
            
            for (int i = 0; i < titles.Count;  i++)
            {
                JobLink link = new JobLink(titles[i], urls[i], state);
                links.Add(link);
            }

            return links;
        }

        public List<Job> GetJobs(List<JobLink> jobLinks)
        {
            throw new NotImplementedException();
        }

        public async Task<Job> ReadPage(JobLink link)
        {
            await driver.Navigate().GoToUrlAsync(link.Url);
            JobReader reader = new JobReader(driver);

            return null;
        }
    }
}
