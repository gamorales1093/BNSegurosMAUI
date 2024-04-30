using System;
using System.Globalization;
using Prism.Mvvm;

namespace BNSegurosMAUI.Models
{
    public class InsuranceQuote : BindableBase
    {
        public string Name { get; set; }
        public string Benefits { get; set; }
        public string Terms { get; set; }
        public string Amount { get; set; }
        public string Currency { get; set; }
        public string QuotationId { get; set; }

        public string FormattedAmount
        {
            get
            {
                try
                {
                    var formattedAmount = string.Format(CultureInfo.InvariantCulture, "{0:0,0.00}", decimal.Parse(Amount, CultureInfo.InvariantCulture));
                    return string.Format("{0}{1}", Currency, formattedAmount);
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        
        private string _backGroundColor;
        public string BackGroundColor
        {
            get => _backGroundColor;
            set => SetProperty(ref _backGroundColor, value);
        }

        private bool _isCellExpanded;
        public bool IsCellExpanded
        {
            get => _isCellExpanded;
            set => SetProperty(ref _isCellExpanded, value);
        }
    }
}
