using Common.Models.Readers.Configs;
using OpenQA.Selenium;

namespace Common.Models.Enums
{
    public static class StateSelector
    {
        public static string GetBaseLink(this State state)
        {
            switch (state)
            {
                case State.NSW:
                    return "https://iworkfor.nsw.gov.au/jobs/all-keywords/all-agencies/all-organisations-entities/all-categories/all-locations/all-worktypes?jobcategoryid=10399,10357,10375,10378,10374,10356,10380,10358,10368,10379,10627,16567&sortby=RelevanceDesc&pagesize=100";
                case State.QLD:
                    return "https://smartjobs.qld.gov.au/jobtools/jncustomsearch.jobsearch?in_organid=14904";
                case State.VIC:
                    return "https://careers.vic.gov.au/";
                case State.ACT:
                    return "https://www.jobs.act.gov.au/opportunities/latest";
                case State.NT:
                    return "";
                case State.SA:
                    return "https://iworkfor.sa.gov.au/";
                case State.WA:
                    return "https://search.jobs.wa.gov.au/page.php?pageID=215";
                case State.TAS:
                    return "https://careers.pageuppeople.com/759/cw/en/listing/";
                case State.FED:
                    return "https://www.apsjobs.gov.au/s/";
            }
            throw new Exception("not a state");
        }

