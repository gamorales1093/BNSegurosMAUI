using System;
using Prism.AppModel;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services;
using Microsoft.Maui.Controls;
using Microsoft.Maui;
using BNSegurosMAUI.Helpers;

namespace BNSegurosMAUI.ViewModels.Dialogs
{
    public class MessageDialogViewModel : BindableBase, IDialogAware
    {
        public MessageDialogViewModel()
        {
            AcceptCommand = new DelegateCommand(OnAcceptCommand);
            RequestClose = new DialogCloseListener();

        }

        public DelegateCommand AcceptCommand { get; }

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

       private  DialogCloseListener exit3()
        {
            return new DialogCloseListener();
        }

        private void OnAcceptCommand()
        {
            try
            {
                RequestClose.Invoke();
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

        public void OnDialogOpened(IDialogParameters parameters)
        {
            
            Title = parameters.GetValue<string>(DialogConstants.DialogTitleKey);
            Message = parameters.GetValue<string>(DialogConstants.DialogMessageKey);
            AcceptButton = parameters.GetValue<string>(DialogConstants.DialogAcceptButtonKey);
            Icon = parameters.GetValue<string>(DialogConstants.DialogIconKey);

        }
    }
}