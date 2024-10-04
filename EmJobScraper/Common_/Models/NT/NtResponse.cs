namespace Common.Models.NT
{
    public class NtResponse
    {
        public bool success { get; set; }
        public string successMessage { get; set; }
        public string errorMessage { get; set; }
        public List<Advertisement> data { get; set; }
    }
}
