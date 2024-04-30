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
	public class BuyInsurancePageViewModel : BasePageViewModel, IInitialize
    {
        #region Constructor
        public BuyInsurancePageViewModel(INavigationService navigationService, IPageDialogService pageDialog)
            : base(navigationService, pageDialog)
        {
            CloseCommand = new DelegateCommand(ClosePage);
            Debug.WriteLine("Constructor: BuyInsurancePageViewModel");
        }
        #endregion

        #region Variables
        public DelegateCommand CloseCommand { get; private set; }
        private INavigationParameters _navigationParams;
        private OnlineInsurance _onlineInsurance;
        public OnlineInsurance OnlineInsurance
        {
            get { return _onlineInsurance; }
            set { SetProperty(ref _onlineInsurance, value); }
        }
        #endregion

        #region Methods
        private async void ClosePage()
        {
            await Navigation.GoBackAsync();
            Debug.WriteLine("Return to: ClosePage");
        }
        #endregion

        #region Initialize
        public void Initialize(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("OnlineInsurance"))
            {
                _navigationParams = parameters;

                OnlineInsurance = parameters.GetValue<OnlineInsurance>("OnlineInsurance");
            }
        }
        #endregion
    }
}
