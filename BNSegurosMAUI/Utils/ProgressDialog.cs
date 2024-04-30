using System;
using Microsoft.Maui.Controls;
using Microsoft.Maui;
using Controls.UserDialogs.Maui;

namespace BNSegurosMAUI.Utils
{
    public static class ProgressDialog
    {
        public static void Show(string message)
        {
                UserDialogs.Instance.ShowLoading(message, MaskType.Black);
        }

        public static void Dismiss()
        {

                UserDialogs.Instance.HideHud();
        }
    }

}
