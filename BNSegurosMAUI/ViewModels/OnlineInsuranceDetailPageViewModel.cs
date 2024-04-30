using System;
using BNSegurosMAUI.Helpers;
using BNSegurosMAUI.Models;
using BNSegurosMAUI.Resources.Text;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System.Diagnostics;

namespace BNSegurosMAUI.ViewModels
{
	public class OnlineInsuranceDetailPageViewModel : BasePageViewModel, IInitialize
    {
        #region Constructor
        public OnlineInsuranceDetailPageViewModel(INavigationService navigationService, IPageDialogService pageDialog)
            : base(navigationService, pageDialog)
        {
            Title = TextResources.OnlineInsurances_TitleLabel;

            CloseCommand = new DelegateCommand(ClosePage);
            BuyInsuranceCommand = new DelegateCommand(GoToBuyInsurancePage);

            Debug.WriteLine("Constructor: OnlineInsuranceDetailPageViewModel");
        }
        #endregion

        #region Variables
        private INavigationParameters _navigationParams;
        private OnlineInsurance _onlineInsurance;
        public OnlineInsurance OnlineInsurance
        {
            get { return _onlineInsurance; }
            set { SetProperty(ref _onlineInsurance, value); }
        }
        private bool _showBuyButton;
        public bool ShowBuyButton { get => _showBuyButton; set => SetProperty(ref _showBuyButton, value); }

        public DelegateCommand CloseCommand { get; private set; }
        public DelegateCommand BuyInsuranceCommand { get; private set; }
        #endregion

        #region Methods
        private async void ClosePage()
        {
            await Navigation.GoBackAsync();
            Debug.WriteLine("Return to: ClosePage");
        }

        private async void GoToBuyInsurancePage()
        {
            // Pass info in to OnlineInsuranceDetailPage
            var onlineInsurance = OnlineInsurance;

            var navigationParams = new NavigationParameters {
                { "OnlineInsurance", onlineInsurance }
            };

            await Navigation.NavigateAsync("BuyInsurancePage", navigationParams);
            Debug.WriteLine("Go to: BuyInsurancePage");
        }
        #endregion

        #region Initialize
        public void Initialize(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("OnlineInsurance"))
            {
                _navigationParams = parameters;

                OnlineInsurance = parameters.GetValue<OnlineInsurance>("OnlineInsurance");
                ShowBuyButton = OnlineInsurance.ComingSoon ? false : true;
            }
        }
        #endregion
    }
}
