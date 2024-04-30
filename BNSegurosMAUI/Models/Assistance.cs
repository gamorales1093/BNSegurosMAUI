using System;
using Prism.Mvvm;

namespace BNSegurosMAUI.Models
{
    public class Assistance : BindableBase
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string SecondPhoneNumber { get; set; }
        public bool SecondPhoneNumberVisible { get { return IsNumberVisible(SecondPhoneNumber); } }
        public string TypeOfAssistance { get; set; }
        public string Detail { get; set; }
        public string Image { get; set; }
        private bool _isCellExpanded;
        public bool IsCellExpanded
        {
            get => _isCellExpanded;
            set => SetProperty(ref _isCellExpanded, value);
        }

        public Boolean IsNumberVisible(string number)
        {
            if (number.Length == 0)
                return false;
            else
                return true;
        }
    }
}
