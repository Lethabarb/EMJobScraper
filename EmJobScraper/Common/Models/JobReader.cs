using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class JobReader
    {
        public By By { get; set; }
        public IWebElement Parent {  get; set; }
        public IWebElement element { get; set; }
        public List<JobReader> children { get; set; } = new List<JobReader>();

        public JobReader(By by, IWebElement parent)
        {
            Parent = parent;
            By = by;
            element = parent.FindElement(by);
        }

        public string Evaluate(Func<IWebElement, string> func)
        {
            return func.Invoke(element);
        }

        public void addChild(By by)
        {
            children.Add(new JobReader(by, element));
        }
    }
}
