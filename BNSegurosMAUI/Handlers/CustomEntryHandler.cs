
#if IOS || MACCATALYST
using PlatformView = Microsoft.Maui.Platform.MauiTextField;
using UIKit;
#elif ANDROID
using PlatformView = AndroidX.AppCompat.Widget.AppCompatEditText;
using Android.Graphics.Drawables;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using Android.Views;
using Android.Content;
#elif WINDOWS
using PlatformView = Microsoft.UI.Xaml.Controls.TextBox;
#elif (NETSTANDARD || !PLATFORM) || (NET6_0_OR_GREATER && !IOS && !ANDROID)
using PlatformView = System.Object;
#endif
using Microsoft.Maui.Handlers;
using System;
using BNSegurosMAUI.Views.Controls;
using System.Runtime.InteropServices;
using Microsoft.Maui.Platform;
using Microsoft.Maui.Controls;


namespace BNSegurosMAUI.Handlers
{
	public partial class CustomEntryHandler: EntryHandler
    {

        private static readonly IPropertyMapper<MainEntry, CustomEntryHandler> PropertyMapper = new PropertyMapper<MainEntry, CustomEntryHandler>(Mapper)
        {
            [nameof(MainEntry.BorderColor)] = MapText,

        };

        public CustomEntryHandler() : base(PropertyMapper)
        {
        }

        protected override void ConnectHandler(PlatformView platformView)
        {
            

            base.ConnectHandler(platformView);
        }
        protected override void DisconnectHandler(PlatformView platformView)
        {
            base.DisconnectHandler(platformView);
        }

        private static void MapText(CustomEntryHandler handler, MainEntry entry)
        {


#if IOS

          //  handler.PlatformView.TextColor  = UIKit.UIColor.Blue;

#elif ANDROID

           
            GradientDrawable shape = new GradientDrawable();
            shape.SetShape(ShapeType.Rectangle);
            shape.SetCornerRadius(8);

            if (entry.IsBorderErrorVisible)
                shape.SetStroke(3, (entry).BorderColor.ToAndroid());
            else
                shape.SetStroke(3, (entry.BorderColor.ToAndroid()));

            handler.PlatformView.Background= shape;

      


#endif
        }


    }



    public partial class ListViewHandler : Microsoft.Maui.Controls.Handlers.Compatibility.ListViewRenderer
    {

        private static readonly IPropertyMapper<CustomListView, ListViewHandler> PropertyMapper = new PropertyMapper<CustomListView, ListViewHandler>(Mapper)
        {
            [nameof(CustomListView.SizeChanged)] = MapText,

        };

        private static void MapText(ListViewHandler handler, CustomListView view)
        {
            //view.up
        }

        public event Action ViewCellSizeChangedEvent
        {
            add { _viewCellSizeChangedEvent += value; }
            remove { _viewCellSizeChangedEvent -= value; }
        }
        private Action _viewCellSizeChangedEvent;

#if ANDROID
        public ListViewHandler(Context context) : base(context)
        {
        }
        //  handler.PlatformView.TextColor  = UIKit.UIColor.Blue;

#endif



        private void ListViewHandler_ViewCellSizeChangedEvent()
        {
         /*   var tv = Control as UITableView;
            if (tv == null) return;
            tv.BeginUpdates();
            tv.EndUpdates();*/
        }

        public void RaiseViewCellSizeChangedEvent()
        {
            _viewCellSizeChangedEvent?.Invoke();
        }

    }


    public class FixedScrollview : ScrollView
    {
        public FixedScrollview()
        {
#if IOS
      ScrollViewHandler.Mapper.AppendToMapping(nameof(IScrollView.ContentSize), OnScrollViewContentSizePropertyChanged);
#endif
        }

#if IOS
  private void OnScrollViewContentSizePropertyChanged(IScrollViewHandler _, IScrollView __)
  {
      if (Handler?.PlatformView is not UIView platformUiView)
          return;
      
      if (platformUiView.Subviews.FirstOrDefault() is not { } contentView)
          return;
      
      contentView.SizeToFit();
  }
#endif
    }

}


