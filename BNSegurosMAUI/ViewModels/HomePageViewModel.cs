using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using BNSegurosMAUI.Helpers;
using BNSegurosMAUI.Models;
using BNSegurosMAUI.Resources.Text;
using BNSegurosMAUI.WebServices;
using PanCardView.Extensions;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Devices.Sensors;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.ApplicationModel.Communication;
using Microsoft.Maui.Controls;
using Microsoft.Maui;
using BNSegurosMAUI.Views;
using BNSegurosMAUI.ViewModels.Dialogs;
using CommunityToolkit.Maui.Core;
using BNSegurosMAUI.Views.Dialogs;

namespace BNSegurosMAUI.ViewModels
{
    public class HomePageViewModel : BasePageViewModel
    {
        IDialogService _dialogservice1 { get; set; }
        IPopupService popUpService { get; set; }
        #region Constructor
        public HomePageViewModel()
        {
            // Parameterless Constructor
        }

        public HomePageViewModel(INavigationService navigationService, IPageDialogService pageDialog, IDialogService dialogService, IPopupService popupServices)
            : base(navigationService, pageDialog, dialogService, popupServices)
        {
            popUpService = popupServices;
            CarouselItems = new ObservableCollection<CarouselItem>();
            HasAdvertisements = false;
            _dialogservice1 = dialogService;
            SessionInfo.GetInstance().FromInsuranceDetail = false;
            SessionInfo.GetInstance().FromFAQPage = false;

            // GO TO
            DisplayFlayoutCommand = new DelegateCommand(ShowFlyoutMenu);
            ShowNoticeCommand = new DelegateCommand(ShowNoticePage);
            ShowInsurancesCommand = new DelegateCommand(ShowInsurancesPage);
            ShowContactCommand = new DelegateCommand(ShowContactPage);
            ShowFAQCommand = new DelegateCommand(ShowFAQPage);
            ShowTipCommand = new DelegateCommand(ShowTipPage);
            ShowOnlineInsurancesCommand = new DelegateCommand(ShowOnlineInsurancesPage);
            ShowRequestProcedureCommand = new DelegateCommand(RequestProcedurePage);
            HTMLPageCommand = new DelegateCommand(HTMLPage);
            GoToSinisterPageCommand = new DelegateCommand(GoToSinisterPage);

            // FOOTER
            ContactActionCommand = new DelegateCommand(ContactAction);
            MailActionCommand = new DelegateCommand(MailAction);
            WebPageActionCommand = new DelegateCommand(WebPageAction);
            MapLocationActionCommand = new DelegateCommand(async () => await MapLocation());

            LoadAdvertisements();
        }
        #endregion

        #region Properties
        const string GOOGLEMAPS = "Google Maps";
        const string WAZEMAPS = "Waze";

        public event EventHandler IsActiveChanged;
        private ObservableCollection<CarouselItem> _carouselItems;
        public ObservableCollection<CarouselItem> CarouselItems
        {
            get => _carouselItems;
            set => SetProperty(ref _carouselItems, value);
        }

        private int _currentIndex;
        public int CurrentIndex
        {
            get => _currentIndex;
            set => SetProperty(ref _currentIndex, value);
        }

        private bool _hasAdvertisements;
        public bool HasAdvertisements
        {
            get => _hasAdvertisements;
            set => SetProperty(ref _hasAdvertisements, value);
        }

        private Models.Contact _contact;
        public Models.Contact Contact
        {
            get { return _contact; }
            set
            {
                SetProperty(ref _contact, value, RaiseIsActiveChanged);
            
            }
        }

        private Models.Location _location;
        public Models.Location Location
        {
            get { return _location; }
            set
            {
                SetProperty(ref _location, value, RaiseIsActiveChanged);
            }
        }

        public DelegateCommand ShowNoticeCommand { get; private set; }
        public DelegateCommand ShowInsurancesCommand { get; private set; }
        public DelegateCommand ShowContactCommand { get; private set; }
        public DelegateCommand ShowFAQCommand { get; private set; }
        public DelegateCommand ShowTipCommand { get; private set; }
        public DelegateCommand ShowOnlineInsurancesCommand { get; private set; }
        public DelegateCommand ShowRequestProcedureCommand { get; private set; }
        public DelegateCommand HTMLPageCommand { get; private set; }
        public DelegateCommand DisplayFlayoutCommand { get; private set; } 
        public DelegateCommand ContactActionCommand { get; private set; }
        public DelegateCommand MailActionCommand { get; private set; }
        public DelegateCommand WebPageActionCommand { get; private set; }
        public DelegateCommand MapLocationActionCommand { get; private set; }
        public DelegateCommand GoToSinisterPageCommand { get; private set; }


