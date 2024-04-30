using System;
using System.Threading.Tasks;
using Prism.Services;

namespace BNSegurosMAUI.ViewModels.Dialogs
{
    public enum DialogInputFormat { Default=0,Numeric}

    public static class DialogExtension
    {
        public static Task<IDialogResult> ShowCustomDialogAsync(this IDialogService dialogService, string name, IDialogParameters parameters)
        {
            var tcs = new TaskCompletionSource<IDialogResult>();

            try
            {
                dialogService.ShowDialog(name, parameters, (result) =>
                {
                        
                    if (result.Exception != null)
                    {
                        tcs.SetException(result.Exception);
                        return;
                    }
                    tcs.SetResult(result);
                });
            } 
            catch (Exception ex)
            {
                tcs.SetException(ex);
            }
            return tcs.Task;
        }

        public static Task<bool> ConfirmDialogAsync(this IDialogService dialogService, string name, IDialogParameters parameters)
        {
            var tcs = new TaskCompletionSource<bool>();

            try
            {
                dialogService.ShowDialog(name, parameters, (result) =>
                {
                    if (result.Exception != null)
                    {
                        tcs.SetException(result.Exception);
                        return;
                    }
                    tcs.SetResult(result.Parameters.GetValue<bool>("Result"));
                });
            }
            catch (Exception ex)
            {
                tcs.SetException(ex);
            }
            return tcs.Task;
        }

    }
}
