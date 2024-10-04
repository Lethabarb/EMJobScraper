using System.Text.RegularExpressions;

namespace ScraperAPI.Utilities
{
    public class RegexHelper
    {
        private static string salaryRegex = @"\$\d{1,3}(,?\d{1,3})*(.\d{1,2})? ?(to)?(-)? ?(\$\d{1,3}(,?\d{1,3})*(.\d{1,2})?)?";
        private static string afterColon = @":(.*)";

        public static string GetSalary(string input)
        {
            Regex regex = new Regex(salaryRegex);
            Match m = regex.Match(input);
            return m.Value;
        }

        public static string GetAfterColon(string input, string key)
        {
            Regex regex = new Regex(key + afterColon);
            Match m = regex.Match(input);
            return m.Groups[1].Value;
        }

    }
}
