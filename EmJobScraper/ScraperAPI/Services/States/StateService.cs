using Common.Models;
using Microsoft.AspNetCore.SignalR;
using OpenQA.Selenium;
using ScraperAPI.Controllers;
using ScraperAPI.Utilities;

namespace ScraperAPI.Services.States
{
    public abstract class StateService
    {
        protected Page CurrentPage;
        protected PageGenerator PageGenerator { get; private set; }
        public StateService(PageGenerator pageGenerator, IHubContext<JobHub> hub)
        {
            PageGenerator = pageGenerator;
            _hub = hub;
        }
        private readonly IHubContext<JobHub> _hub;
        public List<JobInputModel> Jobs { get; set; } = new List<JobInputModel>();
        public List<JobInputModel> errors { get; set; } = new List<JobInputModel>();
        protected abstract Task NextPage();
        protected abstract bool HasNextPage();
        protected abstract Task Search(string query);
        protected abstract Task<List<JobInputModel>> ExtractIndex();
        protected abstract Task<JobInputModel> ExtractJob(JobInputModel job);
        private readonly List<string> _filters = new List<string> {"education", "teach", "legal", "regrister", "executive", "school",
            "technology", "cleric", "payroll", "client", "admin", "data", "engineer", "motor", "mechanic", "health",
            "nurse", "social", "lawyer", "therap", "clinic", "cyber", "budget", "Procurement", "test",
            "finance", "ict", "business", "financial", "enterprise", "hostel", "graphic", "med", "cook", "hire",
            "human resource", "family", "cost", "guardian", "ologist", "regrist" };

        public async Task ExtractWithQuery(string query)
        {
            await _hub.Clients.All.SendAsync("update", $"Searching '{query}'");
            try
            {
                await Search(query);

            } catch (WebDriverException e)
            {
                PageGenerator.newDriver();
                await Search(query);
            }


            try
            {
                List<JobInputModel> indexJobs = await ExtractIndex();
                int pageCount = 1;
                await _hub.Clients.All.SendAsync("update", $"Page {pageCount}");
                Jobs.AddRange(indexJobs);
                while (HasNextPage())
                {
                    await NextPage();
                    pageCount++;
                    await _hub.Clients.All.SendAsync("update", $"Page {pageCount}");
                    indexJobs = await ExtractIndex();
                    Jobs.AddRange(indexJobs);
                }
            } catch (WebDriverException e)
            {
                PageGenerator.newDriver();
            }

        }

        public async Task ExtractJobs()
        {
            List<JobInputModel> newJobs = new List<JobInputModel>();
            double total = Jobs.Count;
            int i = 0;
            foreach (var job in Jobs)
            {
                i++;
                double percent = i / total;
                await _hub.Clients.All.SendAsync("update", percent.ToString("P"));
                //if (_filters.Any(job.Title.Contains))
                //{
                //    continue;
                //}
                bool valid = JobFilter.IsValid(job);
                if (!valid)
                {
                    await _hub.Clients.All.SendAsync("update", $"Skipping: {job.Title} [{job.Organization}]");
                    continue;
                }
                try
                {
                    JobInputModel extracted = await ExtractJob(job);
                    newJobs.Add(extracted);
                }
                catch (Exception ex)
                {
                    errors.Add(job);
                    continue;
                }
            }
            Jobs = newJobs;
        }

    }
}
