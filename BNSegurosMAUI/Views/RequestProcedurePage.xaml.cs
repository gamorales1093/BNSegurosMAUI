using System;
using System.Collections.Generic;
using Microsoft.Maui.Controls;
using Microsoft.Maui;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;

namespace BNSegurosMAUI.Views
{
    public partial class RequestProcedurePage : ContentPage
    {
        public RequestProcedurePage()
        {
            InitializeComponent();

            // Eliminate Back text on BackButton
            Microsoft.Maui.Controls.NavigationPage.SetBackButtonTitle(this, string.Empty);
        }

        void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ItemList.SelectedItem = null;
        }
    }
}