        public static LinkReaderConfiguration GetLinkReader(this State state)
        {
            switch (state)
            {
                case State.NSW:
                    return new LinkReaderConfiguration()
                    {
                        JobCard = By.XPath("//*[@id=\"cmsHomeUpperContent\"]/div[2]/div[3]/div[1]/div"),
                        Url = By.XPath("div/a"),
                        Title = By.XPath("div/a/span"),
                        NeedsSearch = false
                    };
                case State.QLD:
                    return new LinkReaderConfiguration()
                    {
                        JobCard = By.XPath("//*[@id=\"content\"]/div/div/div/form[1]/ol/li"),
                        Url = By.XPath("h3/a"),
                        Title = By.XPath("h3/span"),
                        NeedsSearch = true
                    };
                case State.VIC:
                    return new LinkReaderConfiguration()
                    {
                        JobCard = By.XPath("//*[@id=\"bind_search_result\"]/div"),
                        Url = By.XPath("div[1]/a"),
                        Title = By.XPath("div[1]/a/h2"),
                        NeedsSearch = true
                    };
                case State.ACT:
                    return new LinkReaderConfiguration()
                    {
                        JobCard = By.XPath("//*[@id=\"main\"]/div[contains(@class, 'position')]"),
                        Url = By.XPath("div[2]/a"),
                        Title = By.XPath("h3"),
                        NeedsSearch = false
                    };
                case State.NT:
                    throw new NotImplementedException();
                case State.SA:
                    return new LinkReaderConfiguration()
                    {
                        JobCard = By.XPath("//*[@id=\"brs_report_table_16\"]/tbody/tr[2]/td/table/tbody/tr"),
                        Url = By.XPath("td[1]/a"),
                        Title = By.XPath("td[1]"),
                        NeedsSearch = true,
                    };
                case State.WA:
                    throw new NotImplementedException();
                case State.TAS:
                    throw new NotImplementedException();
                case State.FED:
                    throw new NotImplementedException();
            }
            throw new Exception("not a state");
        }
        public static JobReaderConfiguration GetJobReader(this State state)
        {
            switch (state)
            {
                case State.NSW:
                    return new JobReaderConfiguration()
                    {
                        Title = By.XPath("//*[@id=\"main-content\"]/div/div[1]/div[3]/div/div[2]/h1"),
                        Salary = By.XPath("//*[@id=\"main-content\"]/div/div[1]/div[3]/div/div[2]/div[3]"),
                        TableRows = By.XPath("//*[@id=\"main-content\"]/div/div[1]/div[3]/div/div[2]/div[2]/div/table/tbody/tr"),
                        RowLabel = By.XPath("td[1]"),
                        RowValue = By.XPath("td[2]"),
                        TableMap = new Dictionary<string, JobAttribute>
                        {
                            {"Organisation", JobAttribute.Organisation },
                            {"location", JobAttribute.Location },
                            {"Closing date", JobAttribute.ClosureDate },
                            {"remuneration", JobAttribute.Salary }
                        },
                        DateFormat = "dd/MM/yyyy - hh:mm tt"
                    };
                case State.QLD:
                    return new JobReaderConfiguration()
                    {
                        Title = By.XPath("//*[@id=\"content\"]/div/div/div/h1[2]"),
                        Organization = By.XPath("//*[@id=\"content\"]/div/div/div/div[2]/b"),
                        TableRows = By.XPath("//*[@id=\"details\"]/table/tbody/tr"),
                        RowLabel = By.XPath("th[1]"),
                        RowValue = By.XPath("td[1]"),
                        TableMap = new Dictionary<string, JobAttribute>
                        {
                            {"location", JobAttribute.Location },
                            {"Closing date", JobAttribute.ClosureDate },
                            {"yearly", JobAttribute.Salary }
                        }
                    };
                case State.VIC:
                    return new JobReaderConfiguration()
                    {
                        Title = By.XPath("/html/body/div[1]/div[4]/div[1]/div[2]/div/div[1]/div/div[1]/h1"),
                        Organization = By.XPath("/html/body/div[1]/div[4]/div[1]/div[2]/div/div[2]/div[2]/div[1]/div/div[1]/div[1]/div/div[1]/div/div/div[2]/div[1]/p"),
                        OrganisationRegex = @"Organisation:(.*)",
                        OrganisationIsGroup = true,
                        OrganisationMatchNumber = 0,
                        OrganisationGroupNumber = 1,
                        Salary = By.XPath("/html/body/div[1]/div[4]/div[1]/div[2]/div/div[2]/div[2]/div[1]/div/div[1]/div[1]/div/div[1]/div/div/div[2]/div[2]/p"),
                        SalaryRegex = @"Salary:(.*)",
                        SalaryIsGroup = true,
                        SalaryMatchNumber = 0,
                        SalaryGroupNumber = 1,
                        ClosureDate = By.XPath("/html/body/div[1]/div[4]/div[1]/div[2]/div/div[1]/div/div[2]/div[1]/div/div/div[1]/div/div/div[1]/div[2]/span[2]"),
                        Location = By.XPath("/html/body/div[1]/div[4]/div[1]/div[2]/div/div[2]/div[2]/div[1]/div/div[1]/div[1]/div/div[1]/div/div/div[1]/div[1]/p"),
                        LocationRegex = @"Location:(.*)",
                        LocationIsGroup = true,
                        LocationMatchNumber = 0,
                        LocationGroupNumber = 1,
                        DateFormat = "dd/MM/yyyy '(Midnight)'"
                    };
                case State.SA:
                    return new JobReaderConfiguration()
                    {
                        Title = By.XPath("//*[@id=\"report\"]/table/tbody/tr[1]/td/div[2]/div[1]/span[2]"),
                        TitleRegex = @".*",
                        TitleMatchNumber = 0,
                        Organization = By.XPath("//*[@id=\"report\"]/table/tbody/tr[1]/td/div[2]/div[2]/div/b[1]"),
                        OrganisationRegex = @"(.*)",
                        OrganisationMatchNumber = 0,
                        OrganisationGroupNumber = 0,
                        OrganisationIsGroup = false,
                        Salary = By.XPath("//*[@id=\"report\"]/table/tbody/tr[1]/td/div[2]/div[2]/div"),
                        SalaryRegex = @"\$\d{1,3}(,?\d{1,3})*(.\d{1,2})? ?(to)?(-)? ?(\$\d{1,3}(,?\d{1,3})*(.\d{1,2})?)?",
                        SalaryMatchNumber = 0,
                        SalaryGroupNumber = 0,
                        SalaryIsGroup = false,
                        ClosureDate = By.XPath("//*[@id=\"report\"]/table/tbody/tr[1]/td/div[2]/div[2]/div"),
                        DateRegex = @"Applications close:(.*)",
                        DateMatchNumber = 0,
                        DateGroupNumber = 1,
                        DateIsGroup = true,
                        DateFormat = "dd/MM/yyyy h:mm tt",
                        needSplitting = true
                    };
                case State.NT:
                    throw new NotImplementedException();
                case State.ACT:
                    return new JobReaderConfiguration()
                    {
                        Title = By.XPath("//*[@id=\"main\"]/h2"),
                        Salary = By.XPath("//*[@id=\"main\"]/div[1]/div[1]/p[1]"),
                        Organization = By.XPath("//*[@id=\"main\"]/div[1]/div[1]/p[1]"),
                        ClosureDate = By.XPath("/html/body/div[5]/div[2]/div[1]/div[1]/h3"),
                        DateFormat = "dd MMMM yyyy",
                        needSplitting = true,
                        TitleRegex = @".*(?=[\n|])",
                        TitleMatchNumber = 0,
                        SalaryRegex = @"Salary(.*)",
                        SalaryGroupNumber = 0,
                        SalaryIsGroup = true,
                        SalaryMatchNumber = 0,
                        OrganisationRegex = @"Directorate(.*)",
                        OrganisationMatchNumber = 0,
                        OrganisationIsGroup = true,
                        OrganisationGroupNumber = 0,
                        DateRegex = @"Closes:(.*)",
                        DateMatchNumber = 0,
                        DateIsGroup = true,
                        DateGroupNumber = 1
                    };
                case State.WA:
                    throw new NotImplementedException();
                case State.TAS:
                    throw new NotImplementedException();
                case State.FED:
                    throw new NotImplementedException();
            }
            throw new Exception("not a state");
        }

    }
}
