using Common.Models.Enums;
using OpenQA.Selenium;
using System.Text.RegularExpressions;

namespace Common.Models.Readers
{
    public class ItemReader : Reader
    {
        private static string salaryRegex = @"\$\d{1,3}(,?\d{1,3})*(.\d{1,2})? ?(to)?(-)? ?(\$\d{1,3}(,?\d{1,3})*(.\d{1,2})?)?";
        private Func<IWebElement, string> Func;
        public ItemReader(By by, IWebElement parent, JobAttribute attribute, Func<IWebElement, string> func) : base(by, parent)
        {
            Func = func;
            Attribute = attribute;
        }

        protected override List<JobWrapper> GetWrappers()
        {
            List<JobWrapper> wrappers = new List<JobWrapper>();
            Elements = Parent.FindElements(By).ToList();
            foreach (var ele in Elements)
            {
                JobWrapper wrapper = new JobWrapper();
                wrapper.Parent = Parent;
                wrapper.Element = ele;
                if (Attribute == JobAttribute.Link) wrapper.Input.Url = Func.Invoke(ele);
                if (Attribute == JobAttribute.Title) wrapper.Input.Title = Func.Invoke(ele);
                if (Attribute == JobAttribute.Location) wrapper.Input.Location = Func.Invoke(ele);
                if (Attribute == JobAttribute.Organisation) wrapper.Input.Organization = Func.Invoke(ele);
                if (Attribute == JobAttribute.ClosureDate) wrapper.Input.ClosureDate = Func.Invoke(ele);
                if (Attribute == JobAttribute.Salary)
                {
                    Regex regex = new Regex(salaryRegex);
                    string elementText = Func.Invoke(ele);
                    if (regex.IsMatch(elementText)) wrapper.Input.Salary = regex.Match(elementText).Value;
                }

                wrappers.Add(wrapper);
            }
            return wrappers;
        }
    }
}
