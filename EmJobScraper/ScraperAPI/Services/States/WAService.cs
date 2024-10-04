using Common.Models;
using Common.Models.Enums;
using Microsoft.AspNetCore.SignalR;
using OpenQA.Selenium;
using ScraperAPI.Controllers;
using ScraperAPI.Utilities;
namespace ScraperAPI.Services.States
{
    public class WAService : StateService
    {
        public WAService(PageGenerator pageGenerator, IHubContext<JobHub> hub) : base(pageGenerator, hub)
        {
        }

        protected override async Task<List<JobInputModel>> ExtractIndex()
        {
            List<JobInputModel> index = new List<JobInputModel>();
            var rows = CurrentPage.GetElements(By.XPath("//*[@id=\"customcontent\"]/form/div/table/tbody/tr"));
            for (int i = 1; i < rows.Count; i++)
            {
                JobInputModel job = new JobInputModel();
                var row = rows[i];
                var titleElement = row.FindElement(By.XPath("td[3]/a"));
                string title = titleElement.Text;
                string url = titleElement.GetAttribute("href");

                string closingDate = row.FindElement(By.XPath("td[2]")).Text;
                string agency = row.FindElement(By.XPath("td[5]")).Text;
                string classification = row.FindElement(By.XPath("td[6]")).Text;
                string location = row.FindElement(By.XPath("td[8]")).Text;

                job.Title = title;
                job.Url = url;
                job.ClosureDate = closingDate;
                job.Organization = agency.Replace(", Department of", "");
                job.Salary = RegexHelper.GetSalary(classification);
                job.Location = location;
                job.State = State.WA;
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
                var container = CurrentPage.GetElements(By.XPath("//*[@id=\"customcontent\"]/form/table[2]/tbody/tr/td[3]")).First();
                string width = container.GetAttribute("width");
                string align = container.GetAttribute("align");
                int eleCount = 0;
                eleCount = container.FindElements(By.CssSelector("a")).Count();
                return eleCount > 0 && width == "20%" && align == "right";
            } catch (Exception ex)
            {
                return false;
            }
        }

        protected override async Task NextPage()
        {
            CurrentPage.SelectElement(By.XPath("//*[@id=\"customcontent\"]/form/table[2]/tbody/tr/td[3]/a[1]"));
            await CurrentPage.Click();
        }

        protected override async Task Search(string query)
        {
            CurrentPage = PageGenerator.NewPage(State.WA.GetBaseLink());
            await Task.Delay(3000);
            CurrentPage.SelectElement(By.XPath("//*[@id=\"Data713\"]"));
            
            CurrentPage.Type(query);

            CurrentPage.SelectElement(By.XPath("//*[@id=\"searchButton\"]"));
            await CurrentPage.Click();
        }
    }
}
