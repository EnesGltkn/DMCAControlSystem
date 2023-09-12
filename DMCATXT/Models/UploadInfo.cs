using System.ComponentModel.DataAnnotations.Schema;

namespace DMCATXT.Models
{
    [Table("Upload_Table")]
    public class UploadInfo
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }

        public string? CustomerName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string FilePath { get; set; }

        [NotMapped]
        public IFormFile File { get; set; }

        public string Approval { get; set; }

    }
    
}
