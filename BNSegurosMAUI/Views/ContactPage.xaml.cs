using System;
using System.Collections.Generic;
using BNSegurosMAUI.Views.Controls;
using BNSegurosMAUI.ViewModels;
using System.Threading.Tasks;
using BNSegurosMAUI.Helpers;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace BNSegurosMAUI.Views
{
    public partial class ContactPage : TabbedPage
    {
        public ContactPage()
        {
            InitializeComponent();

          if (SessionInfo.GetInstance().FromInsuranceDetail == true)
                CurrentPage = Children[2];

            // Eliminate Back text on BackButton
            NavigationPage.SetBackButtonTitle(this, string.Empty);
            NavigationPage.SetHasBackButton(this, false);
        }

        protected override bool OnBackButtonPressed()
        {
         var contactPageViewModel = (ContactPageViewModel)BindingContext;
            contactPageViewModel.CloseCommand.Execute();
            // Task.Delay(1000).Wait();

            return base.OnBackButtonPressed();
        }


    }
}
