using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace ScraperAPI.Services
{
    public class Page
    {
        private ChromeDriver driver;
        public IWebElement selectedElement;

        public Page(ChromeDriver driver, string url)
        {
            this.driver = driver;
            driver.SwitchTo().NewWindow(WindowType.Window);
            driver.Navigate().GoToUrl(url);
        }

        public async Task Click()
        {
            selectedElement.Click();
            await Task.Run(() => { Thread.Sleep(5000); });
        }
        public void Type(string value)
        {
            selectedElement.SendKeys(value);
        }

        public string GetText()
        {
            return selectedElement.Text;
        }

        public void SelectElement(By by)
        {
            selectedElement = driver.FindElement(by);
        }
        public List<IWebElement> GetElements(By by)
        {
            var eles = driver.FindElements(by);
            return eles.ToList();
        }
        //private LinkReaderConfiguration linkReader;
        //private JobReaderConfiguration jobReader;
        //private INavigation driverNav;

        //private string[] FILTERS = [ "education", "teach", "legal", "regrister", "executive", "school",
        //    "technology", "cleric", "payroll", "client", "admin", "data", "engineer", "motor", "mechanic", "health",
        //    "nurse", "social", "lawyer", "customer", "therap", "clinic", "cyber", "budget", "Procurement", "test",
        //    "finance", "ict", "it", "business", "financial", "enterprise", "hostel", "graphic", "med", "cook", "hire",
        //    "human resource", "family", "cost", "guardian", "ologist", "regrist" ];
        //public BrowserService()
        //{
        //    ChromeOptions opts = new ChromeOptions();
        //    opts.AddArgument("--headless");
        //    opts.AddArgument("--no-sandbox");
        //    opts.AddArgument("--disable-gpu");
        //    opts.AddArgument("--disable-dev-shm-usage");

        //    ChromeDriverService chromeService = ChromeDriverService.CreateDefaultService();
        //    chromeService.SuppressInitialDiagnosticInformation = true;


        //    driver = new ChromeDriver(chromeService, opts);
        //    driverNav = driver.Navigate();
        //    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        //}


        ////public async Task nextState(State s)
        ////{
        ////    if (driver == null) throw new NullReferenceException();
        ////    state = s;
        ////    await initState(s);
        ////}
        //public void InitState(State state)
        //{
        //    foreach (var window in driver.WindowHandles)
        //    {
        //        driver.SwitchTo().Window(window);
        //        if (driver.WindowHandles.Count > 1) driver.Close();
        //    }
        //    try
        //    {
        //        driver.SwitchTo().NewWindow(WindowType.Window);
        //        driverNav.GoToUrl(state.getBaseLink());
        //        Thread.Sleep(5000);
        //        linkReader = state.GetLinkReader();
        //        jobReader = state.GetJobReader();
        //    }
        //    catch (WebDriverException e)
        //    {
        //        driver.Quit();
        //        throw new WebDriverException("", e);
        //    }
        //}

        //public List<JobInputModel> GetJobLinks(State state, string searchTerm)
        //{
        //    Console.WriteLine("searching: " + searchTerm);
        //    List<JobInputModel> models = new List<JobInputModel>();
        //    Navigator nav;
        //    try
        //    {
        //        Navigator.Refresh(driver, state);
        //        nav = Navigator.GetNavigator(state);
        //        if (linkReader.NeedsSearch) nav.Search(searchTerm, driver);
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine($"[{state}] {e.Message}");
        //        Console.WriteLine(e.StackTrace);
        //        return models;
        //    }

        //    Navigator.Refresh(driver, state);
        //    nav = Navigator.GetNavigator(state);
        //    Thread.Sleep(3000);
        //    try
        //    {
        //        models.AddRange(ReadSingleIndexPage(state));
        //    } catch (Exception e)
        //    {
        //        Console.WriteLine($"[{state}] {e.Message}");
        //        Console.WriteLine(e.StackTrace);
        //    }
        //    while (nav.HasNextPage())
        //    {
        //        try
        //        {
        //            nav.NextPage(driver);
        //            Navigator.Refresh(driver, state);
        //            nav = Navigator.GetNavigator(state);
        //            models.AddRange(ReadSingleIndexPage(state));
        //        }
        //        catch (Exception e)
        //        {
        //            Console.WriteLine($"[{state}] {e.Message}");
        //            Console.WriteLine(e.StackTrace);
        //            continue;
        //        }
        //    }
        //    return models;
        //}

        //private List<JobInputModel> ReadSingleIndexPage(State state)
        //{
        //    //Reader reader = new BaseReader(driver);
        //    //Reader cards = reader.AddChild(linkReader.JobCard);
        //    //Reader link = cards.AddAttrChild(linkReader.Url, JobAttribute.Link);
        //    //Reader title = cards.AddTextChild(linkReader.Title, JobAttribute.Title);
        //    //List<JobWrapper> data = cards.GetData();

        //    BaseReader body = new BaseReader(driver);
        //    ListReader cards = body.AddListChild(linkReader.JobCard);
        //    cards.AddRefChild(linkReader.Url, JobAttribute.Link);
        //    cards.AddTextChild(linkReader.Title, JobAttribute.Title);
        //    if (linkReader.Location != null) cards.AddTextChild(linkReader.Location, JobAttribute.Location);
        //    if (linkReader.Organization != null) cards.AddTextChild(linkReader.Organization, JobAttribute.Organisation);
        //    if (linkReader.ClosureDate != null) cards.AddTextChild(linkReader.ClosureDate, JobAttribute.ClosureDate);
        //    if (linkReader.Salary != null) cards.AddTextChild(linkReader.Salary, JobAttribute.Salary);
        //    var data = cards.GetLinks();
        //    data.ForEach(x => x.State = state);
        //    return data;
        //}


        //public List<Job> GetJobs(List<JobInputModel> jobLinks)
        //{
        //    List<Job> jobs = new List<Job>();
        //    int total = jobLinks.Count;
        //    foreach (var job in jobLinks)
        //    {
        //        int index = jobLinks.IndexOf(job);
        //        double percent = (double) index / (double) total;
        //        Console.WriteLine($"{jobLinks.IndexOf(job)}/{total} ({percent}%)");
        //        try
        //        {
        //            if (FILTERS.Any(filter => job.Title.Contains(filter))) continue;
        //            foreach (var window in driver.WindowHandles)
        //            {
        //                driver.SwitchTo().Window(window);
        //                if (driver.WindowHandles.Count > 1) driver.Close();
        //            }
        //            driver.SwitchTo().NewWindow(WindowType.Tab);
        //            driver.Navigate().GoToUrl(job.Url);
        //            Thread.Sleep(5000);

        //            BaseReader body = new BaseReader(driver);

        //            body.AddChildren(jobReader);

        //            JobInputModel data = body.GetJob();
        //            Job j = null;
        //            if (jobReader.DateFormat != null) j = data.ToJob(jobReader.DateFormat);
        //            else j = data.ToJob();
        //            j.Link = job.Url;
        //            j.State = job.State;
        //            jobs.Add(j);

        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine(ex.Message );
        //            Console.WriteLine(ex.StackTrace );
        //            continue;
        //        }
        //        //Reader page = new Reader(driver);

        //    }
        //    return jobs;
        //}

    }
}
