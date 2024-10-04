using Common.Models.Enums;
using Common.Models.Readers.Configs;
using OpenQA.Selenium;

namespace Common.Models.Readers
{
    public class BaseReader : Reader
    {
        public BaseReader(WebDriver driver) : base(driver)
        {
        }

        protected override List<JobWrapper> GetWrappers()
        {
            return new List<JobWrapper>()
            {
                new JobWrapper()
                {
                    Parent = null,
                    Element = Elements.FirstOrDefault()
                }
            };
        }
        public void AddChildren(JobReaderConfiguration jobReader)
        {
            if (jobReader.needSplitting)
            {
                if (jobReader.Title != null) AddRegex(jobReader.Title, JobAttribute.Title, jobReader.TitleRegex, jobReader.TitleMatchNumber, jobReader.TitleIsGroup, jobReader.TitleGroupNumber);
                if (jobReader.Location != null) AddRegex(jobReader.Location, JobAttribute.Location, jobReader.LocationRegex, jobReader.LocationMatchNumber, jobReader.LocationIsGroup, jobReader.LocationGroupNumber);
                if (jobReader.Organization != null) AddRegex(jobReader.Organization, JobAttribute.Organisation, jobReader.OrganisationRegex, jobReader.OrganisationMatchNumber, jobReader.OrganisationIsGroup, jobReader.OrganisationGroupNumber);
                if (jobReader.ClosureDate != null) AddRegex(jobReader.ClosureDate, JobAttribute.ClosureDate, jobReader.DateRegex, jobReader.DateMatchNumber, jobReader.DateIsGroup, jobReader.DateGroupNumber);
                if (jobReader.Salary != null) AddRegex(jobReader.Salary, JobAttribute.Salary, jobReader.SalaryRegex, jobReader.SalaryMatchNumber, jobReader.SalaryIsGroup, jobReader.SalaryGroupNumber);
            }
            else
            {
                if (jobReader.Title != null) AddTextChild(jobReader.Title, JobAttribute.Title);
                if (jobReader.Location != null) AddTextChild(jobReader.Location, JobAttribute.Location);
                if (jobReader.Organization != null) AddTextChild(jobReader.Organization, JobAttribute.Organisation);
                if (jobReader.ClosureDate != null) AddTextChild(jobReader.ClosureDate, JobAttribute.ClosureDate);
                if (jobReader.Salary != null) AddTextChild(jobReader.Salary, JobAttribute.Salary);

                if (jobReader.TableRows != null)
                {
                    TableReader table = AddTableChild(jobReader.TableRows, jobReader.RowLabel, jobReader.RowValue, jobReader.TableMap);
                }

            }
        }
    }
}
