using Common.Models;
using Common.Models.Enums;
using Microsoft.AspNetCore.SignalR;
using OpenQA.Selenium;
using ScraperAPI.Controllers;
using ScraperAPI.Utilities;

namespace ScraperAPI.Services.States
{
    public class QLDService : StateService
    {
        public QLDService(PageGenerator pageGenerator, IHubContext<JobHub> hub) : base(pageGenerator, hub)
        {
        }

        protected override async Task<List<JobInputModel>> ExtractIndex()
        {
            List<JobInputModel> index = new List<JobInputModel>();
            List<IWebElement> cards = new List<IWebElement>();
            cards = CurrentPage.GetElements(By.XPath("//*[@id=\"content\"]/div/div/div/form[1]/ol/li"));
            foreach (IWebElement card in cards)
            {
                JobInputModel job = new JobInputModel();

                var linkElement = card.FindElement(By.XPath("h3/a"));
                string link = linkElement.GetAttribute("href");
                job.Url = link;

                try
                {
                    var titleOrgElement = linkElement.FindElement(By.XPath("span"));
                    var titleOrg = titleOrgElement.Text.Split(",");
                    job.Title = titleOrg[0];
                    job.Organization = titleOrg[1];
                }
                catch (Exception ex)
                {
                    // do nothing
                }
                try
                {
                    var dateElement = card.FindElement(By.XPath("div[2]/time"));
                    string dateText = dateElement.Text.Split(" ")[1];
                }
                catch (Exception ex)
                {
                    // do nothing
                }


                index.Add(job);

            }
            return index;
        }

        protected override async Task<JobInputModel> ExtractJob(JobInputModel job)
        {
            CurrentPage = PageGenerator.NewPage(job.Url);
            JobInputModel newJob = job;
            CurrentPage.SelectElement(By.CssSelector("#content > div > div > div > h1:nth-child(5)"));
            string title = CurrentPage.GetText();
            if (newJob.Title == null) newJob.Title = title;

            var details = CurrentPage.GetElements(By.XPath("//*[@id=\"details\"]/table/tbody/tr"));
            foreach (IWebElement row in details)
            {
                var head = row.FindElement(By.XPath("th")).Text;
                var value = row.FindElement(By.XPath("td")).Text;

                if (head == "Workplace Location")
                {
                    newJob.Location = value;
                }
                else if (head == "Closing date")
                {
                    newJob.ClosureDate = value;
                }
                else if (head == "Yearly salary")
                {
                    newJob.Salary = RegexHelper.GetSalary(value);
                }
                newJob.State = State.QLD;
            }
            return newJob;
        }

        protected override bool HasNextPage()
        {
            try
            {
                var container = CurrentPage.GetElements(By.CssSelector("#pagination > ul > li.next")).First();
                var element = container.FindElement(By.CssSelector("input"));
                string buttonValue = element.GetAttribute("value");
                if (string.IsNullOrEmpty(buttonValue) || buttonValue == "0") return false;
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        protected override async Task NextPage()
        {
            CurrentPage.SelectElement(By.CssSelector("#pagination > ul > li.next"));
            await CurrentPage.Click();

        }

        protected override async Task Search(string query)
        {
            CurrentPage = PageGenerator.NewPage(State.QLD.GetBaseLink());
            await Task.Run(() => { Thread.Sleep(3000); });

            CurrentPage.SelectElement(By.XPath("//*[@id=\"keyword\"]"));
            CurrentPage.Type(query);
            CurrentPage.SelectElement(By.CssSelector("#searchBtn"));
            await CurrentPage.Click();
        }
    }
}
