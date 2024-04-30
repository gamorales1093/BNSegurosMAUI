using System;
using MvvmHelpers;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Prism.Mvvm;
using System.Collections.ObjectModel;

namespace BNSegurosMAUI.Models
{
    public class Agent : BindableBase
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string MobilePhone { get; set; }
        public string OfficePhone { get; set; }
        public string AssistantPhone { get; set; }
        public string Email { get; set; }
        public string AttentionZone { get; set; }
        public string License { get; set; }
        public string Image { get; set; }

        public string FullName => string.Format("{0} {1}", Name, LastName);

        private bool _isCellExpanded;
        public bool IsCellExpanded
        {
            get => _isCellExpanded;
            set => SetProperty(ref _isCellExpanded, value);
        }

        private int _heightValue = 80;
        public int HeightValue
        {
            get => _heightValue;
            set => SetProperty(ref _heightValue, value);
        }
        
    }

    public class EmployeeGroup : ObservableCollection<Agent>, INotifyPropertyChanged
    {
        public string GroupTitle { get; set; }
        public string FooterTitle { get; set; }

        private string _groupIcon = "down_arrow";
        public string GroupIcon
        {
            get => _groupIcon;
            set => SetProperty(ref _groupIcon, value);
        }

        public EmployeeGroup(string groupTitle, List<Agent> employees, string footerTitle = "") : base(employees)
        {
            GroupTitle = groupTitle;
            FooterTitle = footerTitle;
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
      [CallerMemberName] string propertyName = "",
      Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
