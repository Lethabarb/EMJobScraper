using Common.Models;
using Common.Models.Enums;
using Microsoft.AspNetCore.SignalR;
using OpenQA.Selenium;
using ScraperAPI.Controllers;
using ScraperAPI.Utilities;

namespace ScraperAPI.Services.States
{
    public class ACTService : StateService
    {
        public ACTService(PageGenerator pageGenerator, IHubContext<JobHub> hub) : base(pageGenerator, hub)
        {
        }

        protected override async Task<List<JobInputModel>> ExtractIndex()
        {
            List<JobInputModel> index = new List<JobInputModel>();
            var cards = CurrentPage.GetElements(By.XPath("//*[@id=\"main\"]/div"));
            foreach (var card in cards)
            {
                JobInputModel job = new JobInputModel();
                string cssClass = card.GetAttribute("class");
                if (!cssClass.Contains("position") || card.GetAttribute("data-advertiseddate") == null) continue;

                var urlButton = card.FindElement(By.XPath("div[2]/a"));
                string url = urlButton.GetAttribute("href");
                job.Url = url;

                try
                {
                    var title = card.FindElement(By.CssSelector("h3"));
                    job.Title = title.Text;
                }
                catch (Exception ex)
                {
                    // do nothing
                }
                //try
                //{
                //}
                //catch (Exception ex)
                //{
                //    // do nothing
                //}
                index.Add(job);
            }
            return index;
        }

        protected override async Task<JobInputModel> ExtractJob(JobInputModel job)
        {
            CurrentPage = PageGenerator.NewPage(job.Url);
            await Task.Delay(3000);
            CurrentPage.SelectElement(By.XPath("//*[@id=\"main\"]/div[1]/div[1]/h3"));

            string closesFull = CurrentPage.GetText();
            string closesDate = RegexHelper.GetAfterColon(closesFull, "Closes").Trim();

            CurrentPage.SelectElement(By.XPath("//*[@id=\"main\"]/div[1]/div[1]/p[1]"));
            string data = CurrentPage.GetText();

            string salary = RegexHelper.GetSalary(data);
            string org = RegexHelper.GetAfterColon(data, "Directorate");

            job.ClosureDate = closesDate;
            job.Salary = salary;
            job.Organization = org;
            job.State = State.ACT;
            job.Location = "Canberra";
            return job;
        }

        protected override bool HasNextPage()
        {
            return false;
        }

        protected override async Task NextPage()
        {
            return;
        }

        protected override async Task Search(string query)
        {
            CurrentPage = PageGenerator.NewPage(State.ACT.GetBaseLink());
            await Task.Run(() => { Thread.Sleep(2000); });
        }
    }
}
