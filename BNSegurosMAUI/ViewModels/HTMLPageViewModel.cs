using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using BNSegurosMAUI.Helpers;
using BNSegurosMAUI.WebServices.Client;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace BNSegurosMAUI.ViewModels
{
    public class HTMLPageViewModel : BasePageViewModel, IInitialize
    {
        #region Constructor
        public HTMLPageViewModel(INavigationService navigationService, IPageDialogService pageDialog)
            : base(navigationService, pageDialog)
        {
            Title = "Indemnizaciones";

            CloseCommand = new DelegateCommand(ClosePage);

            Debug.WriteLine("Constructor: HTMLPageViewModel");
        }
        #endregion

        #region Variables
        public DelegateCommand CloseCommand { get; private set; }
        private WebViewSource _webSiteSource;
        public WebViewSource WebSiteSource
        {
            get { return _webSiteSource; }
            set { SetProperty(ref _webSiteSource, value); }
        }

        public interface IBaseUrl { string Get(); }
        #endregion

        #region Methods
        private async void ClosePage()
        {
            if (SessionInfo.GetInstance().FromFAQPage == true)
                await Navigation.GoBackAsync();
            else
                Application.Current.MainPage = new AppShell();

            Debug.WriteLine("Return to: ClosePage");
        }

        private void ValidateTitle(string url)
        {
            if (url == ApiConstants.BaseOpaUrl + "Duplicados")
                Title = "Duplicados";
            else if (url == ApiConstants.BaseOpaUrl + "Emisiones")
                Title = "Emisiones";
            else if (url == ApiConstants.BaseOpaUrl + "Variaciones")
                Title = "Variaciones";
            else if (url == ApiConstants.BaseOpaUrl + "Nocotizante")
                Title = "Nocotizante";
            else if (url == ApiConstants.BaseOpaUrl + "Emisiones")
                Title = "Emisiones";
            else if (url == ApiConstants.BaseOpaUrl + "Cobros")
                Title = "Cobros";
            else if (url == ApiConstants.BaseOpaUrl + "Indemnizaciones")
                Title = "Indemnizaciones";
            else
                Title = "Solicitud";
        }

        public void Initialize(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("HTMLPage") && parameters.ContainsKey("url"))
            {
                if (parameters.GetValue<String>("HTMLPage") == "ComptrollerPage")
                {
                    WebSiteSource = parameters.GetValue<String>("url");
                }
                else if (parameters.GetValue<String>("HTMLPage") == "Claims")
                {
                    WebSiteSource = ApiConstants.BaseOpaUrl + "Indemnizaciones";
                }
            }
            else
            {
                WebSiteSource = parameters.GetValue<String>("OpaUrl");
                ValidateTitle(parameters.GetValue<String>("OpaUrl"));
            }
        }
        #endregion
    }
}
