using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Hub
{
    public interface IJobClient
    {
        Task StartScrape();
        Task Update(string message);
        Task Finish(string res);
    }
}
