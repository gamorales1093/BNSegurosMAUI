using System;
using System.Collections.Generic;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace BNSegurosMAUI.Views
{
    public partial class InsurancesPage : ContentPage
    {
        public InsurancesPage()
        {
            InitializeComponent();

            // Eliminate Back text on BackButton
            NavigationPage.SetBackButtonTitle(this, string.Empty);
        }

        void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //ItemList.SelectedItem = null;
        }
    }
}
