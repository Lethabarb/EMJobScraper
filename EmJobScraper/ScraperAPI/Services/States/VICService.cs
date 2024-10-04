using Common.Models;
using Common.Models.Enums;
using Microsoft.AspNetCore.SignalR;
using OpenQA.Selenium;
using ScraperAPI.Controllers;
using ScraperAPI.Utilities;
namespace ScraperAPI.Services.States
{
    public class VICService : StateService
    {
        public VICService(PageGenerator pageGenerator, IHubContext<JobHub> hub) : base(pageGenerator, hub)
        {
        }

        protected override async Task<List<JobInputModel>> ExtractIndex()
        {
            var cards = CurrentPage.GetElements(By.CssSelector("#bind_search_result > div"));
            var index = new List<JobInputModel>();
            foreach (var card in cards)
            {
                JobInputModel job = new JobInputModel();

                var titleElement = card.FindElement(By.XPath("div[1]/a"));
                string title = titleElement.Text;
                string url = titleElement.GetAttribute("href");

                job.Title = title;
                job.Url = url;

                var rows = card.FindElements(By.XPath("div[2]/div/div/div"));

                foreach (var row in rows)
                {
                    if (row.GetAttribute("class").Contains("show-mobile")) continue;
                    var cells = row.FindElements(By.CssSelector("div"));
                    foreach (var cell in cells)
                    {
                        //var element = row.FindElement(By.XPath("p[1]"));//*[@id="bind_search_result"]/div/div[2]/div[1]/div/div[3]/div[1]/p[1]
                        string text = cell.Text;
                        string type = text.Split(":")[0];
                        string val = text.Split(":")[1].Trim();

                        if (type == "Application close date")
                        {
                            job.ClosureDate = val;
                        }
                        else if (type == "Organisation")
                        {
                            job.Organization = val;
                        }
                        else if (type == "Salary")
                        {
                            job.Salary = val;
                        }
                        else if (type == "Location")
                        {
                            job.Location = val;
                        }
                    }
                }
                job.State = State.VIC;
                index.Add(job);
            }
            return index;
        }

        protected override async Task<JobInputModel> ExtractJob(JobInputModel job)
        {
            return job;
        }

        protected override bool HasNextPage()
        {
            try
            {
                var buttons = CurrentPage.GetElements(By.CssSelector("#paging-nav > ul > li"));
                if (buttons.Count > 2)
                {
                    var button = buttons[buttons.Count - 2];
                    if (button.GetAttribute("aria-label") == "Go to next page") return true;
                }
            } catch (Exception ex)
            {
                //do nothing
            }
            return false;
        }

        protected override async Task NextPage()
        {
            var buttons = CurrentPage.GetElements(By.CssSelector("#paging-nav > ul > li"));
            if (buttons.Count > 2)
            {
                var button = buttons[buttons.Count - 2];
                button.Click();
                await Task.Delay(3000);
            }
        }

        protected override async Task Search(string query)
        {
            CurrentPage = PageGenerator.NewPage(State.VIC.GetBaseLink());
            CurrentPage.SelectElement(By.CssSelector("#search_text"));
            CurrentPage.Type(query);

            CurrentPage.SelectElement(By.CssSelector("#btnsearch1"));
            await CurrentPage.Click();

        }
    }
}
