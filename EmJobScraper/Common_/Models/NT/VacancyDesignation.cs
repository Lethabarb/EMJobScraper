namespace Common.Models.NT
{
    public class VacancyDesignation
    {
        public string advertisedCode { get; set; }
        public double packageMin { get; set; }
        public string packageMinformatted { get; set; }
        public double packageMax { get; set; }
        public string packageMaxformatted { get; set; }
        public string packageCodeDescription { get; set; }
        public string packageCodeValue { get; set; }
        public double salaryMin { get; set; }
        public string salaryMinformatted { get; set; }
        public double salaryMax { get; set; }
        public string salaryMaxformatted { get; set; }
        public bool hideDescription { get; set; }
        public bool hideRemunerationMinimum { get; set; }
        public bool hideRemunerationMaximum { get; set; }
        public bool hideSalary { get; set; }
        public string getDesignationDisplayedForSearch { get; set; }
    }
}
