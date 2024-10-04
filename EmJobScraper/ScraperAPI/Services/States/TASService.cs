using Common.Models;
using Common.Models.Enums;
using Microsoft.AspNetCore.SignalR;
using OpenQA.Selenium;
using ScraperAPI.Controllers;
using ScraperAPI.Utilities;
namespace ScraperAPI.Services.States
{
    public class TASService : StateService
    {
        public TASService(PageGenerator pageGenerator, IHubContext<JobHub> hub) : base(pageGenerator, hub)
        {
        }

        protected override async Task<List<JobInputModel>> ExtractIndex()
        {
            List<JobInputModel> jobs = new List<JobInputModel>();

            var moreButton = CurrentPage.GetElements(By.XPath("//*[@id=\"recent-jobs\"]/div[2]/a")).First();
            while (moreButton.GetAttribute("style") != "display: none;")
            {
                moreButton.Click();
                await Task.Delay(1000);
                moreButton = CurrentPage.GetElements(By.XPath("//*[@id=\"recent-jobs\"]/div[2]/a")).First();
            }

            var cards = CurrentPage.GetElements(By.CssSelector("#recent-jobs-content > .jobCard"));

            foreach (var card in cards)
            {
                var TitleElement = card.FindElement(By.CssSelector(".job-link"));
                string title = TitleElement.Text;
                string url = TitleElement.GetAttribute("href");

                JobInputModel job = new JobInputModel();
                job.Title = title;
                job.Url = url;

                var rows = card.FindElements(By.CssSelector(".jobsTableDisplay > .jobsRow"));

                foreach (var row in rows)
                {
                    string header = row.FindElement(By.CssSelector("h3")).Text;
                    string value = row.FindElement(By.CssSelector("div")).Text;

                    if (header == "Applications close:")
                    {
                        if (value.Contains("AEDT")) value = value.Replace("AEDT", "");
                        if (value.Contains("AEST")) value = value.Replace("AEST", "");
                        job.ClosureDate = value.Trim();
                    } else if (header == "Agency:")
                    {
                        job.Organization = value;
                    } else if (header == "Salary:")
                    {
                        job.Salary = RegexHelper.GetSalary(value);
                    } else if (header == "Location:")
                    {
                        job.Location = value;
                    }
                }
                job.State = State.TAS;
                jobs.Add(job);
            }
            return jobs;
        }

        protected override async Task<JobInputModel> ExtractJob(JobInputModel job)
        {
            return job;
        }

        protected override bool HasNextPage()
        {
            return false;
        }

        protected override async Task NextPage()
        {
            throw new NotImplementedException();
        }

        protected override async Task Search(string query)
        {
            CurrentPage = PageGenerator.NewPage(State.TAS.GetBaseLink());
        }
    }
}
