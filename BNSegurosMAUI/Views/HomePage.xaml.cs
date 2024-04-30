using System;
using System.Collections.Generic;
using static Microsoft.Maui.ApplicationModel.Permissions;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace BNSegurosMAUI.Views
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();

            // Eliminate Back text on BackButton
            NavigationPage.SetBackButtonTitle(this, string.Empty);
            NavigationPage.SetHasBackButton(this, false);

            // TODO Xamarin.Forms.Device.RuntimePlatform is no longer supported. Use Microsoft.Maui.Devices.DeviceInfo.Platform instead. For more details see https://learn.microsoft.com/en-us/dotnet/maui/migration/forms-projects#device-changes
            //  if (Device.RuntimePlatform == Device.Android)
            //AndroidDimensionsAdaptive();
            //else
            //  iOSDimensionsAdaptive();
        }

        #region Methods
        public void AndroidDimensionsAdaptive()
        {
            var height = DeviceDisplay.MainDisplayInfo.Height;
            var width = DeviceDisplay.MainDisplayInfo.Width;
            var density = DeviceDisplay.MainDisplayInfo.Density;

            //BUTTONS SIZE
            if (height >= 1512 && height < 1920)
            {
                //Trámites
                BtnAccessFrame1.HeightRequest = 70;
                BtnAccessFrame1.WidthRequest = 70;
                //Seguros
                BtnAccessFrame2.HeightRequest = 70;
                BtnAccessFrame2.WidthRequest = 70;
                //Siniestro
                BtnAccessFrame3.HeightRequest = 70;
                BtnAccessFrame3.WidthRequest = 70;
                //Comprar
                BtnAccessFrame4.HeightRequest = 70;
                BtnAccessFrame4.WidthRequest = 70;
            }
            else if (height >= 1920 && height <= 2400)
            {
                //Trámites
                BtnAccessFrame1.HeightRequest = 70;
                BtnAccessFrame1.WidthRequest = 70;
                //Seguros
                BtnAccessFrame2.HeightRequest = 70;
                BtnAccessFrame2.WidthRequest = 70;
                //Siniestro
                BtnAccessFrame3.HeightRequest = 70;
                BtnAccessFrame3.WidthRequest = 70;
                //Comprar
                BtnAccessFrame4.HeightRequest = 70;
                BtnAccessFrame4.WidthRequest = 70;
            }
            else if (height > 2400)
            {
                //Trámites
                BtnAccessFrame1.HeightRequest = 70;
                BtnAccessFrame1.WidthRequest = 70;
                //Seguros
                BtnAccessFrame2.HeightRequest = 70;
                BtnAccessFrame2.WidthRequest = 70;
                //Siniestro
                BtnAccessFrame3.HeightRequest = 70;
                BtnAccessFrame3.WidthRequest = 70;
                //Comprar
                BtnAccessFrame4.HeightRequest = 70;
                BtnAccessFrame4.WidthRequest = 70;
            }
            else
            {
                //Trámites
                BtnAccessFrame1.HeightRequest = 70;
                BtnAccessFrame1.WidthRequest = 70;
                //Seguros
                BtnAccessFrame2.HeightRequest = 70;
                BtnAccessFrame2.WidthRequest = 70;
                //Siniestro
                BtnAccessFrame3.HeightRequest = 70;
                BtnAccessFrame3.WidthRequest = 70;
                //Comprar
                BtnAccessFrame4.HeightRequest = 70;
                BtnAccessFrame4.WidthRequest = 70;
            }

            //FOOTER
           /* if (width <= 720)
            {
                FooterContainer.HeightRequest = 60;
                FooterContainer.WidthRequest = 720;
            }
            else if (width > 720 && width <= 1080)
            {
                FooterContainer.HeightRequest = 60;
                FooterContainer.WidthRequest = 1080;
            }
            else if (width > 1080)
            {
                FooterContainer.HeightRequest = 60;
                FooterContainer.WidthRequest = 1440;
            }
            else
            {
                FooterContainer.HeightRequest = 60;
                FooterContainer.WidthRequest = 1080;
            }
           */
            //Samsung Galaxy J7 Prime
            if (height == 1920 && width == 1080 && density == 3) 
            {
                //Carrusel
                CarruselContainer.HeightRequest = 190;
                //Más información
                BtnMoreInfo.HeightRequest = 45;
                BtnMoreInfo.WidthRequest = 210;
                //Trámites
                BtnAccessFrame1.HeightRequest = 63;
                BtnAccessFrame1.WidthRequest = 63;
                //Seguros
                BtnAccessFrame2.HeightRequest = 63;
                BtnAccessFrame2.WidthRequest = 63;
                //Siniestro
                BtnAccessFrame3.HeightRequest = 63;
                BtnAccessFrame3.WidthRequest = 63;
                //Comprar
                BtnAccessFrame4.HeightRequest = 63;
                BtnAccessFrame4.WidthRequest = 63;
                //Footer
              //  FooterContainer.HeightRequest = 53;
               // FooterContainer.WidthRequest = 100;
            }
        }

        public void iOSDimensionsAdaptive()
        {
            var height = DeviceDisplay.MainDisplayInfo.Height;
            var width = DeviceDisplay.MainDisplayInfo.Width;
            var density = DeviceDisplay.MainDisplayInfo.Density;

            //BUTTONS SIZE
            if (height >= 1792 && height < 2436) //iPhone XR, iPhone 11 
            {
                //Carrusel
                CarruselContainer.HeightRequest = 220;
                //Trámites
                BtnAccessFrame1.HeightRequest = 75;
                BtnAccessFrame1.WidthRequest = 75;
                //Seguros
                BtnAccessFrame2.HeightRequest = 75;
                BtnAccessFrame2.WidthRequest = 75;
                //Siniestro
                BtnAccessFrame3.HeightRequest = 75;
                BtnAccessFrame3.WidthRequest = 75;
                //Comprar
                BtnAccessFrame4.HeightRequest = 75;
                BtnAccessFrame4.WidthRequest = 75;
            }
            else if (height >= 2436 && height < 2532) //iPhone 11 Pro, iPhone 12 Mini, iPhone 13 Mini
            {
                //Trámites
                BtnAccessFrame1.HeightRequest = 70;
                BtnAccessFrame1.WidthRequest = 70;
                //Seguros
                BtnAccessFrame2.HeightRequest = 70;
                BtnAccessFrame2.WidthRequest = 70;
                //Siniestro
                BtnAccessFrame3.HeightRequest = 70;
                BtnAccessFrame3.WidthRequest = 70;
                //Comprar
                BtnAccessFrame4.HeightRequest = 70;
                BtnAccessFrame4.WidthRequest = 70;
            }
            else if (height >= 2532 && height < 2688) //iPhone 14, iPhone 14 Pro
            {
                //Trámites
                BtnAccessFrame1.HeightRequest = 70;
                BtnAccessFrame1.WidthRequest = 70;
                //Seguros
                BtnAccessFrame2.HeightRequest = 70;
                BtnAccessFrame2.WidthRequest = 70;
                //Siniestro
                BtnAccessFrame3.HeightRequest = 70;
                BtnAccessFrame3.WidthRequest = 70;
                //Comprar
                BtnAccessFrame4.HeightRequest = 70;
                BtnAccessFrame4.WidthRequest = 70;
            }
            else if (height >= 2688) //iPhone XS Max, iPhone 14 Plus, iPhone 14 Pro Max
            {
                //Trámites
                BtnAccessFrame1.HeightRequest = 75;
                BtnAccessFrame1.WidthRequest = 75;
                //Seguros
                BtnAccessFrame2.HeightRequest = 75;
                BtnAccessFrame2.WidthRequest = 75;
                //Siniestro
                BtnAccessFrame3.HeightRequest = 75;
                BtnAccessFrame3.WidthRequest = 75;
                //Comprar
                BtnAccessFrame4.HeightRequest = 75;
                BtnAccessFrame4.WidthRequest = 75;
            }
            else
            {
                //Trámites
                BtnAccessFrame1.HeightRequest = 75;
                BtnAccessFrame1.WidthRequest = 75;
                //Seguros
                BtnAccessFrame2.HeightRequest = 75;
                BtnAccessFrame2.WidthRequest = 75;
                //Siniestro
                BtnAccessFrame3.HeightRequest = 75;
                BtnAccessFrame3.WidthRequest = 75;
                //Comprar
                BtnAccessFrame4.HeightRequest = 75;
                BtnAccessFrame4.WidthRequest = 75;
            }

            //FOOTER
           /* if (width <= 828) //iPhone XR, iPhone 11
            {
                FooterContainer.HeightRequest = 70;
                FooterContainer.WidthRequest = 828;
                FooterContainer.Padding = new Thickness(0, 0, 0, 18);
            }
            else if (width > 828 && width <= 1125) //iPhone 11 Pro, iPhone 12 Mini, iPhone 13 Mini
            {
                FooterContainer.HeightRequest = 70;
                FooterContainer.WidthRequest = 1125;
                FooterContainer.Padding = new Thickness(0, 0, 0, 18);
            }
            else if (width > 1125 && width <= 1179) //iPhone 14, iPhone 14 Pro
            {
                FooterContainer.HeightRequest = 70;
                FooterContainer.WidthRequest = 1179;
                FooterContainer.Padding = new Thickness(0, 0, 0, 18);
            }
            else if (width > 1179) //iPhone XS Max, iPhone 14 Plus, iPhone 14 Pro Max
            {
                FooterContainer.HeightRequest = 70;
                FooterContainer.WidthRequest = 1290;
                FooterContainer.Padding = new Thickness(0, 0, 0, 18);
            }
            else
            {
                FooterContainer.HeightRequest = 70;
                FooterContainer.WidthRequest = 1179;
                FooterContainer.Padding = new Thickness(0, 0, 0, 18);
            }
           */
            //iPhone 8
            if (height == 1334 && width == 750 && density == 2)
            {
                //Carrusel
                CarruselContainer.HeightRequest = 190;
                //Más información
                BtnMoreInfo.HeightRequest = 45;
                BtnMoreInfo.WidthRequest = 205;
                //Trámites
                BtnAccessFrame1.HeightRequest = 63;
                BtnAccessFrame1.WidthRequest = 63;
                //Seguros
                BtnAccessFrame2.HeightRequest = 63;
                BtnAccessFrame2.WidthRequest = 63;
                //Siniestro
                BtnAccessFrame3.HeightRequest = 63;
                BtnAccessFrame3.WidthRequest = 63;
                //Comprar
                BtnAccessFrame4.HeightRequest = 63;
                BtnAccessFrame4.WidthRequest = 63;
                //Footer
               /* FooterContainer.HeightRequest = 53;
                FooterContainer.WidthRequest = 100;
                FooterContainer.Padding = new Thickness(0, 5, 0, 5);*/
            }
        }
        #endregion
    }
}
