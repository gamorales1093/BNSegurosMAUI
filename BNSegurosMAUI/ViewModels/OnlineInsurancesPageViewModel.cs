using System;
using BNSegurosMAUI.Helpers;
using BNSegurosMAUI.Models;
using BNSegurosMAUI.Resources.Text;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System.Diagnostics;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Linq;
using PanCardView.Extensions;
using BNSegurosMAUI.WebServices;
using System.Collections.Generic;
using Microsoft.Maui.Controls;
using Microsoft.Maui;
using CommunityToolkit.Maui.Core;

namespace BNSegurosMAUI.ViewModels
{
	public class OnlineInsurancesPageViewModel : BasePageViewModel
    {
        #region Constructor
        public OnlineInsurancesPageViewModel(INavigationService navigationService, IPageDialogService pageDialog, IDialogService dialogService, IPopupService popupServices)
            : base(navigationService, pageDialog, dialogService, popupServices)
        {
            Title = TextResources.OnlineInsurances_TitleLabel;
            CloseCommand = new DelegateCommand(ClosePage);
            BuyInsuranceCommand = new DelegateCommand(GoToBuyInsurancePage);
            ShowOnlineInsuranceDetailCommand = new DelegateCommand(ShowOnlineInsuranceDetailPage);
            CarouselItems = new ObservableCollection<OnlineInsurance>();

            LoadOnlineInsurances();

            Debug.WriteLine("Constructor: OnlineInsurancesPageViewModel");
        }
        #endregion

        #region Variables
        private ObservableCollection<OnlineInsurance> _carouselItems;
        public ObservableCollection<OnlineInsurance> CarouselItems
        {
            get => _carouselItems;
            set => SetProperty(ref _carouselItems, value);
        }
        public DelegateCommand CloseCommand { get; private set; }
        public DelegateCommand BuyInsuranceCommand { get; private set; }
        public DelegateCommand ShowOnlineInsuranceDetailCommand { get; private set; }

        private int _currentIndex;
        public int CurrentIndex
        {
            get => _currentIndex;
            set => SetProperty(ref _currentIndex, value);
        }
        #endregion

        #region Methods
        private async void LoadOnlineInsurances()
        {
            if (await IsConnected())
            {
                ShowProgressDialog(TextResources.ProgressDialogDefaultLoading);

                var service = WebServiceFactory.Create(WebServiceType.GetOnlineInsurances);
                var response = await service.Execute<GetOnlineInsurancesResponse>();

                DismissProgressDialog();

                if (response.IsSuccessful)
                {
                    SetUpCarousel(response.Data);
                }
            }
        }

        private async void SetUpCarousel(List<OnlineInsurance> results)
        {
            try
            {
                var items = new ObservableCollection<OnlineInsurance>();

                foreach (var item in results.FindAll(x => x.Active))
                {
                    items.Add(new OnlineInsurance()
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Description = item.Description,
                        Active = item.Active,
                        Url = item.Url,
                        Image = item.Image,
                        ComingSoon = item.IsComingSoon
                    });
                }

                if (items.Count == 0)
                {
                    await DisplayAlert("Aviso", "Actualmente no disponemos de seguros para compra en línea, disculpe las molestias.", TextResources.AlertAcceptButton, AlertIconType.Info);
                    await Navigation.NavigateAsync("/HomePage");
                }

                CarouselItems = items;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        private async void ClosePage()
        {
            await Navigation.GoBackAsync();
            Debug.WriteLine("Return to: ClosePage");
        }

        private async void GoToBuyInsurancePage()
        {
            var url = CarouselItems[CurrentIndex].Url;

            if (url != "Próximamente")
            {
                // Pass info in to OnlineInsuranceDetailPage
                var onlineInsurance = new OnlineInsurance()
                {
                    Id = CarouselItems[CurrentIndex].Id,
                    Name = CarouselItems[CurrentIndex].Name,
                    Description = CarouselItems[CurrentIndex].Description,
                    Active = CarouselItems[CurrentIndex].Active,
                    Url = CarouselItems[CurrentIndex].Url,
                    Image = CarouselItems[CurrentIndex].Image,
                    ComingSoon = CarouselItems[CurrentIndex].ComingSoon
                };

                var navigationParams = new NavigationParameters {
                    { "OnlineInsurance", onlineInsurance }
                };

                await Navigation.NavigateAsync("BuyInsurancePage", navigationParams);
                Debug.WriteLine("Go to: BuyInsurancePage");
            }
            else
            {
                await DisplayAlert("Próximamente", "La compra de este seguro no esta disponible en este momento", TextResources.AlertAcceptButton, AlertIconType.Info);
            }
        }

        private async void ShowOnlineInsuranceDetailPage()
        {
            // Pass info in to OnlineInsuranceDetailPage
            var onlineInsurance = new OnlineInsurance()
            {
                Id = CarouselItems[CurrentIndex].Id,
                Name = CarouselItems[CurrentIndex].Name,
                Description = CarouselItems[CurrentIndex].Description,
                Active = CarouselItems[CurrentIndex].Active,
                Url = CarouselItems[CurrentIndex].Url,
                Image = CarouselItems[CurrentIndex].Image,
                ComingSoon = CarouselItems[CurrentIndex].ComingSoon
            };

            var navigationParams = new NavigationParameters {
                { "OnlineInsurance", onlineInsurance }
            };

            await Navigation.NavigateAsync("OnlineInsuranceDetailPage", navigationParams);
            Debug.WriteLine("Go to: OnlineInsuranceDetailPage");
        }
        #endregion
    }
}