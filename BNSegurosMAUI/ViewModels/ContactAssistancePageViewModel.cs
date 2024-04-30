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

namespace BNSegurosMAUI.ViewModels
{
    class ContactAssistancePageViewModel : BasePageViewModel, IActiveAware
    {
        public ContactAssistancePageViewModel(INavigationService navigationService, IPageDialogService pageDialog)
            : base(navigationService, pageDialog)
        {
            Title = TextResources.Contact_InsuranceAsistanceLabel;

            MoreInfoItemCommand = new DelegateCommand<Assistance>((args) => MoreInfoItemSelected(args));
            CallRedirectCommand = new DelegateCommand<string>((args) => CallCommand(args));
            AssistanceContacts = new List<Assistance>();
            DisplayedItems = new ObservableCollection<Assistance>();
            CloseCommand = new DelegateCommand(ClosePage);
        }

        public event EventHandler IsActiveChanged;
        public DelegateCommand<string> CallRedirectCommand { get; }
        public DelegateCommand CloseCommand { get; private set; }

        private bool _isActive;
        public bool IsActive
        {
            get { return _isActive; }
            set { SetProperty(ref _isActive, value, RaiseIsActiveChanged); }
        }

        private List<Assistance> _assistanceContacts;
        public List<Assistance> AssistanceContacts
        {
            get => _assistanceContacts;
            set => SetProperty(ref _assistanceContacts, value);
        }

        private ObservableCollection<Assistance> _displayedItems;
        public ObservableCollection<Assistance> DisplayedItems
        {
            get => _displayedItems;
            set => SetProperty(ref _displayedItems, value);
        }

        public DelegateCommand<Assistance> MoreInfoItemCommand { get; }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                SetProperty(ref _searchText, value);
                FilterResultsByText();
            }
        }

        protected virtual async void RaiseIsActiveChanged()
        {
            IsActiveChanged?.Invoke(this, EventArgs.Empty);

            if (IsActive && AssistanceContacts?.Count == 0)
            {
                await GetAssistanceInformation();
            }
        }

        private void MoreInfoItemSelected(Assistance item)
        {
            item.IsCellExpanded = !item.IsCellExpanded;
        }

        private void FilterResultsByText()
        {
            var results = new List<Assistance>(AssistanceContacts);
            if (!string.IsNullOrEmpty(SearchText))
            {
                var text = SearchText.ToLowerInvariant();
                results = results.FindAll(x => x.Name.ToLowerInvariant().Contains(text));
            }
            DisplayedItems = new ObservableCollection<Assistance>(results);
        }

        private List<Assistance> AddDefaultImagesTo(List<Assistance> assistances) {
            for (int i = 0; i < assistances.Count; i++) {
                if (assistances[i].Image == null) {
                    assistances[i].Image = "icdflassis"; //"IcDflAssis";
                }
                
            }
            return assistances;
        }

        private async Task GetAssistanceInformation()
        {
            if (await IsConnected())
            {
                ShowProgressDialog(TextResources.ProgressDialogDefaultLoading);

                var service = WebServiceFactory.Create(WebServiceType.GetAssistance);
                var response = await service.Execute<GetAssistanceResponse>();

                DismissProgressDialog();

                if (response.IsSuccessful && response.Data?.Count > 0)
                {
                    response.Data = AddDefaultImagesTo(response.Data);
                    AssistanceContacts = response.Data;
                    FilterResultsByText();
                }
                else
                {
                    await DisplayAlert(TextResources.AlertErrorTitle, response.ErrorMessage, TextResources.AlertAcceptButton, AlertIconType.Error);
                }
            }
        }

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
