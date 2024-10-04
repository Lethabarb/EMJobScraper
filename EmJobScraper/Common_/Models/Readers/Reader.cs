using Common.Models.Enums;
using OpenQA.Selenium;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace Common.Models.Readers
{
    public abstract class Reader
    {
        public By? By { get; set; }
        public IWebElement? Parent { get; set; }
        public List<IWebElement> Elements = new List<IWebElement>();
        //public string? Value { get; private set; }
        //public Func<IWebElement, string>? Func { get; set; }
        public JobAttribute Attribute { get; protected set; } = JobAttribute.Other;
        //public Dictionary<JobAttribute, string> TableMap { get; set; }
        public List<Reader> Children = new List<Reader>();

        public Reader(WebDriver driver)
        {
            Elements = driver.FindElements(By.TagName("body")).ToList();
            Attribute = JobAttribute.Other;
        }

        protected Reader(By by, IWebElement parent)
        {
            By = by;
            //Func = func;
            Parent = parent;
            Elements = parent.FindElements(By).ToList();
            //foreach (IWebElement element in Elements)
            //{
            //    Console.WriteLine($"{by} - {element.Text}");
            //}
            //Attribute = attribute;
        }
        //TODO: make the func stuff into an "HTMLParser" to be abstracted to "TableParser" and "ElementParser".
        //      create "LinkReader" or change element to List<IWebElement>.
        protected void AddChild(Reader reader)
        {
            Children.Add(reader);
        }
        protected void AddChildren(Collection<Reader> readers)
        {
            Children.AddRange(readers);
        }

        public ListReader AddListChild(By listSelector)
        {
            ListReader listReader = new ListReader(listSelector, Elements[0]);
            AddChild(listReader);
            return listReader;
        }
        public TableReader AddTableChild(By tableRowSelector, By labelSelector, By valueSelector, Dictionary<string, JobAttribute> map)
        {
            TableReader tableReader = new TableReader(tableRowSelector, Elements[0], labelSelector, valueSelector, map);
            AddChild(tableReader);
            return tableReader;
        }
        public void AddTextChild(By by, JobAttribute attribute)
        {
            foreach (var ele in Elements)
            {
                AddChild(new ItemReader(by, ele, attribute, e => e.Text));
            }
        }

        public void AddRegex(By by, JobAttribute attribute, string pattern, int matchIndex, bool captureGroup = false, int GroupIndex = 0)
        {
            Regex r = new Regex(pattern);
            foreach (var ele in Elements)
            {
                //string eleText = ele.Text;
                //Console.WriteLine(eleText);
                //List<Match> matches = r.Matches(eleText).ToList();
                //foreach (Match m in matches)
                //{
                //    Console.WriteLine("  " + m);
                //    GroupCollection groups = m.Groups;
                //    foreach (var item in groups)
                //    {
                //        Console.WriteLine("    " + item);
                //    }
                //}
                AddChild(new ItemReader(by, ele, attribute, e => captureGroup ? GetRegexGroup(e.Text, matchIndex, GroupIndex, r) : GetRegexMatch(e.Text, matchIndex, r)));
            }
        }

        public string GetRegexMatch(string text, int matchIndex, Regex r)
        {
            try
            {
                return r.Matches(text)[matchIndex].Value.Trim();
            }
            catch (Exception ex)
            {
                return "Not Provided";
            }
        }
        public string GetRegexGroup(string text, int matchIndex, int groupIndex, Regex r)
        {
            try
            {
                return r.Matches(text)[matchIndex].Groups[groupIndex].Value.Trim();
            }
            catch (Exception ex)
            {
                return "Not Provided";
            }
        }

        public void AddRefChild(By by, JobAttribute attribute)
        {
            foreach (var ele in Elements)
            {
                AddChild(new ItemReader(by, ele, attribute, e => e.GetAttribute("href")));
            }
        }

        protected abstract List<JobWrapper> GetWrappers();

        public List<JobInputModel> GetLinks()
        {
            List<JobWrapper> wrappers = GetWrappers();

            foreach (var child in Children)
            {
                JobWrapper parentWrapper = wrappers.Where(w => w.Element == child.Parent).First();
                JobInputModel childData = child.GetJob();
                parentWrapper.Input.mergeInputs(childData);
            }
            return wrappers.ConvertAll(new Converter<JobWrapper, JobInputModel>(jw => jw.Input));
        }

        public JobInputModel GetJob()
        {
            JobInputModel input = new JobInputModel();
            List<JobWrapper> wrappers = GetWrappers();
            foreach (JobWrapper wrapper in wrappers)
            {
                input.mergeInputs(wrapper.Input);
            }
            foreach (var child in Children)
            {
                JobInputModel childModel = child.GetJob();
                input.mergeInputs(childModel);
            }
            return input;
        }


        public List<string> GetListFromDictionary(JobAttribute attribute, Dictionary<JobAttribute, List<string>> dict)
        {
            List<string>? list = dict.GetValueOrDefault(attribute, null);
            if (list == null) list = new List<string>();
            else dict.Remove(attribute);
            return list;
        }

    }
}
