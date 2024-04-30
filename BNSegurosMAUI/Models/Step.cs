using System;
namespace BNSegurosMAUI.Models
{
    public class Step
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public string PdfFile { get; set; }
        public string OpaForm { get; set; }

        public bool ShowDownloadPdfFile { get { return ValidatePdfUrl(PdfFile); } }
        public bool ValidatePdfUrl(string url)
        {
            if (url != string.Empty)
                return true;
            else
                return false;
        }

        public bool ShowUploadPdfFile { get { return ValidateOpaUrl(OpaForm); } }
        public bool ValidateOpaUrl(string url)
        {
            if (url != string.Empty)
                return true;
            else
                return false;
        }
    }
}
