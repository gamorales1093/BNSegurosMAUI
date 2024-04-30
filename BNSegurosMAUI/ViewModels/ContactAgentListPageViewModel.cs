using BNSegurosMAUI.Helpers;
using BNSegurosMAUI.Models;
using BNSegurosMAUI.Resources.Text;
using BNSegurosMAUI.WebServices;
using Prism;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.ApplicationModel.Communication;
using System.Windows.Input;

namespace BNSegurosMAUI.ViewModels
{
    public class ContactAgentListPageViewModel : BasePageViewModel, IActiveAware
    {
        public ContactAgentListPageViewModel(INavigationService navigationService, IPageDialogService pageDialog)
            : base(navigationService, pageDialog)
        {
            FirstItemCommand = new DelegateCommand<Agent>((args) => OnPrimaryItemSelected(args));
            Title = TextResources.Contact_InsurancesLabel;

            CallRedirectCommand = new DelegateCommand<string>((args) => CallCommand(args));
            MailRedirectCommand = new DelegateCommand<string>((args) => MailCommand(args));
            CloseCommand = new DelegateCommand(ClosePage);

            DisplayedItems = new ObservableCollection<Agent>();
        }

        public event EventHandler IsActiveChanged;
        public DelegateCommand<string> CallRedirectCommand { get; }
        public DelegateCommand<string> MailRedirectCommand { get; }
        public DelegateCommand CloseCommand { get; private set; }

        private bool _isActive;
        public bool IsActive
        {
            get { return _isActive; }
            set { SetProperty(ref _isActive, value, RaiseIsActiveChanged); }
        }

        private ObservableCollection<Agent> _displayedItems;
        public ObservableCollection<Agent> DisplayedItems
        {
            get => _displayedItems;
            set => SetProperty(ref _displayedItems, value);
        }

        private List<EmployeeGroup> _employees;
        public List<EmployeeGroup> Employees
        {
            get => _employees;
            set => SetProperty(ref _employees, value);
        }

        //public List<EmployeeGroup> Employees { get; set; } = new List<EmployeeGroup>();

        public DelegateCommand<Agent> FirstItemCommand { get; }

        protected virtual async void RaiseIsActiveChanged()
        {
            IsActiveChanged?.Invoke(this, EventArgs.Empty);

            if (IsActive && DisplayedItems?.Count == 0)
            {
                await GetAgentsInformation();
            }
        }

        private void OnPrimaryItemSelected(Agent item)
        {

                item.IsCellExpanded = !item.IsCellExpanded;
            if (item.IsCellExpanded == true)
                item.HeightValue = 350;
            else
                item.HeightValue = 80;


        }

        private async Task GetAgentsInformation()
        {
            if (await IsConnected())
            {
                ShowProgressDialog(TextResources.ProgressDialogDefaultLoading);

                var service = WebServiceFactory.Create(WebServiceType.GetAgents);
                var response = await service.Execute<GetAgentsResponse>();

                DismissProgressDialog();

                if (response.IsSuccessful && response.Data?.Count > 0)
                {
                    DisplayedItems = new ObservableCollection<Agent>(response.Data);

                    Employees = new List<EmployeeGroup>();

                    var groupedData = DisplayedItems.GroupBy(f => f.FullName).Select(f => new EmployeeGroup(f.Key.ToString(), f.ToList()));
                    Employees.AddRange(groupedData);
                }
                else
                {
                    await DisplayAlert(TextResources.AlertErrorTitle, response.ErrorMessage, TextResources.AlertAcceptButton, AlertIconType.Error);
                }
            }
        }

        public ICommand AddOrRemoveGroupDataCommand => new Command<EmployeeGroup>((item) =>
        {
            if (item.GroupIcon == "down_arrow")
            {
                item.Clear();
                item.GroupIcon = "up_arrow";
            }
            else
            {
                var recordsTobeAdded = DisplayedItems.Where(f => f.FullName.ToLower().Contains(item.GroupTitle.ToLower())).ToList();
                //   item.AddRange(recordsTobeAdded);

                foreach (var itemGroup in recordsTobeAdded)
                {
                    item.Add(itemGroup);
                }
                item.GroupIcon = "down_arrow";
            }
        });

        private async void CallCommand(string phone)
        {
            try
            {
                PhoneDialer.Open(phone);
            }
            catch (ArgumentNullException)
            {
                await DisplayAlert(TextResources.Contact_CallInvalidErrorTitle, TextResources.Contact_CallInvalidErrorMessage, TextResources.Alert_AcceptButtonText, AlertIconType.Error);
            }
            catch (FeatureNotSupportedException)
            {
                await DisplayAlert(TextResources.Contact_CallInvalidErrorTitle, TextResources.Contact_CallSupportPhoneErrorMessage, TextResources.Alert_AcceptButtonText, AlertIconType.Error);
            }
            catch (Exception)
            {
                await DisplayAlert(TextResources.Contact_CallInvalidErrorTitle, TextResources.Contact_CallErrorMessage, TextResources.Alert_AcceptButtonText, AlertIconType.Error);
            }
        }

        private async void MailCommand(string mail)
        {
            try
            {
                string UrlAux = "mailto:" + mail;

                if (await Launcher.CanOpenAsync(UrlAux))
                {
                    await Launcher.OpenAsync(UrlAux);
                }
                else
                {
                    await DisplayAlert(TextResources.Contact_MailInvalidErrorTitle, TextResources.Contact_MailErrorMessage, TextResources.Alert_AcceptButtonText, AlertIconType.Error);
                }
            }
            catch (Exception)
            {
                await DisplayAlert(TextResources.Contact_MailInvalidErrorTitle, TextResources.Contact_MailErrorMessage, TextResources.Alert_AcceptButtonText, AlertIconType.Error);
            }
        }

        private async void ClosePage()
        {
            if (SessionInfo.GetInstance().FromInsuranceDetail == true)
                await Navigation.GoBackAsync();
            else
                Application.Current.MainPage = new AppShell();

            Debug.WriteLine("Return to: ClosePage");
        }
    }
}
