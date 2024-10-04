using Common.Entities;
using System.Net.Http.Json;

namespace Blazor.Services
{
    public class ExtractorService
    {
        public string CurrentState { get; set; } = "";
        public int Total { get; } = 8;
        public int Progress = 0;
        public double ProgressPercentage
        {
            get
            {
                return (double)Progress / (double)Total * 100;
            }
        }
        private JobsService jobsService;
        public ExtractorService(JobsService _jobsService)
        {
            jobsService = _jobsService;
        }


        public void Start()
        {
            Task.Run(GetJobs);
        }


        public async Task GetJobs()
        {
            List<Job> jobs = new List<Job>();

            HttpClient client = new HttpClient();
            client.Timeout = TimeSpan.FromDays(1);

            string baselink = "https://localhost:32772";
            HttpResponseMessage res;
            try
            {
                CurrentState = "reading QLD 1/8";
                Progress = 1;
                res = await client.GetAsync($"{baselink}/QLD");
                List<Job> qld = await res.Content.ReadFromJsonAsync<List<Job>>();
                jobs.AddRange(qld);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                CurrentState = "skipping QLD";
            }
            try
            {

                CurrentState = "reading SA 2/8";
                Progress = 2;
                res = await client.GetAsync($"{baselink}/SA");
                List<Job> sa = await res.Content.ReadFromJsonAsync<List<Job>>();
                jobs.AddRange(sa);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                CurrentState = "skipping SA";
            }
            try
            {
                CurrentState = "reading VIC 3/8";
                Progress = 3;
                res = await client.GetAsync($"{baselink}/VIC");
                List<Job> vic = await res.Content.ReadFromJsonAsync<List<Job>>();
                jobs.AddRange(vic);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                CurrentState = "skipping VIC";
            }
            try
            {
                CurrentState = "reading WA 4/8";
                Progress = 4;
                res = await client.GetAsync($"{baselink}/WA");
                List<Job> vic = await res.Content.ReadFromJsonAsync<List<Job>>();
                jobs.AddRange(vic);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                CurrentState = "skipping WA";
            }
            try
            {

                CurrentState = "reading NSW 5/8";
                Progress = 5;
                res = await client.GetAsync($"{baselink}/NSW");
                List<Job> nsw = await res.Content.ReadFromJsonAsync<List<Job>>();
                jobs.AddRange(nsw);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                CurrentState = "skipping NSW";
            }
            try
            {
                CurrentState = "reading ACT 6/8";
                Progress = 6;
                res = await client.GetAsync($"{baselink}/ACT");
                List<Job> act = await res.Content.ReadFromJsonAsync<List<Job>>();
                jobs.AddRange(act);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                CurrentState = "skipping ACT";
            }
            try
            {

                CurrentState = "reading TAS 7/8 ";
                Progress = 7;
                res = await client.GetAsync($"{baselink}/TAS");
                List<Job> tas = await res.Content.ReadFromJsonAsync<List<Job>>();
                jobs.AddRange(tas);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                CurrentState = "skipping TAS";
            }
            try
            {
                CurrentState = "reading NT 8/8";
                Progress = 8;
                res = await client.GetAsync($"{baselink}/NT");
                List<Job> nt = await res.Content.ReadFromJsonAsync<List<Job>>();
                jobs.AddRange(nt);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                CurrentState = "skipping NT";
            }
            CurrentState = "Finished";
            Console.WriteLine(jobs.Count);
            jobsService.AddJobs(jobs.RemoveDuplicates());
        }
    }
}
