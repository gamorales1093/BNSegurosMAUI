using BNSegurosMAUI.ViewModels.Dialogs;
using BNSegurosMAUI.Views;
using BNSegurosMAUI.Views.Dialogs;
using FFImageLoading.Maui;
using Microsoft.Extensions.Logging;
using BNSegurosMAUI.ViewModels;
using CommunityToolkit.Maui;
using Controls.UserDialogs.Maui;
using BNSegurosMAUI.Handlers;
using BNSegurosMAUI.Views.Controls;
using Microsoft.Maui.Controls.Compatibility.Hosting;
using Syncfusion.Maui.Core.Hosting;

#if IOS
using BNSegurosMAUI.Platforms.iOS;
#endif
#if ANDROID
using BNSegurosMAUI.Platforms.Android;
#endif
namespace BNSegurosMAUI;

public static class MauiProgram
{

    public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
     //  builder.Services.AddTransient<AlertPopupViewModel>();
        builder
            
			.UseMauiApp<App>()
             .UseFFImageLoading()
             .UseUserDialogs()
             .UseMauiCompatibility()
             
             .ConfigureMauiHandlers(handlers =>
             {
                 handlers.AddHandler(typeof(JustifiedLabel), typeof(MyJustifiedLabelHandler));
                 handlers.AddHandler(typeof(MainEntry), typeof(CustomEntryHandler));

#if IOS
                 handlers.AddHandler(typeof(CustomListView), typeof(ListViewHandler));
                 handlers.AddHandler<Shell, CustomShellRenderer>();
                handlers.AddHandler<CustomCollectionView, CustomCollectionViewRenderer>();


#elif ANDROID
                 handlers.AddHandler<Shell, CustomShellRenderer>();
#endif


             })
             .UseMauiCommunityToolkit()
             .UsePrism(prism => prism
             

                .RegisterTypes(containerRegistry =>
                {
                    
                    Routing.RegisterRoute("home", typeof(AppShell));
                    ViewModelLocationProvider.Register<HomePage, HomePageViewModel>();
                    containerRegistry.RegisterForNavigation<NavigationPage>();
                    containerRegistry.RegisterForNavigation<HomePage, HomePageViewModel>();
                    containerRegistry.RegisterForNavigation<NoticePage, NoticePageViewModel>();
                    containerRegistry.RegisterForNavigation<AppShell>();
                    containerRegistry.RegisterForNavigation<NoticePage>();
                    containerRegistry.RegisterForNavigation<InsurancesPage>();
                    containerRegistry.RegisterForNavigation<ContactPage, ContactPageViewModel>();
                    containerRegistry.RegisterForNavigation<ContactAgentPage>();
                    containerRegistry.RegisterForNavigation<ContactAssistancePage>();
                    containerRegistry.RegisterForNavigation<ContactAgentListPage>();
                    containerRegistry.RegisterForNavigation<FAQPage>();
                    containerRegistry.RegisterForNavigation<TipPage>();
                    containerRegistry.RegisterForNavigation<InsuranceDetailPage>();
                    containerRegistry.RegisterForNavigation<RequestInsurancePage>();
                    containerRegistry.RegisterForNavigation<CotizeInsurancePage>();
                    containerRegistry.RegisterForNavigation<RequestProcedurePage>();
                    containerRegistry.RegisterForNavigation<HTMLPage>();
                    containerRegistry.RegisterForNavigation<ProcedureFormPage>();
                    containerRegistry.RegisterForNavigation<SinisterPage>();
                    containerRegistry.RegisterForNavigation<SinisterStepsPage>();
                    containerRegistry.RegisterForNavigation<OnlineInsurancesPage>();
                    containerRegistry.RegisterForNavigation<OnlineInsuranceDetailPage>();
                    containerRegistry.RegisterForNavigation<BuyInsurancePage>();
                    containerRegistry.RegisterForNavigation<TestStyles, TestStylesViewModel>();
                    //containerRegistry.RegisterDialog<MessageDialog,MessageDialogViewModel>();
                }).CreateWindow(async (container, navigationService) => await navigationService.CreateBuilder().AddSegment<AppShell>()
                .NavigateAsync(HandleNavigationError))

                .ConfigureServices(services =>
                {
                    services.AddTransientPopup<AlertPopup, AlertPopupViewModel>();
                })
                
                )

            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                //fonts.AddFont("OpenSans-Regular.ttf", "AvenirBook");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("Sans Serif Medium.ttf", "SansSerifMedium");
                try
                {
                    fonts.AddFont("Roboto-Regular.ttf", "AvenirBook");
                    fonts.AddFont("Roboto-Bold.ttf", "AvenirBookBold");
                    //  fonts.AddFont("Roboto-Bold.ttf", "AvenirBook");
                }
                catch (Exception ex)
                {

                }
                
            });

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}

    private static void HandleNavigationError(Exception ex)
    {
        Console.WriteLine(ex);
        System.Diagnostics.Debugger.Break();
    }
}

