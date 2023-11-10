namespace MedicalWeb.Models
{
    public class UploadingFile
    {
        public string DocumentName { get; set; }

        public IFormFile file { get; set; }
    }
}
