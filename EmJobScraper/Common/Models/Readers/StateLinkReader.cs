using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models.Readers
{
    public class StateLinkReader
    {
        public By JobCard { get; set; }

        public By Url { get; set; }

        public By Title { get; set; }

        public StateLinkReader() { }
    }
}
