using Common.Models.NT;

namespace ScraperAPI.Services
{
    public class HttpService
    {
        private HttpClient client;

        public HttpService()
        {
            client = new HttpClient();
            client.Timeout = TimeSpan.FromDays(1);
        }

        public async Task<NtResponse> GetNTData()
        {
            var res = await client.PostAsync("https://jobs.nt.gov.au/Home/Search", null);
            return await res.Content.ReadFromJsonAsync<NtResponse>();
        }
    }
}
