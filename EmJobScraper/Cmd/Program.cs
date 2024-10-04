using Common.Entities;
using CsvHelper;
using CsvHelper.TypeConversion;
using Microsoft.VisualBasic;
using System.Formats.Asn1;
using System.Globalization;
using System.Net.Http.Json;

HttpClient client = new HttpClient();
client.Timeout = TimeSpan.FromDays(1);

List<Job> jobs = new List<Job>();

string baselink = "https://localhost:32772";
HttpResponseMessage res;
try
{
    Console.WriteLine("reading QLD 1/6");
    res = await client.GetAsync($"{baselink}/QLD");
    List<Job> qld = await res.Content.ReadFromJsonAsync<List<Job>>();
    jobs.AddRange(qld);

}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
    Console.WriteLine(ex.StackTrace);
    Console.WriteLine("skipping QLD");
}
try
{

    Console.WriteLine("reading SA 2/6 ");
    res = await client.GetAsync($"{baselink}/SA");
    List<Job> sa = await res.Content.ReadFromJsonAsync<List<Job>>();
    jobs.AddRange(sa);
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
    Console.WriteLine(ex.StackTrace);
    Console.WriteLine("skipping SA");
}
try
{
    Console.WriteLine("reading VIC 3/6");
    res = await client.GetAsync($"{baselink}/VIC");
    List<Job> vic = await res.Content.ReadFromJsonAsync<List<Job>>();
    jobs.AddRange(vic);

}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
    Console.WriteLine(ex.StackTrace);
    Console.WriteLine("skipping VIC");
}
try
{
    Console.WriteLine("reading WA 4/6");
    res = await client.GetAsync($"{baselink}/WA");
    List<Job> vic = await res.Content.ReadFromJsonAsync<List<Job>>();
    jobs.AddRange(vic);

}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
    Console.WriteLine(ex.StackTrace);
    Console.WriteLine("skipping WA");
}
try
{

    Console.WriteLine("reading NSW 5/6");
    res = await client.GetAsync($"{baselink}/NSW");
    List<Job> nsw = await res.Content.ReadFromJsonAsync<List<Job>>();
    jobs.AddRange(nsw);
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
    Console.WriteLine("skipping NSW");
}
try
{
    Console.WriteLine("reading ACT 6/6");
    res = await client.GetAsync($"{baselink}/ACT");
    List<Job> act = await res.Content.ReadFromJsonAsync<List<Job>>();
    jobs.AddRange(act);

}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
    Console.WriteLine("skipping ACT");
}
try
{

    Console.WriteLine("reading TAS 7/6 ");
    res = await client.GetAsync($"{baselink}/TAS");
    List<Job> tas = await res.Content.ReadFromJsonAsync<List<Job>>();
    jobs.AddRange(tas);
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
    Console.WriteLine(ex.StackTrace);
    Console.WriteLine("skipping TAS");
}
try
{
    Console.WriteLine("reading NT 8/6");
    res = await client.GetAsync($"{baselink}/NT");
    List<Job> nt = await res.Content.ReadFromJsonAsync<List<Job>>();
    jobs.AddRange(nt);

}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
    Console.WriteLine("skipping NT");
}



using (var writer = new StreamWriter("C:\\Users\\Colin\\OneDrive\\Documents\\ACIM\\Jobs\\unfiltered.csv"))
using (var csv = new CsvWriter(writer, CultureInfo.CurrentCulture))
{
    var options = new TypeConverterOptions { Formats = new[] { "dd MMM yyyy" } };
    csv.Context.TypeConverterOptionsCache.AddOptions<DateOnly>(options);
    csv.WriteRecords(jobs);
}

//try
//{
//    string[] FILTERS = [ "education", "teach", "legal", "regrister", "executive", "school",
//            "technology", "cleric", "payroll", "client", "admin", "data", "engineer", "motor", "mechanic", "health",
//            "nurse", "social", "lawyer", "customer", "therap", "clinic", "cyber", "budget", "Procurement", "test",
//            "finance", "ict", "it", "business", "financial", "enterprise", "hostel", "graphic", "med", "cook", "hire",
//            "human resource", "family", "cost", "guardian", "ologist", "regrist" ];
//    List<Job> removed = new List<Job>();
//    foreach (Job j in jobs)
//    {
//        foreach (string s in FILTERS)
//        {
//            if (j.Title.Contains(s)) removed.Add(j);
//            if (j.Organization.Contains(s)) removed.Add(j);
//        }
//    }
//    foreach (Job j in removed)
//    {
//        jobs.Remove(j);
//    }

//}
//catch (Exception ex)
//{
//    Console.WriteLine($"{ex.Message}");
//    Console.WriteLine($"{ex.StackTrace}");
//}
//Job j = new Job()
//{
//    Title = "title",
//    ClosureDate = DateOnly.FromDateTime(DateTime.Now),
//    Link = "link",
//    Location = "location",
//    Organization = "org",
//    State = Common.Models.Enums.State.FED
//};


//using (var writer = new StreamWriter("C:\\Users\\Colin\\OneDrive\\Documents\\ACIM\\Jobs\\filtered.csv"))
//using (var csv = new CsvWriter(writer, CultureInfo.CurrentCulture))
//{
//    var options = new TypeConverterOptions { Formats = new[] { "dd MMM yyyy" } };
//    csv.Context.TypeConverterOptionsCache.AddOptions<DateOnly>(options);
//    csv.WriteRecords(jobs);
//}
