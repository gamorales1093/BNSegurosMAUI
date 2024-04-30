using System;
using System.Diagnostics;
using BNSegurosMAUI.Models;
using BNSegurosMAUI.Resources.Text;
using BNSegurosMAUI.WebServices;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Prism.Services;

namespace BNSegurosMAUI.ViewModels
{
    public class TipPageViewModel : BasePageViewModel, IInitialize
    {
        #region Constructor
        public TipPageViewModel(INavigationService navigationService, IPageDialogService pageDialog)
            : base(navigationService, pageDialog)
        {
            Title = TextResources.Tip_TitleLabel;

            CloseCommand = new DelegateCommand(ClosePage);

            Debug.WriteLine("Constructor: TipPageViewModel");
            _= GetTipInformation();

        }
        #endregion

        #region Variables
        private Tip _tip;
        public Tip Tip
        {
            get { return _tip; }
            set { SetProperty(ref _tip, value); }
        }

        public DelegateCommand CloseCommand { get; private set; }
        #endregion

        #region Methods
        private async void ClosePage()
        {
            // await Navigation.NavigateAsync("/HomePage");
            Application.Current.MainPage = new AppShell();
            Debug.WriteLine("Return to: ClosePage");
        }

        public void Initialize(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("Tip"))
            {
                Tip = parameters.GetValue<Tip>("Tip");
            }
        }
        private async Task<Tip> GetTipInformation()
        {
            if (await IsConnected(true))
            {
                ShowProgressDialog(TextResources.ProgressDialogDefaultLoading);

                var service = WebServiceFactory.Create(WebServiceType.GetTips);
                var response = await service.Execute<GetTipsResponse>();

                DismissProgressDialog();

                if (response.IsSuccessful && response.Data?.Count > 0)
                {
                 
                    if (response.Data[0] != null)
                    {
                        var navigationParams = new NavigationParameters { { "Tip", response.Data[0] } };
                        Initialize(navigationParams);
                    }

                    return response.Data[0];
                }
                else
                {
                    await DisplayAlert(TextResources.AlertErrorTitle, response.ErrorMessage, TextResources.AlertAcceptButton, AlertIconType.Error);
                }

             
            }
            return null;
        }


        #endregion
    }
}
