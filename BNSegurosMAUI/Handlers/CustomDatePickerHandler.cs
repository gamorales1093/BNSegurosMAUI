using System;
#if ANDROID
using Android.Graphics.Drawables;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
#endif
using BNSegurosMAUI.Views.Controls;
using Microsoft.Maui.Handlers;

namespace BNSegurosMAUI.Handlers
{

    public partial class CustomDatePickerHandler : DatePickerHandler
    {

        private static readonly IPropertyMapper<MainDatePicker, CustomDatePickerHandler> PropertyMapper = new PropertyMapper<MainDatePicker, CustomDatePickerHandler>(Mapper)
        {
            [nameof(MainDatePicker.Background)] = MapText,

        };

        public CustomDatePickerHandler() : base(PropertyMapper)
        {
        }


        private static void MapText(CustomDatePickerHandler handler, MainDatePicker entry)
        {


#if IOS

          //  handler.PlatformView.TextColor  = UIKit.UIColor.Blue;

#elif ANDROID

            
            GradientDrawable shape = new GradientDrawable();
            shape.SetShape(ShapeType.Rectangle);
            shape.SetCornerRadius(8);

            shape.SetStroke(3, ((entry).BorderColor.ToAndroid()));

            handler.PlatformView.Background = shape;




#endif
        }


    }
}

