using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using BNSegurosMAUI.Models;
using BNSegurosMAUI.Resources.Text;
using BNSegurosMAUI.WebServices;
using BNSegurosMAUI.WebServices.Client;
using CommunityToolkit.Maui.Core;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace BNSegurosMAUI.ViewModels
{
    public class SinisterPageViewModel : BasePageViewModel, IInitialize
    {
        #region Constructor
        public SinisterPageViewModel(INavigationService navigationService, IPageDialogService pageDialog, IDialogService dialogService, IPopupService popupServices)
            : base(navigationService, pageDialog, dialogService, popupServices)
        {
            Title = "Siniestros";
            ItemSelectedCommand = new DelegateCommand<object>(OnItemSelected);
            CloseCommand = new DelegateCommand(ClosePage);

            Debug.WriteLine("Constructor: SinisterPageViewModel");
        }
        #endregion

        #region Variables
        public DelegateCommand CloseCommand { get; private set; }
        public DelegateCommand<object> ItemSelectedCommand { get; }

        private ObservableCollection<Sinister> _displayedItems;
        public ObservableCollection<Sinister> DisplayedItems
        {
            get => _displayedItems;
            set => SetProperty(ref _displayedItems, value);
        }
        #endregion

        #region Methods
        private async Task GetSinisterList()
        {
            if (await IsConnected())
            {
                ShowProgressDialog(TextResources.ProgressDialogDefaultLoading);

                var service = WebServiceFactory.Create(WebServiceType.GetSinisters);
                var response = await service.Execute<GetSinistersResponse>();

                DismissProgressDialog();

                if (response.IsSuccessful && response.Data?.Count > 0)
                {
                    var results = new List<Sinister>(response.Data);

                    foreach (var sinister in results)
                    {
                        if (sinister.Description == "Reclamo Seguro de Vida")
                            sinister.Image = "icreclaiminsulife";
                        else if (sinister.Description == "Reclamo Seguro de Desempleo")
                            sinister.Image = "icreclaiminsujob";
                        else if (sinister.Description == "Reclamo Seguro de Vehículo")
                            sinister.Image = "icreclaiminsuvehicle";
                        else if (sinister.Description == "Reclamo Seguro de Vivienda")
                            sinister.Image = "icreclaiminsuhome";
                        else
                            sinister.Image = "icopaconstancy";
                    }

                    displayList(results);
                }
                else
                {
                    await DisplayAlert(TextResources.AlertErrorTitle, response.ErrorMessage, TextResources.AlertAcceptButton, AlertIconType.Error);
                }
            }
        }

        private async void displayList(List<Sinister> results)
        {
            await Task.Delay(100);
            DisplayedItems = new ObservableCollection<Sinister>(results);
        }

        private async void OnItemSelected(object items)
        {
            if (items == null)
                return;

            var steps = (Sinister)items;
            var navigationParams = new NavigationParameters {
                { "Steps", steps },
            };

            if (steps.Steps.Count == 0)
                await DisplayAlert(TextResources.AlertDefaultTitle, TextResources.AlertSinisterStepsNoInfo, TextResources.Alert_AcceptButtonText, AlertIconType.Info);
            else
                await Navigation.NavigateAsync("SinisterStepsPage", navigationParams);

            Debug.WriteLine("Go to: SinisterStepsPage");
        }

        private async void ClosePage()
        {
            await Navigation.GoBackAsync();
            Debug.WriteLine("Return to: ClosePage");
        }

        public void Initialize(INavigationParameters parameters)
        {
            GetSinisterList();
        }
        #endregion
    }
}