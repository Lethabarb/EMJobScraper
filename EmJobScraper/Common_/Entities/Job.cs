using Common.Models.Enums;
using CsvHelper.Configuration.Attributes;

namespace Common.Entities
{
    public class Job
    {
        [Ignore]
        public int Id { get; set; }
        [Ignore]
        public bool IsValid { get; set; }
        [Name("Title")]
        public string Title { get; set; }
        [Name("Location")]
        public string Location { get; set; }
        [Name("STATE")]
        public State State { get; set; }
        [Name("Organisation")]
        public string Organization { get; set; }
        [Name("Closure Date")]
        public DateOnly ClosureDate { get; set; }
        [Name("Annual Salary")]
        public string Salary { get; set; }
        [Name("Jobs Link")]
        public string Link { get; set; }

        public Job() { }
    }
    public static class JobExtensions
    {
        public static List<Job> RemoveDuplicates(this List<Job> jobs)
        {
            Dictionary<string, Job> links = new Dictionary<string, Job>();
            foreach (Job job in jobs)
            {
                string link = job.Link;
                if (link.StartsWith("https://smartjobs.qld.gov.au/jobtools/jncustomsearch.viewFullSingle"))
                {
                    Console.WriteLine(link);
                    link = link.Split("&")[0];
                    Console.WriteLine(link);
                }
                if (!links.ContainsKey(link))
                {
                    links.Add(link, job);
                }
            }
            return links.Values.ToList();
        }
    }
}
