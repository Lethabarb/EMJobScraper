using Common.Entities;
using CsvHelper.TypeConversion;
using CsvHelper;
using System.Globalization;

namespace Blazor.Services
{
    public class JobsService
    {
        public List<Job> Jobs { get; set; } = new();

        private List<List<Job>> Deleted = new();

        public JobsService() { }

        public void AddJobs(IEnumerable<Job> jobs)
        {
            foreach (Job job in jobs)
            {
                Console.WriteLine(job.Title);
            }
            Jobs.AddRange(jobs);
        }

        public string JobsToCSVString()
        {
            Console.WriteLine("Saving Jobs");
            try
            {
                using (var stream = new MemoryStream())
                using (var reader = new StreamReader(stream))
                using (var writer = new StreamWriter(stream))
                using (var csv = new CsvWriter(writer, CultureInfo.CurrentCulture))
                {
                    var options = new TypeConverterOptions { Formats = new[] { "dd MMM yyyy" } };
                    csv.Context.TypeConverterOptionsCache.AddOptions<DateOnly>(options);
                    csv.WriteRecords(Jobs);
                    csv.Flush();
                    stream.Position = 0;
                    string text = reader.ReadToEnd();
                    return text;
                }

            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return "";
        }

        public void RemoveJobs(IEnumerable<Job> jobs)
        {
            Console.WriteLine("Before: " + Jobs.Count);
            foreach (Job job in jobs)
            {
                Jobs.Remove(job);
            }
            Deleted.Add(jobs.ToList());
            Console.WriteLine("After: " + Jobs.Count);
        }
        public void Undo()
        {
            List<Job> addBack = Deleted.Last();
            Jobs.AddRange(addBack);
            Deleted.RemoveAt(Deleted.Count - 1);
        }
    }
}
