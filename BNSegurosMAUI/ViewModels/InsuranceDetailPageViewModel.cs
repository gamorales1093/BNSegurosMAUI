using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using BNSegurosMAUI.Helpers;
using BNSegurosMAUI.Models;
using BNSegurosMAUI.Resources.Text;
using BNSegurosMAUI.WebServices;
using CommunityToolkit.Maui.Core;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace BNSegurosMAUI.ViewModels
{
    public class InsuranceDetailPageViewModel : BasePageViewModel, IInitialize
    {
        #region Constructor
        public InsuranceDetailPageViewModel(INavigationService navigationService, IPageDialogService pageDialog, IDialogService dialogService, IPopupService popupServices)
            : base(navigationService, pageDialog, dialogService, popupServices)
        {
            RequestInsuranceCommand = new DelegateCommand(RequestInsurance);
            CloseCommand = new DelegateCommand(ClosePage);
            GoToContactPageCommand = new DelegateCommand(GoToContactPage);
        }
        #endregion

        #region Variables
        private INavigationParameters _navigationParams;

        private Insurance _insurance;
        public DelegateCommand CloseCommand { get; private set; }
        public DelegateCommand GoToContactPageCommand { get; private set; }
        public Insurance Insurance
        {
            get { return _insurance; }
            set { SetProperty(ref _insurance, value); }
        }

        private string _buttonName;
        public string ButtonName
        {
            get { return _buttonName; }
            set { SetProperty(ref _buttonName, value); }
        }

        public DelegateCommand RequestInsuranceCommand { get; private set; }
        #endregion

        #region Methods
        private async void RequestInsurance()
        {
            switch (Insurance.Type)
            {
                case InsuranceType.MedicalExpense:
                    await RequestMedicalExpenseInsurance();
                    break;
                case InsuranceType.Unemployment:
                    await RequestUnemploymentInsurance();
                    break;
                case InsuranceType.Home:
                case InsuranceType.Other:
                    await RequestOtherInsurance();
                    break;
                case InsuranceType.Bicycle:
                    await RequestBicycleInsurance();
                    break;
                default:
                    await Navigation.NavigateAsync("RequestInsurancePage", _navigationParams);
                    break;
            }
        }

        private async Task RequestMedicalExpenseInsurance()
        {
            List<MedicalInsuranceClass> medicalClasses = null;
            var medicalCategories = await GetMedicalCategories();

            if (medicalCategories != null)
            {
                medicalClasses = await GetMedicalClasses();
            }

            if (medicalClasses != null)
            {
                _navigationParams.Add("MedicalCategories", medicalCategories);
                _navigationParams.Add("MedicalClasses", medicalClasses);

                await Navigation.NavigateAsync("RequestInsurancePage", _navigationParams);
            }
        }

        private async Task RequestOtherInsurance()
        {
            var otherInsurances = await GetOtherInsurances();
            if (otherInsurances != null)
            {
                _navigationParams.Add("OtherInsurances", otherInsurances);
                await Navigation.NavigateAsync("RequestInsurancePage", _navigationParams);
            }
        }

        private async Task RequestUnemploymentInsurance()
        {
            var currencies = await GetCurrencies();
            if (currencies != null)
            {
                _navigationParams.Add("Currencies", currencies);
                await Navigation.NavigateAsync("RequestInsurancePage", _navigationParams);
            }
        }

        private async Task RequestBicycleInsurance()
        {
            var currencies = await GetCurrencies();
            if (currencies != null)
            {
                _navigationParams.Add("Currencies", currencies);
                await Navigation.NavigateAsync("RequestInsurancePage", _navigationParams);
            }
        }

        private async Task<List<Currency>> GetCurrencies()
        {
            if (await IsConnected(true))
            {
                ShowProgressDialog(TextResources.ProgressDialogDefaultLoading);

                var service = WebServiceFactory.Create(WebServiceType.GetCurrencies);
                var response = await service.Execute<GetCurrenciesResponse>();

                DismissProgressDialog();

                if (response.IsSuccessful && response.Data?.Count > 0)
                {
                    return response.Data;
                }
                else
                {
                    await DisplayAlert(TextResources.AlertErrorTitle, response.ErrorMessage, TextResources.AlertAcceptButton, AlertIconType.Error);
                }
            }
            return null;
        }

        private async Task<List<MedicalInsuranceCategory>> GetMedicalCategories()
        {
            if (await IsConnected(true))
            {
                ShowProgressDialog(TextResources.ProgressDialogDefaultLoading);

                var service = WebServiceFactory.Create(WebServiceType.GetMedicalExpensesCategories);
                var response = await service.Execute<GetMedicalExpensesCategoriesResponse>();

                DismissProgressDialog();

                if (response.IsSuccessful && response.Data?.Count > 0)
                {
                    return response.Data;
                }
                else
                {
                    await DisplayAlert(TextResources.AlertErrorTitle, response.ErrorMessage, TextResources.AlertAcceptButton, AlertIconType.Error);
                }
            }
            return null;
        }

        private async Task<List<MedicalInsuranceClass>> GetMedicalClasses()
        {
            if (await IsConnected(true))
            {
                ShowProgressDialog(TextResources.ProgressDialogDefaultLoading);

                var service = WebServiceFactory.Create(WebServiceType.GetMedicalExpensesClasses);
                var response = await service.Execute<GetMedicalExpensesClassesResponse>();

                DismissProgressDialog();

                if (response.IsSuccessful && response.Data?.Count > 0)
                {
                    return response.Data;
                }
                else
                {
                    await DisplayAlert(TextResources.AlertErrorTitle, response.ErrorMessage, TextResources.AlertAcceptButton, AlertIconType.Error);
                }
            }
            return null;
        }

        private async Task<List<OtherInsurance>> GetOtherInsurances()
        {
            if (await IsConnected(true))
            {
                ShowProgressDialog(TextResources.ProgressDialogDefaultLoading);

                var service = WebServiceFactory.Create(WebServiceType.GetOtherInsurances);
                var response = await service.Execute<GetOtherInsurancesResponse>();

                DismissProgressDialog();

                if (response.IsSuccessful && response.Data?.Count > 0)
                {
                    return response.Data;
                }
                else
                {
                    await DisplayAlert(TextResources.AlertErrorTitle, response.ErrorMessage, TextResources.AlertAcceptButton, AlertIconType.Error);
                }
            }
            return null;
        }
        #endregion

        public void Initialize(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("Insurance") && parameters.ContainsKey("TermsAndConditions"))
            {
                _navigationParams = parameters;

                Insurance = parameters.GetValue<Insurance>("Insurance");

                //if (Insurance.Id == 4)
                // ButtonName = TextResources.InsuranceQuote_ButtonLabel;
                //else
                ButtonName = TextResources.QuoteInsuranceButton;

                Title = Insurance.Name;
            }
        }

        private async void ClosePage()
        {
            await Navigation.GoBackAsync();
            Debug.WriteLine("Return to: ClosePage");
        }

        private async void GoToContactPage()
        {
            SessionInfo.GetInstance().FromInsuranceDetail = true;
            await Navigation.NavigateAsync("ContactPage");
            Debug.WriteLine("Go to: GoToContactPage");
        }
    }
}
