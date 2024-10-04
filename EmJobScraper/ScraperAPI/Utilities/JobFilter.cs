using Common.Entities;
using Common.Models;

namespace ScraperAPI.Utilities
{
    public class JobFilter
    {
        private static readonly List<string> _filters = new List<string> {"education", "teach", "legal", "regrister", "executive", "school",
            "technology", "cleric", "payroll", "client", "admin", "data", "engineer", "motor", "mechanic", "health",
            "nurse", "social", "lawyer", "therap", "clinic", "cyber", "budget", "Procurement", "test",
            "finance", "ict", "business", "financial", "enterprise", "hostel", "graphic", "med", "cook", "hire",
            "human resource", "family", "cost", "guardian", "ologist", "regrist" };
        public static bool IsValid(JobInputModel job)
        {
            bool valid = true;
            string hitFilter = "";
            foreach (string f in _filters)
            {
                if (job.Title != null && job.Title.ToLower().Contains(f))
                {
                    valid = false;
                    hitFilter = f;
                    Console.WriteLine($"{f}");
                }
                if (job.Organization != null && job.Organization.ToLower().Contains(f))
                {
                    valid = false;
                    hitFilter = f;
                    Console.WriteLine($"{f}");
                }
            }
            return valid;
        }
        public static bool IsValid(Job job)
        {
            bool valid = true;
            string hitFilter = "";
            foreach (string f in _filters)
            {
                if (job.Title != null && job.Title.ToLower().Contains(f))
                {
                    valid = false;
                    hitFilter = f;
                    Console.WriteLine($"{f}");
                }
                if (job.Organization != null && job.Organization.ToLower().Contains(f))
                {
                    valid = false;
                    hitFilter = f;
                    Console.WriteLine($"{f}");
                }
            }
            return valid;
        }
    }
}
