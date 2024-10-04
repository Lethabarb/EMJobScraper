using Common.Hub;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace ScraperAPI.Controllers
{
    public class JobHub : Hub<IJobClient>
    {
        public void Start()
        {

        }
        public async override Task OnConnectedAsync()
        {
            await Clients.All.Update("update");
        }
    }
}
