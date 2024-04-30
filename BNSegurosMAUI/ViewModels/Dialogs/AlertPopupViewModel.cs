using System;
using System.Windows.Input;
using CommunityToolkit.Maui.Core;

namespace BNSegurosMAUI.ViewModels.Dialogs
{

    public delegate Task CloseHandler<T>(T result);
    public class AlertPopupViewModel: BasePageViewModel
	{

        public event CloseHandler<bool> OnClose;
       // public DelegateCommand<bool> AcceptCommand { get; set; }
        public ICommand AcceptCommand { get; set; }


        public DialogCloseListener RequestClose { get; set; }

        private string _icon;
        public string Icon
        {
            get => _icon;
            set => SetProperty(ref _icon, value);
        }

        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private string _message;
        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        private string _acceptButton;
        public string AcceptButton
        {
            get => _acceptButton;
            set => SetProperty(ref _acceptButton, value);
        }

        private Color _color;
        public Color Color
        {
            get => _color;
            set => SetProperty(ref _color, value);
        }


        //public DialogCloseEvent RequestClose { get; set; }




        //   DialogCloseListener IDialogAware.RequestClose => throw new NotImplementedException();

        private DialogCloseListener exit3()
        {
            return new DialogCloseListener();
        }

        private void  OnAcceptCommand()
        {
            try
            {

                 OnClose?.Invoke(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public bool CanCloseDialog() => true;

        public void OnDialogClosed()
        {
            Console.WriteLine("OnDialogClosed");
        }

        public AlertPopupViewModel()
		{
            AcceptCommand = new DelegateCommand(OnAcceptCommand);
          //  AcceptCommand=  new Command<bool>(OnAcceptCommand)
        }

		internal void SetParameters(DialogParameters parameters)
		{
            Title = parameters.GetValue<string>(DialogConstants.DialogTitleKey);
            Message = parameters.GetValue<string>(DialogConstants.DialogMessageKey);
            AcceptButton = parameters.GetValue<string>(DialogConstants.DialogAcceptButtonKey);
            Icon = parameters.GetValue<string>(DialogConstants.DialogIconKey);

        }

	}
}

