using OpenQA.Selenium;
using ScraperAPI.Utilities;
using Common.Models;
using Common.Models.Enums;
using Microsoft.AspNetCore.SignalR;
using ScraperAPI.Controllers;

namespace ScraperAPI.Services.States
{
    public class NSWService : StateService
    {
        private IReadOnlyCollection<IWebElement> _buttons;
        public NSWService(PageGenerator pageGenerator, IHubContext<JobHub> hub) : base(pageGenerator, hub)
        {
        }

        protected override async Task<List<JobInputModel>> ExtractIndex()
        {
            List<JobInputModel> index = new List<JobInputModel>();
            List<IWebElement> cards = CurrentPage.GetElements(By.XPath("//*[@id=\"cmsHomeUpperContent\"]/div[2]/div[3]/div[1]/div"));
            foreach (var card in cards)
            {
                JobInputModel job = new JobInputModel();
                var titleURL = card.FindElement(By.XPath("div[1]/a"));
                job.Url = titleURL.GetAttribute("href");
                try
                {
                    var titleSpan = titleURL.FindElement(By.CssSelector("span"));
                    job.Title = titleSpan.Text;
                }
                catch
                {
                    // do nothing
                }
                try
                {
                    var dateElement = card.FindElement(By.XPath("div[2]/div/div[1]/div/p[1]"));
                    string fullText = dateElement.Text;
                    string closing = fullText.Split("-")[1].Trim().Split(":")[1].Trim();
                    job.ClosureDate = closing;
                }
                catch
                {
                    // do nothing
                }
                try
                {
                    var orgElement = card.FindElement(By.XPath("div[2]/div/div[2]/div/h2"));
                    string org = orgElement.Text;
                    job.Organization = org;
                }
                catch
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
            await Task.Run(() => { Thread.Sleep(10000); });

            CurrentPage.SelectElement(By.XPath("//*[@id=\"main-content\"]/div/div[1]/div[3]/div/div[2]/h1"));
            string title = CurrentPage.GetText();

            if (string.IsNullOrEmpty(job.Title)) job.Title = title;

            var table = CurrentPage.GetElements(By.XPath("//*[@id=\"main-content\"]/div/div[1]/div[3]/div/div[2]/div[2]/div/table/tbody/tr"));
            bool foundSalary = false;
            foreach (var row in table)
            {
                var cells = row.FindElements(By.CssSelector("td"));
                string header = cells[0].Text;
                string val = cells[1].Text;

                if (header == "Organisation / Entity:")
                {
                    job.Organization = val;
                }
                else if (header == "Job location:")
                {
                    job.Location = val;
                }
                else if (header == "Closing date:")
                {
                    job.ClosureDate = val;
                }
                else if (header == "Total remuneration package:")
                {
                    try
                    {
                        string sal = RegexHelper.GetSalary(val);
                        if (sal != null)
                        {
                            job.Salary = sal;
                            foundSalary = true;
                        }
                    }
                    catch
                    {
                        //do nothin
                    }
                }

                if (!foundSalary)
                {
                    CurrentPage.SelectElement(By.XPath("//*[@id=\"main-content\"]/div/div[1]/div[3]/div/div[2]/div[3]"));
                    string text = CurrentPage.GetText();
                    try
                    {
                        string sal = RegexHelper.GetSalary(text);
                        if (sal != null)
                        {
                            job.Salary = sal;
                            foundSalary = true;
                        }

                    }
                    catch (Exception ex)
                    {
                        if (text.Contains("$78.28 p/fortnight + $33.14 p/hour"))
                        {
                            job.Salary = "$78.28 p/fortnight + $33.14 p/hour";
                            foundSalary = true;
                        }
                    }
                    if (!foundSalary)
                    {
                        job.Salary = "Not Provided";
                    }
                }
            }
            job.State = State.NSW;
            return job;
        }

        protected override bool HasNextPage()
        {
            var next = _buttons.ElementAt(_buttons.Count - 2);
            if (next.GetAttribute("class").Contains("disabled")) return false;
            return true;
        }

        protected override async Task NextPage()
        {
            var next = _buttons.ElementAt(_buttons.Count - 2);
            next.Click();
            await Task.Run(() => { Thread.Sleep(5000); });
            _buttons = CurrentPage.GetElements(By.XPath("//*[@id=\"cmsHomeUpperContent\"]/div[2]/div[1]/div[1]/div[1]/div[1]/button"));
        }

        protected override async Task Search(string query)
        {
            CurrentPage = PageGenerator.NewPage(State.NSW.GetBaseLink());
            await Task.Run(() => { Thread.Sleep(2000); });
            _buttons = CurrentPage.GetElements(By.XPath("//*[@id=\"cmsHomeUpperContent\"]/div[2]/div[1]/div[1]/div[1]/div[1]/button"));

        }
    }
}
