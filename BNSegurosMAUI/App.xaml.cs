#if ANDROID
using Android.Graphics.Drawables;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using Android.Content;
#endif
using BNSegurosMAUI.Handlers;
using BNSegurosMAUI.ViewModels.Dialogs;
using BNSegurosMAUI.Views;
using BNSegurosMAUI.Views.Controls;
using BNSegurosMAUI.Views.Dialogs;


namespace BNSegurosMAUI;

    public partial class App 
{

    public App()
    {
        InitializeComponent();
        InicializeRenderLabel();
        InicializeRenderEntry();
        Current.UserAppTheme = AppTheme.Light;
        //MainPage = new AppShell();
        ChangeThemelight();
        Current.RequestedThemeChanged += (s, a) =>
        {
            if (Current.UserAppTheme != AppTheme.Light)
            {
                ChangeThemelight();
            }
        };

    }

    private void ChangeThemelight()
    {
        Current.UserAppTheme = AppTheme.Light;
    }

    public void InicializeRenderLabel()
    {



        Microsoft.Maui.Handlers.LabelHandler.Mapper.AppendToMapping(nameof(IView.Background), (handler, view) =>
        {
            if (view is JustifiedLabel)
            {


#if ANDROID
                try
                {
                    handler.PlatformView.JustificationMode = Android.Text.JustificationMode.InterWord;
                }
                catch (Exception ex)
                {

                }

#elif IOS
                handler.PlatformView.TextAlignment = UIKit.UITextAlignment.Justified;
#elif WINDOWS
                //
#endif

            }
        });






        /*     Microsoft.Maui.Controls.Handlers.Items.CollectionViewHandler.Mapper.AppendToMapping(nameof(IView.DesiredSize), (h, v) =>
             {

                 if(v is CustomCollectionView)
             {





     #if ANDROID
                     //     handler.PlatformView.JustificationMode = Android.Text.JustificationMode.InterWord;



     #elif IOS

                     h.PlatformView.AutosizesSubviews=true;
                     h.PlatformView.SizeToFit();


     #elif WINDOWS
                     //
     #endif


                 }
             });

             */

    }

    public void InicializeRenderEntry()
    {


        Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(IEntry.Background), (handler, entry) =>
        {
            if (entry is MainEntry)
            {
                var backgroundColor = ((MainEntry)entry).EntryBackgroundColor;
                var borderColor = ((MainEntry)entry).BorderColor;
                var borderWidth = ((MainEntry)entry).BorderWidth;
                var cornerRadius = ((MainEntry)entry).CornerRadius;

#if ANDROID


                borderWidth = borderWidth > 0 ? borderWidth : 0;
                cornerRadius = cornerRadius > 0 ? cornerRadius : 0;

                var strokeWidth = 3;
                var radius = 5;

                var background = new GradientDrawable();
                background.SetColor(backgroundColor.ToAndroid());

                if (radius > 0)
                {
                    background.SetCornerRadius(radius);
                }
                if (strokeWidth > 0)
                {
                    background.SetStroke(strokeWidth, borderColor.ToAndroid());
                }


                if (((MainEntry)entry).HasShadow)
                {
                    var shadowColors = new int[] {
                    -1,
                    0856918
                };
                    var shadow = new GradientDrawable(GradientDrawable.Orientation.TopBottom, shadowColors);
                    shadow.SetCornerRadius(radius);

                    int inset = strokeWidth / 2;
                    var drawables = new Drawable[] { shadow, background };
                    var layer = new LayerDrawable(drawables);
                    layer.SetLayerInset(0, 0, 0, 0, 0);
                    layer.SetLayerInset(1, inset, strokeWidth, inset, strokeWidth);

                    handler.PlatformView.Background = layer;
                }
                else
                {
                    handler.PlatformView.Background = background;
                }
                handler.PlatformView.SetPadding(20, 0, 20, 0);

                GradientDrawable shape = new GradientDrawable();
                shape.SetShape(ShapeType.Rectangle);
                shape.SetCornerRadius(8);




#elif IOS
                if (borderColor.Red == 0 && borderColor.Green == 0 && borderColor.Blue == 0 && borderColor.Alpha == 0)
                {
                    handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
                }
#elif WINDOWS
                //
#endif

            }
        });

        //DatePicker

