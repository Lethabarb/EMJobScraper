using OpenQA.Selenium;

namespace Common.Models.Readers.Configs
{
    public class LinkReaderConfiguration
    {
        public By JobCard { get; set; }

        public By Url { get; set; }

        public By Title { get; set; }
        public By Location { get; set; }
        public By Organization { get; set; }
        public By ClosureDate { get; set; }
        public By Salary { get; set; }
        public bool NeedsSearch { get; set; }

        public LinkReaderConfiguration() { }
    }
}
