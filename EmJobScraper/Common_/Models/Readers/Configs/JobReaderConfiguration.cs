using Common.Models.Enums;
using OpenQA.Selenium;

namespace Common.Models.Readers.Configs
{
    public class JobReaderConfiguration
    {
        public By Title { get; set; }
        public By Location { get; set; }
        public By Organization { get; set; }
        public By ClosureDate { get; set; }
        public string DateFormat { get; set; }
        public By Salary { get; set; }
        public By Table { get; set; }
        public By TableRows { get; set; }
        public By RowLabel { get; set; }
        public By RowValue { get; set; }

        public bool needSplitting { get; set; } = false;
        public string TitleRegex { get; set; }
        public int TitleMatchNumber { get; set; }
        public int TitleGroupNumber { get; set; } = 0;
        public bool TitleIsGroup { get; set; } = false;
        public string LocationRegex { get; set; }
        public int LocationMatchNumber { get; set; }
        public int LocationGroupNumber { get; set; } = 0;
        public bool LocationIsGroup { get; set; } = false;
        public string OrganisationRegex { get; set; }
        public int OrganisationMatchNumber { get; set; }
        public int OrganisationGroupNumber { get; set; } = 0;
        public bool OrganisationIsGroup { get; set; } = false;
        public string DateRegex { get; set; }
        public int DateMatchNumber { get; set; }
        public int DateGroupNumber { get; set; } = 0;
        public bool DateIsGroup { get; set; } = false;
        public string SalaryRegex { get; set; }
        public int SalaryMatchNumber { get; set; }
        public int SalaryGroupNumber { get; set; } = 0;
        public bool SalaryIsGroup { get; set; } = false;
        public Dictionary<string, JobAttribute> TableMap { get; set; }
    }
}
