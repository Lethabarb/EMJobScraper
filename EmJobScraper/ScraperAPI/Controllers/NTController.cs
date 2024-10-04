using Common.Entities;
using Common.Models.Enums;
using Common.Models.NT;
using Microsoft.AspNetCore.Mvc;
using ScraperAPI.Services;
using ScraperAPI.Utilities;

namespace ScraperAPI.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class NTController
    {
        private readonly ILogger<NTController> _logger;

        public NTController(ILogger<NTController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public async Task<IEnumerable<Job>> GetNT()
        {
            HttpService http = new HttpService();
            NtResponse res = await http.GetNTData();
            List<Job> jobs = new List<Job>();

            foreach (var ad in res.data)
            {
                try
                {
                    Job j = new Job()
                    {
                        Title = ad.jobTitle,
                        Location = ad.locations,
                        ClosureDate = DateOnly.FromDateTime(DateTime.Parse(ad.closingDate)),
                        Salary = $"{ad.lowestRemuneration} - {ad.highestRemuneration}",
                        Organization = ad.agency,
                        Link = $"https://jobs.nt.gov.au/Home/JobDetails?rtfId={ad.rtfid}",
                        State = State.NT
                    };
                    if (JobFilter.IsValid(j))
                    {
                        jobs.Add(j);
                    }

                }
                catch (Exception e)
                {
                    Job j = new Job()
                    {
                        Title = ad.jobTitle,
                        Location = ad.locations,
                        ClosureDate = DateOnly.FromDateTime(DateTime.Now.AddYears(1)),
                        Salary = $"{ad.lowestRemuneration} - {ad.highestRemuneration}",
                        Organization = ad.agency,
                        Link = $"https://jobs.nt.gov.au/Home/JobDetails?rtfId={ad.rtfid}",
                        State = State.NT
                    };
                    jobs.Add(j);
                }
            }
            return jobs;
        }
    }
}
