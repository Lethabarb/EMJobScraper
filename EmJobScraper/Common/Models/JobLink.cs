using Common.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class JobLink
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public State State { get; set; }

        private JobLink(string title, string url, State state)
        {
            Title = title;
            Url = url;
            State = state;
        }

        public static JobLink Nsw(string title, string url)
        {
            return new JobLink(title, url, State.NSW);
        }
        public static JobLink Qld(string title, string url)
        {
            return new JobLink(title, url, State.QLD);
        }
        public static JobLink Vic(string title, string url)
        {
            return new JobLink(title, url, State.VIC);
        }
        public static JobLink Act(string title, string url)
        {
            return new JobLink(title, url, State.ACT);
        }
        public static JobLink Nt(string title, string url)
        {
            return new JobLink(title, url, State.NT);
        }
        public static JobLink Sa(string title, string url)
        {
            return new JobLink(title, url, State.SA);
        }
        public static JobLink Wa(string title, string url)
        {
            return new JobLink(title, url, State.WA);
        }
        public static JobLink Tas(string title, string url)
        {
            return new JobLink(title, url, State.TAS);
        }
        public static JobLink Fed(string title, string url)
        {
            return new JobLink(title, url, State.FED);
        }
    }
}
