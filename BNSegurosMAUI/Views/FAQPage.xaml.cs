using System;
using System.Collections.Generic;
using BNSegurosMAUI.Models;
using BNSegurosMAUI.ViewModels;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace BNSegurosMAUI.Views
{
    public partial class FAQPage : ContentPage
    {
        public FAQPage()
        {
            InitializeComponent();

            // Eliminate Back text on BackButton
           NavigationPage.SetBackButtonTitle(this, string.Empty);
           NavigationPage.SetHasBackButton(this, false);
        }

        void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //ItemList.SelectedItem = null;
        }

        protected override bool OnBackButtonPressed()
        {
       //     var pageViewModel = (FAQPageViewModel)BindingContext;
         //   pageViewModel.CloseCommand.Execute();
           // Application.Current.MainPage = new AppShell();
            return false;
        }

        protected async override void OnAppearing()
        {
            var pageViewModel = (FAQPageViewModel)BindingContext;
            pageViewModel.GetQuestionData();
            base.OnAppearing();
        }

    }
}
