using Common.Entities;
using Common.Models.Enums;
using System.Globalization;

namespace Common.Models
{
    public class JobInputModel
    {
        public string Title { get; set; }
        public string Location { get; set; }
        public string Organization { get; set; }
        public string ClosureDate { get; set; }
        public string Salary { get; set; }
        public string Url { get; set; }
        public State State { get; set; }
        public JobInputModel() { }

        public JobInputModel(string title, string url, State state)
        {
            Title = title;
            Url = url;
            State = state;
        }

        public Job ToJob(string dateFormat)
        {
            Job j = new Job();
            j.Title = Title;
            j.Location = Location;
            j.Organization = Organization;
            j.State = State;
            j.Salary = Salary;
            j.Link = Url;
            DateTime dt = DateTime.Now.AddYears(1);
            if (ClosureDate != null && ClosureDate.ToLower() != "ongoing" && ClosureDate.ToLower() != "not provided") dt = DateTime.ParseExact(ClosureDate, dateFormat, CultureInfo.InvariantCulture);
            j.ClosureDate = DateOnly.FromDateTime(dt);
            return j;
        }
        public Job ToJob()
        {
            Job j = new Job();
            j.Title = Title;
            if (Location == null && State == State.ACT) Location = "Canberra";
            else j.Location = Location;
            j.Organization = Organization;
            j.State = State;
            j.Salary = Salary;
            j.Link = Url;
            DateTime dt = DateTime.Now.AddYears(1);
            if (ClosureDate != null && ClosureDate.ToLower() != "ongoing") dt = DateTime.Parse(ClosureDate, CultureInfo.InvariantCulture);
            j.ClosureDate = DateOnly.FromDateTime(dt);
            return j;
        }

        public void mergeInputs(JobInputModel j)
        {
            if (Title == null && j.Title != null) Title = j.Title;
            if (Location == null && j.Location != null) Location = j.Location;
            if (Organization == null && j.Organization != null) Organization = j.Organization;
            if (ClosureDate == null && j.ClosureDate != null) ClosureDate = j.ClosureDate;
            if (Salary == null && j.Salary != null) Salary = j.Salary;
            if (Url == null && j.Url != null) Url = j.Url;
        }

        public static JobInputModel Nsw(string title, string url)
        {
            return new JobInputModel(title, url, State.NSW);
        }
        public static JobInputModel Qld(string title, string url)
        {
            return new JobInputModel(title, url, State.QLD);
        }
        public static JobInputModel Vic(string title, string url)
        {
            return new JobInputModel(title, url, State.VIC);
        }
        public static JobInputModel Act(string title, string url)
        {
            return new JobInputModel(title, url, State.ACT);
        }
        public static JobInputModel Nt(string title, string url)
        {
            return new JobInputModel(title, url, State.NT);
        }
        public static JobInputModel Sa(string title, string url)
        {
            return new JobInputModel(title, url, State.SA);
        }
        public static JobInputModel Wa(string title, string url)
        {
            return new JobInputModel(title, url, State.WA);
        }
        public static JobInputModel Tas(string title, string url)
        {
            return new JobInputModel(title, url, State.TAS);
        }
        public static JobInputModel Fed(string title, string url)
        {
            return new JobInputModel(title, url, State.FED);
        }
    }
}
