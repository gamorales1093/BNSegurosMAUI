using System;
using System.Diagnostics;
using BNSegurosMAUI.Resources.Text;
using CommunityToolkit.Maui.Core;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace BNSegurosMAUI.ViewModels
{
    public class ContactPageViewModel : BasePageViewModel
    {
   /*     public FAQPageViewModel(INavigationService navigationService, IPageDialogService pageDialog, IDialogService dialogService)
    : base(navigationService, pageDialog, dialogService)*/
        public ContactPageViewModel()

        {
            CloseCommand = new DelegateCommand(ClosePage);
        }
        #region Constructor
        public ContactPageViewModel(INavigationService navigationService, IPageDialogService pageDialog)
            : base(navigationService, pageDialog)
        {
            Title = TextResources.Contact_TitleLabel;
            CloseCommand = new DelegateCommand(ClosePage);
            Debug.WriteLine("Constructor: ContactPageViewModel");
        }
        #endregion

        public DelegateCommand CloseCommand { get; private set; }

        private async void ClosePage()
        {
            await Navigation.GoBackAsync();
            Debug.WriteLine("Return to: ClosePage");
        }
    }
}
