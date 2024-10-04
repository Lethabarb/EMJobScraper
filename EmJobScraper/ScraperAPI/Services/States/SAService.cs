using Common.Models;
using Common.Models.Enums;
using Microsoft.AspNetCore.SignalR;
using OpenQA.Selenium;
using ScraperAPI.Controllers;
namespace ScraperAPI.Services.States
{
    public class SAService : StateService
    {
        public SAService(PageGenerator pageGenerator, IHubContext<JobHub> hub) : base(pageGenerator, hub)
        {
        }

        protected override async Task<List<JobInputModel>> ExtractIndex()
        {
            List<JobInputModel> jobs = new List<JobInputModel>();
            var rows = CurrentPage.GetElements(By.XPath("//*[@id=\"brs_report_table_16\"]/tbody/tr[2]/td/table/tbody/tr"));
            
            for (int i = 1; i < rows.Count; i++)
            {
                var cells = rows[i].FindElements(By.CssSelector("td"));

                var TitleElement = cells[0].FindElement(By.CssSelector("a"));
                string url = TitleElement.GetAttribute("href");
                string title = TitleElement.Text;

                string expDate = cells[3].Text;

                string agency = cells[4].Text;

                string salary = cells[6].Text;

                string location = cells[7].Text;

                JobInputModel job = new JobInputModel();
                job.Title = title;
                job.Url = url;
                job.State = State.SA;
                job.ClosureDate = expDate;
                job.Organization = agency;
                job.Salary = salary;
                job.Location = location;
                if (!string.IsNullOrEmpty(title) && !string.IsNullOrEmpty(url) && !string.IsNullOrEmpty(expDate) && !string.IsNullOrEmpty(agency) && !string.IsNullOrEmpty(salary) && !string.IsNullOrEmpty(location))
                {
                    jobs.Add(job);
                }
            }
            return jobs;
        }

        protected override async Task<JobInputModel> ExtractJob(JobInputModel job)
        {
            return job;
        }

        protected override bool HasNextPage()
        {
            try
            {
                var buttons = CurrentPage.GetElements(By.XPath("//*[@id=\"brs_report_table_16\"]/tbody/tr[3]/td/table/tbody/tr/td[3]/nobr/a"));
                if (buttons.Count > 0)
                {
                   return buttons[0].Text.Contains("Next");
                }
            } catch (Exception ex)
            {
                // do nothing
            }
            return false;
        }

        protected override async Task NextPage()
        {
            var buttons = CurrentPage.GetElements(By.XPath("//*[@id=\"brs_report_table_16\"]/tbody/tr[3]/td/table/tbody/tr/td[3]/nobr/a"));
            if (buttons.Count > 0)
            {
                buttons[0].Click();
                await Task.Delay(3000);
            }
        }

        protected override async Task Search(string query)
        {
            CurrentPage = PageGenerator.NewPage(State.SA.GetBaseLink());

            CurrentPage.SelectElement(By.XPath("//*[@id=\"BRSFormItem_3108\"]/td[2]/input"));
            CurrentPage.Type(query);

            CurrentPage.SelectElement(By.CssSelector("#brsSearchBtn"));
            await CurrentPage.Click();
            await Task.Run(() => { Thread.Sleep(5000); });
            try
            {
                CurrentPage.SelectElement(By.CssSelector("#switch_f"));
                await CurrentPage.Click();

            }catch (Exception e)
            {
                //do nothin
            }
        } 
    }
}
