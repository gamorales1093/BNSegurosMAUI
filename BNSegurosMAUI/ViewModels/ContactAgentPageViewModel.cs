using BNSegurosMAUI.Helpers;
using BNSegurosMAUI.Models;
using BNSegurosMAUI.Resources.Text;
using BNSegurosMAUI.WebServices;
using Prism;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using Contact = BNSegurosMAUI.Models.Contact;
//using Device = Xamarin.Forms.Device;
using Microsoft.Maui.Devices.Sensors;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.ApplicationModel.Communication;
using Microsoft.Maui.Controls;
using Microsoft.Maui;
using CommunityToolkit.Maui.Core;

namespace BNSegurosMAUI.ViewModels
{
    public class ContactAgentPageViewModel : BasePageViewModel, IActiveAware, IInitializeAsync
    {
        public ContactAgentPageViewModel(INavigationService navigationService, IPageDialogService pageDialog, IDialogService dialogService, IPopupService popupServices)
            : base(navigationService, pageDialog, dialogService, popupServices)
        {
            Title = TextResources.Contact_BNInsuranceLabel;
            ComptrollerHTMLPageCommand = new DelegateCommand(ComptrollerHTMLPage);
            MailRedirectCommand = new DelegateCommand(MailCommand);
            ServiceClientMailRedirectCommand = new DelegateCommand(ServiceClientMailCommand);
            CallRedirectCommand = new DelegateCommand(CallCommand);
            CompensationCallRedirectCommand = new DelegateCommand(CallCompezationCommand);
            SMSRedirectCommand = new DelegateCommand(SMSCommand);
            LocationRedirectCommand = new DelegateCommand(async () => await LocationCommand());
            FacebookRedirectCommand = new DelegateCommand(FacebookCommand);
            TwitterRedirectCommand = new DelegateCommand(TwitterCommand);
            InstagramRedirectCommand = new DelegateCommand(InstagramCommand);
            TiktokRedirectCommand = new DelegateCommand(TiktokCommand);
            CloseCommand = new DelegateCommand(ClosePage);
        }

        public DelegateCommand MailRedirectCommand { get; }
        public DelegateCommand ServiceClientMailRedirectCommand { get; }
        public DelegateCommand CallRedirectCommand { get; }
        public DelegateCommand CompensationCallRedirectCommand { get; }
        public DelegateCommand SMSRedirectCommand { get; }
        public DelegateCommand LocationRedirectCommand { get; }
        public DelegateCommand FacebookRedirectCommand { get; }
        public DelegateCommand TwitterRedirectCommand { get; }
        public DelegateCommand InstagramRedirectCommand { get; }
        public DelegateCommand TiktokRedirectCommand { get; }
        public DelegateCommand ComptrollerHTMLPageCommand { get; }
        public DelegateCommand CloseCommand { get; private set; }

        public event EventHandler IsActiveChanged;
        const string GOOGLEMAPS = "Google Maps";
        const string WAZEMAPS = "Waze";
        const string OTHERMAPS = "Otro";

        private bool _isActive;
        public bool IsActive
        {
            get { return _isActive; }
            set { SetProperty(ref _isActive, value, RaiseIsActiveChanged); }
        }

