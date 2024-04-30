using System;
using System.Collections.Generic;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace BNSegurosMAUI.Views
{	
	public partial class OnlineInsurancesPage : ContentPage
	{	
		public OnlineInsurancesPage ()
		{
			InitializeComponent ();

            // Eliminate Back text on BackButton
            NavigationPage.SetBackButtonTitle(this, string.Empty);
        }
	}
}

