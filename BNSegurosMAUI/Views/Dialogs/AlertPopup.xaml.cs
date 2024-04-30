using BNSegurosMAUI.ViewModels.Dialogs;
using CommunityToolkit.Maui.Views;

namespace BNSegurosMAUI.Views.Dialogs;

public partial class AlertPopup : Popup
{
	AlertPopupViewModel vm;
	public AlertPopup(AlertPopupViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
		//vm = (AlertPopupViewModel?)(BindingContext = new AlertPopupViewModel());
        vm.OnClose += async result => await CloseAsync(result, CancellationToken.None);
    }
}
