using System;
using System;
using System.Diagnostics;
using BNSegurosMAUI.Models;
using BNSegurosMAUI.Resources.Text;
using BNSegurosMAUI.WebServices;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Prism.Services;


namespace BNSegurosMAUI.ViewModels
{
	public class TestStylesViewModel : BasePageViewModel, IInitialize
    {
        public TestStylesViewModel(INavigationService navigationService, IPageDialogService pageDialog)
            : base(navigationService, pageDialog)
        {
             Navigation.NavigateAsync("ContactPage");


        }

        public void Initialize(INavigationParameters parameters)
        {
            throw new NotImplementedException();
        }
    }
}

