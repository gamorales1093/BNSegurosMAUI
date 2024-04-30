using BNSegurosMAUI.Views;

namespace BNSegurosMAUI;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        //FlowDirection = FlowDirection == Microsoft.Maui.FlowDirection.LeftToRight ? Microsoft.Maui.FlowDirection.RightToLeft : Microsoft.Maui.FlowDirection.LeftToRight;
       // InvalidateMeasure();

    }

  /*  protected override bool OnBackButtonPressed()
    {

        return base.OnBackButtonPressed();
    }
  */

}

