using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using BNSegurosMAUI.Helpers;
using BNSegurosMAUI.Models;
using BNSegurosMAUI.Resources.Text;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Microsoft.Maui.ApplicationModel;
using CommunityToolkit.Maui.Core;

namespace BNSegurosMAUI.ViewModels
{
    public class SinisterStepsPageViewModel : BasePageViewModel, IInitialize
    {
        #region Constructor
        public SinisterStepsPageViewModel(INavigationService navigationService, IPageDialogService pageDialog, IDialogService dialogService ,IPopupService popupServices)
            : base(navigationService, pageDialog, dialogService, popupServices)
        {
            Title = "Pasos";

            CloseCommand = new DelegateCommand(ClosePage);
            DownloadPdfCommand = new DelegateCommand<string>((args) => DownloadPdf(args));
            UploadPdfCommand = new DelegateCommand<string>((args) => UploadPdf(args));

            Debug.WriteLine("Constructor: SinisterStepsPageViewModel");
        }
        #endregion

        #region Variables
        public DelegateCommand CloseCommand { get; private set; }
        public DelegateCommand<string> DownloadPdfCommand { get; private set; }
        public DelegateCommand<string> UploadPdfCommand { get; private set; }

        private ObservableCollection<Step> _displayedItems;
        public ObservableCollection<Step> DisplayedItems
        {
            get => _displayedItems;
            set => SetProperty(ref _displayedItems, value);
        }
        #endregion

        #region Methods
        private async void ClosePage()
        {
            await Navigation.GoBackAsync();
            Debug.WriteLine("Return to: ClosePage");
        }

        private void GetSinisterStepsList(Sinister sinister)
        {
            var results = new List<Step>(sinister.Steps);
            var textStep = new Step();
            textStep.Description = "footer_text";
            results.Add(textStep);
            displayList(results);
        }

        private void displayList(List<Step> results)
        {
            DisplayedItems = new ObservableCollection<Step>(results);
        }

        private async void DownloadPdf(string url)
        {
            try
            {
                if (await Launcher.CanOpenAsync(url))
                    await Launcher.OpenAsync(url);
                else
                    await DisplayAlert(TextResources.AlertErrorTitle, TextResources.AlertDefaultError, TextResources.Alert_AcceptButtonText, AlertIconType.Error);
            }
            catch (Exception ex)
            {
                await DisplayAlert(TextResources.AlertErrorTitle, TextResources.AlertDefaultError, TextResources.Alert_AcceptButtonText, AlertIconType.Error);
                Debug.WriteLine("Exception DownloadPdf: " + ex.Message);
            }
        }

        private async void UploadPdf(string url)
        {
            SessionInfo.GetInstance().FromFAQPage = true;

            var navigationParams = new NavigationParameters {
                { "OpaUrl", url }
            };
            await Navigation.NavigateAsync("HTMLPage", navigationParams);
            Debug.WriteLine("Go to: HTMLPage");
        }

        public void Initialize(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("Steps"))
            {
                var steps = parameters.GetValue<Sinister>("Steps");
                Title = steps.Description;
                GetSinisterStepsList(steps);
            }
        }
        #endregion
    }
}