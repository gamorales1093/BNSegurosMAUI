using System;
using Prism.Mvvm;

namespace BNSegurosMAUI.Models
{
    public class Procedures : BindableBase
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        private bool _isCellExpanded;
        public bool IsCellExpanded
        {
            get => _isCellExpanded;
            set => SetProperty(ref _isCellExpanded, value);
        }
    }
}
