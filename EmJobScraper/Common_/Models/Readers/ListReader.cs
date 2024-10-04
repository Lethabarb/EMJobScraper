using OpenQA.Selenium;

namespace Common.Models.Readers
{
    public class ListReader : Reader
    {
        public ListReader(By by, IWebElement parent) : base(by, parent)
        {
        }

        protected override List<JobWrapper> GetWrappers()
        {
            List<JobWrapper> wrappers = new List<JobWrapper>();
            foreach (var ele in Elements)
            {
                JobWrapper wrapper = new JobWrapper();
                wrapper.Parent = Parent;
                wrapper.Element = ele;
                wrappers.Add(wrapper);
            }
            return wrappers;
        }
    }
}
