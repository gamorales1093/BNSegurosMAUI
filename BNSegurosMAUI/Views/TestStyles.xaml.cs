namespace BNSegurosMAUI.Views;


public partial class TestStyles : ContentPage
{
	public TestStyles()
	{
        InitializeComponent();

        // Eliminate Back text on BackButton
        NavigationPage.SetBackButtonTitle(this, string.Empty);
        NavigationPage.SetHasBackButton(this, false);
    }
}
