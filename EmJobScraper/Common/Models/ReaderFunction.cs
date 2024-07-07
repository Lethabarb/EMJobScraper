using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class ReaderFunction<T>
    {
        By By { get; set; }
        T Value { get; set; }
    }
}