        #endregion

        #region Methods
        protected virtual void RaiseIsActiveChanged()
        {
            IsActiveChanged?.Invoke(this, EventArgs.Empty);
        }

        private void ShowFlyoutMenu()
        {
            ((FlyoutPage)Application.Current.MainPage).IsPresented = true;
        }

        private void SetUpCarousel(List<Advertisement> advertisements)
        {
            var items = new ObservableCollection<CarouselItem>();

            foreach (var ad in advertisements.FindAll(x => x.Active))
            {
                items.Add(new CarouselItem()
                {
                    Image = ad.Image,
                    Id = ad.Id,
                    Color = Colors.White,
                    Title = ad.Name,
                    Description = ad.Description
                });
            }

            if (items.Count > 0)
            {
                HasAdvertisements = true;
            }
            CarouselItems = items;
        }

        private async void ShowNoticePage()
        {
            // Pass info in to NoticePage
            SessionInfo.GetInstance().NoticeModel = new Models.Notice()
            {
                NoticeImageUrl = CarouselItems[CurrentIndex].Image,
                NoticeTitle = CarouselItems[CurrentIndex].Title,
                NoticeDescription = CarouselItems[CurrentIndex].Description
            };

            await Navigation.NavigateAsync("NoticePage");
            Debug.WriteLine("Go to: ShowNoticePage");
        }

        private async void ShowInsurancesPage()
        {

            string termsAndConditions = null;
            var insurances = await GetInsurancesInformation();

            if (insurances != null)
            {
                termsAndConditions = await GetTermsAndConditions();
            }

            if (termsAndConditions != null)
            {
                var navigationParams = new NavigationParameters {
                    { "Insurances", insurances },
                    { "TermsAndConditions", termsAndConditions }
                };
                await Navigation.NavigateAsync("InsurancesPage", navigationParams);
                Debug.WriteLine("Go to: ShowInsurancePage");
            }
        }

        private async void ShowContactPage()
        {
            await Navigation.NavigateAsync("ContactPage");
            Debug.WriteLine("Go to: ShowContactPage");
        }

        private async void ShowFAQPage()
        {
            List<FrequentlyAskedQuestion> questions = null;
            var types = await GetQuestionTypes();

            if (types != null)
            {
                questions = await GetFrequentlyAskedQuestions();
            }

            if (questions != null)
            {
                var navigationParams = new NavigationParameters {
                    { "QuestionTypes", types },
                    { "Questions", questions }
                };
                await Navigation.NavigateAsync("FAQPage", navigationParams);
                Debug.WriteLine("Go to: ShowFAQPage");
            }
        }

        private async void ShowTipPage()
        {
            var tip = await GetTipInformation();
            if (tip != null)
            {
                var navigationParams = new NavigationParameters { { "Tip", tip } };
                await Navigation.NavigateAsync("TipPage", navigationParams);
                Debug.WriteLine("Go to: ShowTipPage");
            }
        }

        private async void ShowOnlineInsurancesPage()
        {
            await Navigation.NavigateAsync("OnlineInsurancesPage");
            Debug.WriteLine("Go to: ShowOnlineInsurancesPage");
        }

        private async void RequestProcedurePage()
        {
            await Navigation.NavigateAsync("RequestProcedurePage");
            Debug.WriteLine("Go to: RequestProcedurePage");
        }

        private async void HTMLPage()
        {
            await DisplayAlert("Aviso de Siniestro", "Informe sobre el evento sufrido en carretera o el daño a su propiedad", "ACEPTAR", AlertIconType.Info);
            var navigationParams = new NavigationParameters {
                { "HTMLPage", "Claims" },
                { "url", "url" }
            };
            await Navigation.NavigateAsync("HTMLPage", navigationParams);
            Debug.WriteLine("Go to: HTMLPage");
        }

        private async void GoToSinisterPage()
        {
            await Navigation.NavigateAsync("SinisterPage");
            Debug.WriteLine("Go to: SinisterPage");
        }

