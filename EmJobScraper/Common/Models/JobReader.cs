using Common.Models.Enums;
using OpenQA.Selenium;

namespace Common.Models
{
    public class JobReader
    {
        public By? By { get; set; }
        public IWebElement? Parent { get; set; }
        public List<IWebElement> elements = new List<IWebElement>();
        public string? Value { get; private set; }
        public Func<IWebElement, string>? Func { get; set; }
        public List<JobReader> children { get; set; } = new List<JobReader>();
        public JobAttribute? Attribute { get; set; }

        public JobReader(WebDriver driver)
        {
            elements = driver.FindElements(By.TagName("body")).ToList();
            Attribute = JobAttribute.Other;
        }

        private JobReader(By by, Func<IWebElement, string> func, IWebElement parent, JobAttribute attribute)
        {
            By = by;
            Func = func;
            Parent = parent;
            elements = parent.FindElements(By).ToList();
            Attribute = attribute;
        }
        //TODO: make the func stuff into an "HTMLParser" to be abstracted to "TableParser" and "ElementParser".
        //      create "LinkReader" or change element to List<IWebElement>.
        private JobReader AddChild(By by, Func<IWebElement, string>? func, JobAttribute attribute)
        {
            foreach (var element in elements)
            {
                JobReader reader = new JobReader(by, func, element, attribute);
                children.Add(reader);
            }
            return children[0];
        }
        public JobReader AddChild(By by)
        {
            return AddChild(by, null, JobAttribute.Other);
        }
        public JobReader AddTextChild(By by, JobAttribute attribute)
        {
            return AddChild(by, ele => ele.Text, attribute);
        }
        public JobReader AddAttrChild(By by, JobAttribute attribute)
        {
            return AddChild(by, ele => ele.GetAttribute(attribute.HtmlSelector()), attribute); //TODO: make a selector
        }

        public Dictionary<JobAttribute?, List<string>> GetData()
        {
            Dictionary<JobAttribute?, List<string>> pairs = new Dictionary<JobAttribute?, List<string>>();
            foreach (var child in children)
            {
                pairs.Union(child.GetData());
            }
            if (Attribute != null)
            {
                foreach (var ele in elements)
                {
                    List<string> list = pairs.GetValueOrDefault(Attribute, null);
                    if (list == null) list = new List<string>();
                    else pairs.Remove(Attribute);
                    list.Add(Func.Invoke(ele));
                    pairs.Add(Attribute, list);
                }
            }
            return pairs;
        }
    }
}
