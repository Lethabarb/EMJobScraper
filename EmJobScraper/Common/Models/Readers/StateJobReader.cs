using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models.Readers
{
    public class StateJobReader
    {
        public By Title { get; set; }
        public By Location { get; set; }
        public By Organization { get; set; }
        public By ClosureDate { get; set; }
        public By Salary { get; set; }
        public By Table { get; set; }
    }
}
