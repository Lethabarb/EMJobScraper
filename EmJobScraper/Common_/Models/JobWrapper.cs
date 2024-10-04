using OpenQA.Selenium;

namespace Common.Models
{
    public class JobWrapper
    {
        public JobInputModel Input { get; set; } = new JobInputModel();
        public IWebElement Parent { get; set; }
        public IWebElement Element { get; set; }

    }
}
