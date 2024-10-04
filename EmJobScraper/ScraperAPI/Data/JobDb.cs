using Microsoft.EntityFrameworkCore;

namespace ScraperAPI.Data
{
    public class JobDb : DbContext
    {
        public DbSet<JobDb> Jobs { get; set; }
    }
}