        Microsoft.Maui.Handlers.DatePickerHandler.Mapper.AppendToMapping(nameof(IEntry.Background), (handler, entry) =>
        {
            if (entry is MainDatePicker)
            {


#if ANDROID
                var backgroundColor = ((MainDatePicker)entry).EntryBackgroundColor;
                var borderColor = ((MainDatePicker)entry).BorderColor;
                var borderWidth = ((MainDatePicker)entry).BorderWidth;
                var cornerRadius = ((MainDatePicker)entry).CornerRadius;

                borderWidth = borderWidth > 0 ? borderWidth : 0;
                cornerRadius = cornerRadius > 0 ? cornerRadius : 0;

                var strokeWidth = 3;
                var radius = 5;

                var background = new GradientDrawable();
                background.SetColor(backgroundColor.ToAndroid());

                if (radius > 0)
                {
                    background.SetCornerRadius(radius);
                }
                if (strokeWidth > 0)
                {
                    background.SetStroke(strokeWidth, borderColor.ToAndroid());
                }
                handler.PlatformView.SetPadding(20, 0, 20, 0);
                handler.PlatformView.Background = background;




#elif IOS
                
#elif WINDOWS
                //
#endif

            }
        });

        //SearchBar
        Microsoft.Maui.Handlers.SearchBarHandler.Mapper.AppendToMapping(nameof(ISearchBar.Background), (handler, entry) =>
        {
            if (entry is MainSearchBar)
            {


#if ANDROID
                var backgroundColor = ((MainSearchBar)entry).EntryBackgroundColor;
                var borderColor = ((MainSearchBar)entry).BorderColor;
                var borderWidth = ((MainSearchBar)entry).BorderWidth;
                var cornerRadius = ((MainSearchBar)entry).CornerRadius;

                borderWidth = borderWidth > 0 ? borderWidth : 0;
                cornerRadius = cornerRadius > 0 ? cornerRadius : 0;

                var strokeWidth = 3;
                var radius = 50;

                var background = new GradientDrawable();
                background.SetColor(backgroundColor.ToAndroid());

                if (radius > 0)
                {
                    background.SetCornerRadius(radius);
                }
                if (strokeWidth > 0)
                {
                    background.SetStroke(strokeWidth, borderColor.ToAndroid());
                }

                Android.Widget.LinearLayout linearLayout = handler.PlatformView.GetChildAt(0) as Android.Widget.LinearLayout;
                linearLayout = linearLayout.GetChildAt(2) as Android.Widget.LinearLayout;
                linearLayout = linearLayout.GetChildAt(1) as Android.Widget.LinearLayout;
                linearLayout.Background = null;
                //  handler.PlatformView.SetPadding(30, 20, 20, 20);
                //handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
                 handler.PlatformView.Background = background;

               


#elif IOS
                handler.PlatformView.BarStyle = UIKit.UIBarStyle.Default;
                handler.PlatformView.SearchBarStyle = UIKit.UISearchBarStyle.Minimal;
                handler.PlatformView.BackgroundColor= UIKit.UIColor.FromRGBA(0, 0, 0, 0);

                handler.PlatformView.SearchTextField.TokenBackgroundColor = UIKit.UIColor.FromRGBA(0,0,0,0);
                handler.PlatformView.SearchTextField.BackgroundColor = UIKit.UIColor.FromRGBA(0, 0, 0, 0);
#elif WINDOWS
                //
#endif

            }
        });

    }
    protected override void OnStart()
        {
            // Handle when your app starts

            //PROD
            //AppCenter.Start("android=250e9abc-b707-4e1f-922e-34549cccf01c;" + "ios=6bca1d96-f23f-4c64-932c-3a14675068a7;", typeof(Analytics), typeof(Crashes));

            //QA
            //AppCenter.Start("android=8c5e5415-a161-4244-89df-d66ace027d6e;" + "ios=d04b75b7-4747-46c4-92fa-1396c7cb4609;", typeof(Analytics), typeof(Crashes));
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }


    public override void CloseWindow(Window window)
    {
        base.CloseWindow(window);
    }

    

    /*     protected override void RegisterTypes(IContainerRegistry containerRegistry)
         {
             containerRegistry.RegisterForNavigation<NavigationPage>();
             containerRegistry.RegisterForNavigation<HomePage>();
             containerRegistry.RegisterForNavigation<NoticePage>();
             containerRegistry.RegisterForNavigation<InsurancesPage>();
             containerRegistry.RegisterForNavigation<ContactPage>();
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
             containerRegistry.RegisterDialog<MessageDialog, MessageDialogViewModel>();
         }*/
}


