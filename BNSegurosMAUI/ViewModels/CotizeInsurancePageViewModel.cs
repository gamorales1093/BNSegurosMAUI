
using BNSegurosMAUI.Helpers;
using BNSegurosMAUI.Models;
using BNSegurosMAUI.Resources.Text;
using BNSegurosMAUI.WebServices;
using CommunityToolkit.Maui.Core;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Contact = BNSegurosMAUI.Models.Contact;

namespace BNSegurosMAUI.ViewModels
{
    public class CotizeInsurancePageViewModel : BasePageViewModel, IInitialize
    {
        #region Constructor
        public CotizeInsurancePageViewModel(INavigationService navigationService, IPageDialogService pageDialog, IDialogService dialogService, IPopupService popupServices)
            : base(navigationService, pageDialog, dialogService, popupServices)
        {
            FirstItemCommand = new DelegateCommand<InsuranceQuote>((args) => OnPrimaryItemSelected(args));
            SendInsuranceQuoteCommand = new DelegateCommand(async () => await SendInsuranceQuoteCommandExecute());
            CloseCommand = new DelegateCommand(ClosePage);
        }
        #endregion

        private Insurance _insurance;

        public DelegateCommand<InsuranceQuote> FirstItemCommand { get; }
        public DelegateCommand SendInsuranceQuoteCommand { get; }
        public DelegateCommand CloseCommand { get; private set; }

        private List<InsuranceQuote> _insuranceQuotes;
        public List<InsuranceQuote> InsuranceQuotes
        {
            get => _insuranceQuotes;
            set => SetProperty(ref _insuranceQuotes, value);
        }

        private string _mail;
        public string Mail
        {
            get => _mail;
            set => SetProperty(ref _mail, value);
        }

        private string _phoneNumber;
        public string PhoneNumber
        {
            get => _phoneNumber;
            set => SetProperty(ref _phoneNumber, value);
        }

        private void OnPrimaryItemSelected(InsuranceQuote item)
        {
            item.IsCellExpanded = !item.IsCellExpanded;
            if (item.IsCellExpanded) {
                item.BackGroundColor = "#F0F0F0";
            } else {
                item.BackGroundColor = "#FFFFFF";
            }
        }

        private async Task SendInsuranceQuoteCommandExecute()
        {
            try
            {
                if (await IsConnected(true))
                {
                    ShowProgressDialog(TextResources.InsuranceQuote_ProgressDialogSendingData);

                    var request = new SendQuoteRequest { QuotationId = InsuranceQuotes[0].QuotationId };
                    var service = WebServiceFactory.Create(WebServiceType.SendQuote, null, request);
                    var response = await service.Execute<SendQuoteResponse>();

                    DismissProgressDialog();

                    if (response.IsSuccessful)
                    {
                        await DisplayAlert(TextResources.InsuranceQuote_PopupTitle, response.Message, TextResources.AlertAcceptButton, AlertIconType.Mail);
                        await Navigation.NavigateAsync("/HomePage");
                        Debug.WriteLine("Return to: HomePage");
                    }
                    else
                    {
                        await DisplayAlert(TextResources.AlertErrorTitle, response.ErrorMessage, TextResources.AlertAcceptButton, AlertIconType.Error);
                    }
                }
            }
            catch (Exception)
            {
                await DisplayAlert(TextResources.InsuranceQuote_PopupErrorTitle, TextResources.InsuranceQuote_PopupErrorMessage, TextResources.Alert_AcceptButtonText, AlertIconType.Error);
            }
        }

        public List<InsuranceQuote> AddColorBackGround(List<InsuranceQuote> insuranceQuotes) {
            for (int i = 0; i < insuranceQuotes.Count; i++) {
                insuranceQuotes[i].BackGroundColor = "#FFFFFF";
            }
            return insuranceQuotes;
        }

        public void Initialize(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("Quotes") && parameters.ContainsKey("Insurance") && parameters.ContainsKey("Contact"))
            {
                _insurance = parameters.GetValue<Insurance>("Insurance");
                Title = _insurance.Name;

                var contactInfo = parameters.GetValue<Contact>("Contact");
                if (contactInfo != null)
                {
                    Mail = contactInfo.Email;
                    PhoneNumber = contactInfo.PhoneNumber;
                }

                InsuranceQuotes = AddColorBackGround(parameters.GetValue<List<InsuranceQuote>>("Quotes"));
            }
        }

        private async void ClosePage()
        {
            await Navigation.GoBackAsync();
            Debug.WriteLine("Return to: ClosePage");
        }
    }
}
