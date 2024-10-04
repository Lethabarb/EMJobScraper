using CsvHelper.Configuration;

namespace Common.Entities
{
    class JobClassMap : ClassMap<Job>
    {
        public JobClassMap()
        {

            Map(m => m.Title);
            Map(m => m.Location);
            try
            {
                Map(m => m.State.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Map(m => m.Organization);
            Map(m => m.Salary);
            Map(m => m.Link);
        }
    }
}
