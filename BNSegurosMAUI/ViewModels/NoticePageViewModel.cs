using System;
using System.Diagnostics;
using BNSegurosMAUI.Helpers;
using BNSegurosMAUI.Models;
using BNSegurosMAUI.Resources.Text;
using FFImageLoading;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace BNSegurosMAUI.ViewModels
{
    public class NoticePageViewModel : BasePageViewModel
    {
        #region Constructor
        public NoticePageViewModel(INavigationService navigationService, IPageDialogService pageDialog)
            : base(navigationService, pageDialog)
        {
            Title = TextResources.Notices_TitleLabel;

            Notice model = SessionInfo.GetInstance().NoticeModel;
            NoticeImageSource = model.NoticeImageUrl;
            NoticeTitle = model.NoticeTitle;
            NoticeDescription = model.NoticeDescription;

            CloseCommand = new DelegateCommand(ClosePage);

            Debug.WriteLine("Constructor: NoticePageViewModel");
        }
        #endregion

        #region Variables
        private string _noticeImageSource;
        public string NoticeImageSource
        {
            get { return _noticeImageSource; }
            set { SetProperty(ref _noticeImageSource, value); }
        }

        private string _noticeTitle;
        public string NoticeTitle
        {
            get { return _noticeTitle; }
            set { SetProperty(ref _noticeTitle, value); }
        }

        private string _noticeDescription;
        public string NoticeDescription
        {
            get { return _noticeDescription; }
            set { SetProperty(ref _noticeDescription, value); }
        }

        public DelegateCommand CloseCommand { get; private set; }
        #endregion

        #region Methods
        private async void ClosePage()
        {
            //await Navigation.GoBackAsync();
            await Navigation.GoBackAsync();
            Debug.WriteLine("Return to: ClosePage");
        }
        #endregion
    }
}
