using System;
using System.Collections.Generic;
using BNSegurosMAUI.ViewModels;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace BNSegurosMAUI.Views
{
    public partial class TipPage : ContentPage
    {
        public TipPage()
        {
            InitializeComponent();

            // Eliminate Back text on BackButton
            NavigationPage.SetBackButtonTitle(this, string.Empty);
            NavigationPage.SetHasBackButton(this, false);
        }

        protected override bool OnBackButtonPressed()
        {
            var pageViewModel = (TipPageViewModel)BindingContext;
            pageViewModel.CloseCommand.Execute();
            return true;
        }
    }
}
