using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models.Enums
{
    public static class StateSelector
    {
        public static string getBaseLink(this State state)
        {
            switch (state)
            {
                case State.NSW:
                    return "https://iworkfor.nsw.gov.au/jobs/all-keywords/all-agencies/all-organisations-entities/all-categories/all-locations/all-worktypes?jobcategoryid=10399,10357,10375,10378,10374,10356,10380,10358,10368,10379,10627,16567&sortby=RelevanceDesc&pagesize=100";
                case State.QLD:
                    return "https://smartjobs.qld.gov.au/jobtools/jncustomsearch.jobsearch?in_organid=14904";
                case State.VIC:
                    return "https://careers.vic.gov.au/";
                case State.ACT:
                    return "https://www.jobs.act.gov.au/jobs/worksafe-act/permanent/65803,-several";
                case State.NT:
                    return "";
                case State.SA:
                    return "https://iworkfor.sa.gov.au/";
                case State.WA:
                    return "https://search.jobs.wa.gov.au/page.php?pageID=215";
                case State.TAS:
                    return "";
                case State.FED:
                    return "https://www.apsjobs.gov.au/s/";
            }
            throw new Exception("not a state");
        }

    }
}