        private Contact _contact;
        public Contact Contact
        {
            get { return _contact; }
            set
            {
                SetProperty(ref _contact, value, RaiseIsActiveChanged);

                if (_contact != null)
                {
                    ShowFB = !string.IsNullOrEmpty(_contact.Facebook);
                    ShowTwitter = !string.IsNullOrEmpty(_contact.Twitter);
                    ShowInstagram = !string.IsNullOrEmpty(_contact.Instagram);
                    ShowTiktok = !string.IsNullOrEmpty(_contact.Tiktok);
                }
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

        private bool _showFB;
        public bool ShowFB
        {
            get { return _showFB; }
            set { SetProperty(ref _showFB, value); }
        }

        private bool _showTwitter;
        public bool ShowTwitter
        {
            get { return _showTwitter; }
            set { SetProperty(ref _showTwitter, value); }
        }

        private bool _showInstagram;
        public bool ShowInstagram
        {
            get { return _showInstagram; }
            set { SetProperty(ref _showInstagram, value); }
        }

        private bool _showTiktok;
        public bool ShowTiktok
        {
            get { return _showTiktok; }
            set { SetProperty(ref _showTiktok, value); }
        }

        protected virtual void RaiseIsActiveChanged()
        {
            IsActiveChanged?.Invoke(this, EventArgs.Empty);
        }

        private async void CallCommand()
        {
            try
            {
                PhoneDialer.Open(Contact.PhoneNumber);
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

        private async void CallCompezationCommand()
        {
            try
            {
                PhoneDialer.Open(Contact.Compensation);
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

        private async void MailCommand()
        {
            try
            {
                string UrlAux = "mailto:" + Contact.Email;

                if (await Launcher.CanOpenAsync(UrlAux))
                {
                    await Launcher.OpenAsync(UrlAux);
                }
                else
                {
                    await DisplayAlert(TextResources.Contact_MailInvalidErrorTitle, TextResources.Contact_MailErrorMessage, TextResources.Alert_AcceptButtonText, AlertIconType.Error);
                }
            }
            catch (Exception)
            {
                await DisplayAlert(TextResources.Contact_MailInvalidErrorTitle, TextResources.Contact_MailErrorMessage, TextResources.Alert_AcceptButtonText, AlertIconType.Error);
            }
        }

        private async void ServiceClientMailCommand()
        {
            try
            {
                string UrlAux = "mailto:" + Contact.CustomerService;

                if (await Launcher.CanOpenAsync(UrlAux))
                {
                    await Launcher.OpenAsync(UrlAux);
                }
                else
                {
                    await DisplayAlert(TextResources.Contact_MailInvalidErrorTitle, TextResources.Contact_MailErrorMessage, TextResources.Alert_AcceptButtonText, AlertIconType.Error);
                }
            }
            catch (Exception)
            {
                await DisplayAlert(TextResources.Contact_MailInvalidErrorTitle, TextResources.Contact_MailErrorMessage, TextResources.Alert_AcceptButtonText, AlertIconType.Error);
            }
        }

        private async void SMSCommand()
        {
            try
            {
                if (!string.IsNullOrEmpty(Contact.Sms))
                {
                    await SendSMS(string.Empty, Contact.Sms);
                }
            }
            catch (FeatureNotSupportedException)
            {
                await DisplayAlert(TextResources.Contact_SmsSupportPhoneErrorTitle, TextResources.Contact_SmsSupportPhoneErrorMessage, TextResources.Alert_AcceptButtonText, AlertIconType.Error);
            }
            catch (Exception)
            {
                await DisplayAlert(TextResources.Contact_SmsSupportPhoneErrorTitle, TextResources.Contact_SmsErrorMessage, TextResources.Alert_AcceptButtonText, AlertIconType.Error);
            }
        }

        public async Task SendSMS(string messageText, string recipient)
        {
            try
            {
                var message = new SmsMessage(messageText, recipient);
                await Sms.ComposeAsync(message);
            }
            catch (FeatureNotSupportedException)
            {
                await DisplayAlert(TextResources.Contact_SmsSupportPhoneErrorTitle, TextResources.Contact_SmsSupportPhoneErrorMessage, TextResources.Alert_AcceptButtonText, AlertIconType.Error);
            }
            catch (Exception)
            {
                await DisplayAlert(TextResources.Contact_SmsSupportPhoneErrorTitle, TextResources.Contact_SmsErrorMessage, TextResources.Alert_AcceptButtonText, AlertIconType.Error);
            }
        }

        private async Task LocationCommand()
        {
            try
            {
                var mylocation = await Geolocation.GetLastKnownLocationAsync();
                var bnlatitude = double.Parse(Location.Latitude, CultureInfo.InvariantCulture);
                var bnlongitude = double.Parse(Location.Longitude, CultureInfo.InvariantCulture);
                var language = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
                var gmapsbnlocation = string.Empty;
                if (language == "es")
                    gmapsbnlocation = "saddr=Tu+ubicación&daddr=" + Location.Latitude + "," + Location.Longitude + "&directionsmode=driving";
                else
                    gmapsbnlocation = "saddr=My+Location&daddr=" + Location.Latitude + "," + Location.Longitude + "&directionsmode=driving";

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
                        //maui
                        await Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
                        //Device.OpenUri(uri);
                    }
                }
                else if (choice == WAZEMAPS)
                {
                    await Launcher.OpenAsync("waze://?ll=" + Location.Latitude + "," + Location.Longitude + "&navigate=yes");
                }
            }
            catch (Exception)
            {
                await DisplayAlert(TextResources.Contact_SocialMediaErrorTitle, TextResources.Contact_SocialMediaErrorMessage, TextResources.Alert_AcceptButtonText, AlertIconType.Error);
            }
        }

        public async void FacebookCommand()
        {
            try
            {
                if (await Launcher.CanOpenAsync("fb://profile/" + Contact.Facebook))
                {
                    // TODO Xamarin.Forms.Device.RuntimePlatform is no longer supported. Use Microsoft.Maui.Devices.DeviceInfo.Platform instead. For more details see https://learn.microsoft.com/en-us/dotnet/maui/migration/forms-projects#device-changes
                    if (Device.RuntimePlatform == Device.Android)
                        await Browser.Default.OpenAsync(new Uri("fb://page/" + Contact.Facebook), BrowserLaunchMode.SystemPreferred);
                    //Device.OpenUri(new Uri("fb://page/" + Contact.Facebook));
                    else
                        await Launcher.OpenAsync("fb://profile/" + Contact.Facebook);
                }
                else
                {
                    await DisplayAlert(TextResources.Contact_SocialMediaErrorTitle, TextResources.Contact_SocialMediaErrorMessage, TextResources.Alert_AcceptButtonText, AlertIconType.Error);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert(TextResources.Contact_SocialMediaErrorTitle, TextResources.Contact_SocialMediaErrorMessage, TextResources.Alert_AcceptButtonText, AlertIconType.Error);
            }
        }

        public async void TwitterCommand()
        {
            try
            {
                Uri uri = new Uri("twitter://user?screen_name=" + Contact.Twitter);

                await Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
            }
            catch (Exception ex)
            {
                await DisplayAlert(TextResources.Contact_SocialMediaErrorTitle, TextResources.Contact_SocialMediaErrorMessage, TextResources.Alert_AcceptButtonText, AlertIconType.Error);
            }
        }

        public async void InstagramCommand()
        {
            try
            {
              Uri uri = new Uri("instagram://user?username=" + Contact.Instagram);

                await Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
            }
            catch (Exception ex)
            {
                await DisplayAlert(TextResources.Contact_SocialMediaErrorTitle, TextResources.Contact_SocialMediaErrorMessage, TextResources.Alert_AcceptButtonText, AlertIconType.Error);
            }
            }

        public async void TiktokCommand()
        {
            try
            {
                await Launcher.OpenAsync("https://www.tiktok.com/@" + Contact.Tiktok);
            }
            catch (Exception ex)
            {
                await DisplayAlert(TextResources.Contact_SocialMediaErrorTitle, TextResources.Contact_SocialMediaErrorMessage, TextResources.Alert_AcceptButtonText, AlertIconType.Error);
            }
        }

        private async Task GetContactInformation()
        {
            if (await IsConnected(true))
            {
                ShowProgressDialog(TextResources.ProgressDialogDefaultLoading);

                var service = WebServiceFactory.Create(WebServiceType.GetContacts);
                var response = await service.Execute<GetContactsResponse>();

                DismissProgressDialog();

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

        public async Task InitializeAsync(INavigationParameters parameters)
        {
            if (Contact == null)
            {
                await GetContactInformation();
            }
            if (Location == null)
            {
                await GetLocationInformation();
            }
        }

        private async void ComptrollerHTMLPage()
        {
            Debug.WriteLine("Action: ComptrollerHTMLPage");
            try
            {
                var url = Contact.ComptrollerOfService;

                var uri = new Uri(url);
                await Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
            }
            catch (Exception)
            {
                await DisplayAlert(TextResources.AlertErrorTitle, "Error", TextResources.AlertAcceptButton, AlertIconType.Error);
            }
        }

        private async void ClosePage()
        {
            if (SessionInfo.GetInstance().FromInsuranceDetail == true)
                await Navigation.GoBackAsync();
            else
                Application.Current.MainPage = new AppShell();

            Debug.WriteLine("Return to: ClosePage");
        }
    }
}
