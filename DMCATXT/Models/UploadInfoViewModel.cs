namespace DMCATXT.Models
{
    public class UploadInfoViewModel
    {
        public UploadInfoViewModel()
        {
            uploadInfo = new UploadInfo();
            customers = new Customers();
        }

        public UploadInfo uploadInfo { get; set; }
        public Customers customers { get; set; }

    }
}