        private async void ContactAction()
        {
            Debug.WriteLine("Action: ContactAction");
            try
            {
                //DEBE DE VENIR POR MEDIO DE SERIVICIO
                var phone = Contact.PhoneNumber;

                // if(PhoneDialer.IsSupported)
                PhoneDialer.Open(phone);
            }
            catch (ArgumentNullException)
            {
                
                await DisplayAlert(TextResources.Contact_CallInvalidErrorTitle, TextResources.Contact_CallInvalidErrorMessage, TextResources.Alert_AcceptButtonText, AlertIconType.Error);
            }
            catch (FeatureNotSupportedException)
            {

                await DisplayAlert(TextResources.Contact_CallInvalidErrorTitle, TextResources.Contact_CallSupportPhoneErrorMessage, TextResources.Alert_AcceptButtonText, AlertIconType.Error);
            }
            catch (Exception)
            {
                await DisplayAlert(TextResources.Contact_CallInvalidErrorTitle, TextResources.Contact_CallErrorMessage, TextResources.Alert_AcceptButtonText, AlertIconType.Error);
            }
        }

        private async void MailAction()
        {
            Debug.WriteLine("Action: MailAction");
            try
            {
                //DEBE DE VENIR POR MEDIO DE SERIVICIO
                var mail = Contact.Email;

                string UrlAux = "mailto:" + mail;

                if (await Launcher.CanOpenAsync(UrlAux))
                    await Launcher.OpenAsync(UrlAux);
                else
                    await DisplayAlert(TextResources.Contact_MailInvalidErrorTitle, TextResources.Contact_MailErrorMessage, TextResources.Alert_AcceptButtonText, AlertIconType.Error);
            }
            catch (Exception)
            {
                await DisplayAlert(TextResources.Contact_MailInvalidErrorTitle, TextResources.Contact_MailErrorMessage, TextResources.Alert_AcceptButtonText, AlertIconType.Error);
            }
        }

        private async void WebPageAction()
        {
            Debug.WriteLine("Action: WebPageAction");
            try
            {
                //DEBE DE VENIR POR MEDIO DE SERIVICIO
                var url = Contact.Website;

                var uri = new Uri(url);
                await Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
            }
            catch (Exception)
            {
                await DisplayAlert(TextResources.AlertErrorTitle, "Error", TextResources.AlertAcceptButton, AlertIconType.Error);
            }
        }

        private async Task MapLocation()
        {
            Debug.WriteLine("Action: MapLocation");
            try
            {
                //DEBE DE VENIR POR MEDIO DE SERIVICIO
                var latitude = Location.Latitude;
                var longitude = Location.Longitude;

                var mylocation = await Geolocation.GetLastKnownLocationAsync();
                var bnlatitude = double.Parse(latitude, CultureInfo.InvariantCulture);
                var bnlongitude = double.Parse(longitude, CultureInfo.InvariantCulture);
                var language = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
                var gmapsbnlocation = string.Empty;
                if (language == "es")
                    gmapsbnlocation = "saddr=Tu+ubicación&daddr=" + latitude + "," + longitude + "&directionsmode=driving";
                else
                    gmapsbnlocation = "saddr=My+Location&daddr=" + latitude + "," + longitude + "&directionsmode=driving";

                var mapApps = new[] { GOOGLEMAPS, WAZEMAPS };
                var installedMapApps = new List<string>();

                foreach (var a in mapApps)
                {
                    installedMapApps.Add(a);
                }

                var choice = await App.Current.MainPage.DisplayActionSheet("Abrir con...", "Cancelar", null, installedMapApps.ToArray());

                if (choice == GOOGLEMAPS)
                {
                    // TODO Xamarin.Forms.Device.RuntimePlatform is no longer supported. Use Microsoft.Maui.Devices.DeviceInfo.Platform instead. For more details see https://learn.microsoft.com/en-us/dotnet/maui/migration/forms-projects#device-changes
                    if (Device.RuntimePlatform == Device.iOS)
                    {
                        await Launcher.OpenAsync("comgooglemaps://?" + gmapsbnlocation);
                    }
                    else
                    {
                        var uri = new Uri("http://maps.google.com/maps?" + gmapsbnlocation);
                        //Device.OpenUri(uri);
                        await Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
                    }
                }
                else if (choice == WAZEMAPS)
                {
                    await Launcher.OpenAsync("waze://?ll=" + latitude + "," + longitude + "&navigate=yes");
                }
            }
            catch (Exception)
            {
                await DisplayAlert(TextResources.Contact_SocialMediaErrorTitle, TextResources.Contact_SocialMediaErrorMessage, TextResources.Alert_AcceptButtonText, AlertIconType.Error);
            }
        }

