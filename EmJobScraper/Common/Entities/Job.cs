using Common.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities
{
    public class Job
    {
        private static DateOnly startDate = new DateOnly(2024, 1, 1);
        public string Title { get; set; }
        public string Location { get; set; }
        public State State { get; set; }
        public string Organization { get; set; }
        public DateOnly ClosureDate { get; set; }
        public double DateValue 
        {
            get
            {
                return (ClosureDate.ToDateTime(TimeOnly.MinValue) - startDate.ToDateTime(TimeOnly.MinValue)).TotalDays;
            }
        }
        public string Salary { get; set; }
        public string Link { get; set; }

        public Job() { }
    }
}
