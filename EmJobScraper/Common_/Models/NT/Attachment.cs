namespace Common.Models.NT
{
    public class Attachment
    {
        public int id { get; set; }
        public string fileExtension { get; set; }
        public string fileURL { get; set; }
        public string fileCategory { get; set; }
        public string fileSize { get; set; }
        public int fileId { get; set; }
        public bool rpFile { get; set; }
    }
}