        private async void LoadAdvertisements()
        {
            if (await IsConnected())
            {
                ShowProgressDialog(TextResources.ProgressDialogDefaultLoading);

                var service = WebServiceFactory.Create(WebServiceType.GetAdvertisements);
                var response = await service.Execute<GetAdvertisementsResponse>();

                DismissProgressDialog();

                if (response.IsSuccessful)
                {
                    SetUpCarousel(response.Data);
                    if (Contact == null)
                    {
                        await GetContactInformation();
                    }
                    if (Location == null)
                    {
                        await GetLocationInformation();
                    }
                }
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
                    return response.Data[0];
                }
                else
                {
                    await DisplayAlert(TextResources.AlertErrorTitle, response.ErrorMessage, TextResources.AlertAcceptButton, AlertIconType.Error);
                }
            }
            return null;
        }

        private async Task<List<Insurance>> GetInsurancesInformation()
        {
            if (await IsConnected(true))
            {
                ShowProgressDialog(TextResources.ProgressDialogDefaultLoading);

                var service = WebServiceFactory.Create(WebServiceType.GetInsurances);
                var response = await service.Execute<GetInsurancesResponse>();

                DismissProgressDialog();

                if (response.IsSuccessful && response.Data?.Count > 0)
                {
                    //ORDEN BY NORMAL LIST
                    //return response.Data.OrderBy(x => x.Id).ToList();

                    //ORDEN BY WEBSERVICE LIST
                    return response.Data.ToList();
                }
                else
                {
                    await DisplayAlert(TextResources.AlertErrorTitle, response.ErrorMessage, TextResources.AlertAcceptButton, AlertIconType.Error);
                }
            }
            return null;
        }

        private async Task<string> GetTermsAndConditions()
        {
            if (await IsConnected(true))
            {
                ShowProgressDialog(TextResources.ProgressDialogDefaultLoading);

                var service = WebServiceFactory.Create(WebServiceType.GetTermsAndConditions);
                var response = await service.Execute<GetTermsAndConditionsResponse>();
                var termsAndConditions = response.GetDocumentUrl();

                DismissProgressDialog();

                if (termsAndConditions != null)
                {
                    return termsAndConditions;
                }
                else
                {
                    await DisplayAlert(TextResources.AlertErrorTitle, response.ErrorMessage, TextResources.AlertAcceptButton, AlertIconType.Error);
                }
            }
            return null;
        }

        private async Task<List<QuestionType>> GetQuestionTypes()
        {
            if (await IsConnected(true))
            {
                ShowProgressDialog(TextResources.ProgressDialogDefaultLoading);

                var service = WebServiceFactory.Create(WebServiceType.GetQuestionTypes);
                var response = await service.Execute<GetQuestionTypesResponse>();

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

        private async Task<List<FrequentlyAskedQuestion>> GetFrequentlyAskedQuestions()
        {
            if (await IsConnected(true))
            {
                ShowProgressDialog(TextResources.ProgressDialogDefaultLoading);

                var service = WebServiceFactory.Create(WebServiceType.GetFrequentlyAskedQuestions);
                var response = await service.Execute<GetFrequentlyAskedQuestionsResponse>();

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

        private async Task GetContactInformation()
        {
            if (await IsConnected(true))
            {
                // ShowProgressDialog(TextResources.ProgressDialogDefaultLoading);

                var service = WebServiceFactory.Create(WebServiceType.GetContacts);
                var response = await service.Execute<GetContactsResponse>();

                //  DismissProgressDialog();

                if (response.IsSuccessful && response.Data?.Count > 0)
                {
                    Contact = response.Data[0];
                }
                else
                {
                    await DisplayAlert(TextResources.AlertErrorTitle, response.ErrorMessage, TextResources.AlertAcceptButton, AlertIconType.Error);
                }
            }
        }

        private async Task GetLocationInformation()
        {
            if (await IsConnected())
            {
                var service = WebServiceFactory.Create(WebServiceType.GetLocation);
                var response = await service.Execute<GetLocationResponse>();

                if (response.IsSuccessful && response.Data?.Count > 0)
                {
                    Location = response.Data[0];
                }
                else
                {
                    await DisplayAlert(TextResources.AlertErrorTitle, response.ErrorMessage, TextResources.AlertAcceptButton, AlertIconType.Error);
                }
            }
        }

        public bool CanCloseDialog()
        {
            //
            return true;
        }

        public void OnDialogClosed()
        {
            //
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
           //
        }
    }
}