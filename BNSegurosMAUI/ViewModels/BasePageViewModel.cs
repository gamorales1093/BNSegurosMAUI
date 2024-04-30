using System;
using System.Threading;
using System.Threading.Tasks;
using BNSegurosMAUI.Utils;
using BNSegurosMAUI.Resources.Text;
using BNSegurosMAUI.ViewModels.Dialogs;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using Microsoft.Maui.Networking;
using Controls.UserDialogs.Maui;
using CommunityToolkit.Maui.Core;
using BNSegurosMAUI.Views.Dialogs;

namespace BNSegurosMAUI.ViewModels
{
    public abstract class BasePageViewModel : BindableBase
    {
        private  IPopupService PopupService { get; set; }
        protected enum AlertIconType { None=0,Mail,Error,Info }
        #region Variables
        private IDialogService AlertDialog { get; }
        protected INavigationService Navigation { get; set; }
        protected IPageDialogService PageDialog { get; set; }
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        #endregion

        #region Constructors
        protected BasePageViewModel() {

        }

        protected BasePageViewModel(INavigationService navigationService) : this(navigationService, null) { }

        protected BasePageViewModel(INavigationService navigationService, IPageDialogService pageDialog)
        {
            Navigation = navigationService;
            PageDialog = pageDialog;
  
        }

        protected BasePageViewModel(IDialogService alertDialog):this(null,null,alertDialog,null) { }

        protected BasePageViewModel(INavigationService navigationService, IPageDialogService pageDialog,IDialogService alertDialog, IPopupService popupService)
        {
            Navigation = navigationService;
            PageDialog = pageDialog;
            AlertDialog = alertDialog;
            PopupService = popupService;
        }
        #endregion

        protected async Task DisplayAlert(string title, string message, string acceptButton, AlertIconType icon = AlertIconType.None)
        {
            var parameters = new DialogParameters()
            {
                { DialogConstants.DialogTitleKey, title },
                { DialogConstants.DialogMessageKey, message },
                { DialogConstants.DialogAcceptButtonKey, acceptButton },
                { DialogConstants.DialogIconKey, GetAlertIconResource(icon)}
            };

          //  await AlertDialog.ShowCustomDialogAsync("MessageDialog", parameters);

            try
            {
                this.PopupService.ShowPopup<AlertPopupViewModel>(onPresenting: viewModel => viewModel.SetParameters(parameters));
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.Alert(ex.Message, title, acceptButton, GetAlertIconResource(icon));
            }
            // UserDialogs.Instance.Alert(message, title, acceptButton, GetAlertIconResource(icon));


        }

        protected void ShowProgressDialog(string message)
        {
            ProgressDialog.Show(message);
        }

        protected void DismissProgressDialog()
        {
            ProgressDialog.Dismiss();
        }

        private string GetAlertIconResource(AlertIconType iconType)
        {
            string resource;
            switch (iconType)
            {
                case AlertIconType.Mail:
                    resource = "icmailmedium";
                    break;
                case AlertIconType.Error:
                    resource = "icfail";
                    break;
                case AlertIconType.Info:
                    resource = "icbnalert";
                    break;
                case AlertIconType.None:
                default:
                    resource = null;
                    break;
            }
            return resource;
        }

        void CloseDialogCallback(IDialogResult dialogResult)
        {

        }

        protected async Task<bool> IsConnected(bool showNoConnectivityAlert = false)
        {
            bool isConnected = Connectivity.NetworkAccess == NetworkAccess.Internet;
            if (!isConnected && showNoConnectivityAlert)
            {
                await DisplayAlert(TextResources.AlertNoConnectivityTitle, TextResources.AlertNoConnectivity, TextResources.AlertAcceptButton, AlertIconType.Error);
            }
            return isConnected;
        }
    }
}
