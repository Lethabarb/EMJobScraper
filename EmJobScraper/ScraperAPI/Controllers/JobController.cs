using Common.Entities;
using Common.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using ScraperAPI.Services;
using ScraperAPI.Services.States;
namespace ScraperAPI.Controllers
{

    namespace ScraperAPI.Controllers
    {
        [ApiController]
        public class JobController : ControllerBase
        {

            private readonly ILogger<NTController> _logger;
            private PageGenerator _pageGenerator;
            private string[] searchTerms = ["Regulations", "Investigations", "Governance", "Fairwork", "WHS", "Ranger", "Fire Fighter", "Surf Life Saving", "Emergency Management", "Incident Management", "Inspector", "Risk Management", "Safety", "Auditor"];
            //private ACTService act;
            //private NSWService nsw;
            //private QLDService qld;
            //private SAService sa;
            //private TASService tas;
            //private VICService vic;
            //private WAService wa;
            private IHubContext<JobHub> _hubContext;

            //public JobController(ILogger<SAController> logger, ACTService act, NSWService nsw, QLDService qld, SAService sa, TASService tas, VICService vic, WAService wa)
            public JobController(ILogger<NTController> logger, PageGenerator pageGenerator, IHubContext<JobHub> hubContext)
            {
                _logger = logger;
                _hubContext = hubContext;
                _pageGenerator = pageGenerator;
            }

            [HttpGet("QLD")]
            public async Task<IEnumerable<Job>> GetQLD()
            {
                Console.WriteLine("QLD");
                List<Job> jobs = new List<Job>();
                List<JobInputModel> inputs = new List<JobInputModel>();
                foreach (var term in searchTerms)
                {
                    QLDService qld = new QLDService(_pageGenerator, _hubContext);
                  await qld.ExtractWithQuery(term);
                  await qld.ExtractJobs();
                  inputs.AddRange(qld.Jobs);
                }
                foreach (JobInputModel jobInputModel in inputs)
                {
                    jobs.Add(jobInputModel.ToJob("dd-MMM-yyyy"));
                }
                return jobs;
            }
            [HttpGet("SA")]
            public async Task<IEnumerable<Job>> GetSA()
            {
                Console.WriteLine("SA");
                List<Job> jobs = new List<Job>();
                List<JobInputModel> inputs = new List<JobInputModel>();
                foreach (var term in searchTerms)
                {
                    SAService sa = new SAService(_pageGenerator, _hubContext);
                    await sa.ExtractWithQuery(term);
                    await sa.ExtractJobs();
                    inputs.AddRange(sa.Jobs);
                }
                foreach (JobInputModel jobInputModel in inputs)
                {
                    jobs.Add(jobInputModel.ToJob("dd/MM/yyyy"));
                }
                return jobs;
            }
            [HttpGet("VIC")]
            public async Task<IEnumerable<Job>> GetVIC()
            {
                Console.WriteLine("VIC");
                List<Job> jobs = new List<Job>();
                List<JobInputModel> inputs = new List<JobInputModel>();
                foreach (var term in searchTerms)
                {
                    VICService vic = new VICService(_pageGenerator, _hubContext);
                    await vic.ExtractWithQuery(term);
                    await vic.ExtractJobs();
                    inputs.AddRange(vic.Jobs);
                }
                foreach (JobInputModel jobInputModel in inputs)
                {
                    jobs.Add(jobInputModel.ToJob("dd/MM/yyyy"));
                }
                return jobs;
            }
            [HttpGet("WA")]
            public async Task<IEnumerable<Job>> GetWA()
            {
                Console.WriteLine("WA");
                List<Job> jobs = new List<Job>();
                List<JobInputModel> inputs = new List<JobInputModel>();
                foreach (var term in searchTerms)
                {
                    WAService wa = new WAService(_pageGenerator, _hubContext);
                    await wa.ExtractWithQuery(term);
                    await wa.ExtractJobs();
                    inputs.AddRange(wa.Jobs);
                }
                foreach (JobInputModel jobInputModel in inputs)
                {
                    jobs.Add(jobInputModel.ToJob("yyyy-MM-dd h:mm tt"));
                }
                return jobs;
            }
            [HttpGet("NSW")]
            public async Task<IEnumerable<Job>> GetNSW()
            {
                Console.WriteLine("NSW");
                List<Job> jobs = new List<Job>();
                List<JobInputModel> inputs = new List<JobInputModel>();
                NSWService qld = new NSWService(_pageGenerator, _hubContext);
                await qld.ExtractWithQuery("");
                await qld.ExtractJobs();
                inputs.AddRange(qld.Jobs);
                foreach (JobInputModel jobInputModel in inputs)
                {
                    bool success = false;
                    try
                    {
                        jobs.Add(jobInputModel.ToJob("dd/MM/yyyy - hh:mm tt"));
                        success = true;
                    } catch (Exception ex)
                    {
                    }
                    try
                    {
                        if (!success) jobs.Add(jobInputModel.ToJob("dd MMM yyyy"));
                    }
                    catch (Exception ex)
                    {
                    }

                }
                return jobs;
            }
            [HttpGet("ACT")]
            public async Task<IEnumerable<Job>> GetACT()
            {
                Console.WriteLine("ACT");
                List<Job> jobs = new List<Job>();
                List<JobInputModel> inputs = new List<JobInputModel>();
                ACTService qld = new ACTService(_pageGenerator, _hubContext);
                await qld.ExtractWithQuery("");
                await qld.ExtractJobs();
                inputs.AddRange(qld.Jobs);
                foreach (JobInputModel jobInputModel in inputs)
                {
                    jobs.Add(jobInputModel.ToJob("dd MMMM yyyy"));
                }
                return jobs;
            }
            [HttpGet("TAS")]
            public async Task<IEnumerable<Job>> GetTAS()
            {
                Console.WriteLine("TAS");
                List<Job> jobs = new List<Job>();
                List<JobInputModel> inputs = new List<JobInputModel>();
                TASService qld = new TASService(_pageGenerator, _hubContext);
                await qld.ExtractWithQuery("");
                await qld.ExtractJobs();
                inputs.AddRange(qld.Jobs);
                foreach (JobInputModel jobInputModel in inputs)
                {
                    jobs.Add(jobInputModel.ToJob("dddd d MMMM, yyyy hh:mm tt"));
                }
                return jobs;
            }

        }
    }

}
