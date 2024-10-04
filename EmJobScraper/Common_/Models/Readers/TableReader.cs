using Common.Models.Enums;
using OpenQA.Selenium;

namespace Common.Models.Readers
{
    public class TableReader : Reader
    {
        public By LabelSelector { get; set; }
        public By ValueSelector { get; set; }
        public Dictionary<string, JobAttribute> AttributeMap { get; private set; }
        public TableReader(By by, IWebElement parent, By labelSelector, By valueSelector, Dictionary<string, JobAttribute> map) : base(by, parent)
        {
            AttributeMap = map;
            LabelSelector = labelSelector;
            ValueSelector = valueSelector;
        }

        protected override List<JobWrapper> GetWrappers()
        {
            List<JobWrapper> wrappers = new List<JobWrapper>();
            foreach (var row in Elements)
            {
                JobWrapper wrapper = new JobWrapper();
                wrapper.Parent = Parent;
                wrapper.Element = row;
                IWebElement label = row.FindElement(LabelSelector);
                IWebElement value = row.FindElement(ValueSelector);

                List<string> keys = AttributeMap.Keys.ToList();
                foreach (var key in keys)
                {
                    if (label.Text.Contains(key))
                    {
                        JobAttribute attr = AttributeMap[key];
                        if (attr == JobAttribute.Link) wrapper.Input.Url = value.Text;
                        if (attr == JobAttribute.Title) wrapper.Input.Title = value.Text;
                        if (attr == JobAttribute.Location) wrapper.Input.Location = value.Text;
                        if (attr == JobAttribute.Organisation) wrapper.Input.Organization = value.Text;
                        if (attr == JobAttribute.ClosureDate) wrapper.Input.ClosureDate = value.Text;
                        if (attr == JobAttribute.Salary) wrapper.Input.Salary = value.Text;
                    }
                }
                wrappers.Add(wrapper);
            }
            return wrappers;
        }

    }
}
