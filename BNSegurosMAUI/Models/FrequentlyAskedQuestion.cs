using System;
using Prism.Mvvm;
using System.Collections.ObjectModel;

namespace BNSegurosMAUI.Models
{
    public class FrequentlyAskedQuestion : BindableBase
    {
        public int Id;
        public string Question { get; set; }
        public string Answer { get; set; }
        public bool RedirectInsurances { get; set; }
        public QuestionType QuestionType { get; set; }
        public bool Active { get; set; }
        public string PdfFile { get; set; }
        public string OpaForm { get; set; }

        private bool _isCellExpanded;
        public bool IsCellExpanded
        {
            get => _isCellExpanded;
            set => SetProperty(ref _isCellExpanded, value);
        }

        public bool HavePdf { get { return ValidatePdfUrl(PdfFile); } }
        public bool ValidatePdfUrl(string url)
        {
            if (url != string.Empty)
                return true;
            else
                return false;
        }
    }
}
