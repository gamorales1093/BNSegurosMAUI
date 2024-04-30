using System;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace BNSegurosMAUI.Views.Controls
{
    public class MainDatePicker : DatePicker
    {
        public static readonly BindableProperty EnterTextProperty = BindableProperty.Create(propertyName: "Placeholder",
            returnType: typeof(string),
            declaringType: typeof(MainDatePicker),
            defaultValue: default(string));
        public string Placeholder { get; set; }

        public static readonly BindableProperty BorderColorProperty =
            BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(MainDatePicker), default(Color));
        public Color BorderColor
        {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }

        public static readonly BindableProperty BorderWidthProperty =
            BindableProperty.Create(nameof(BorderWidth), typeof(float), typeof(MainDatePicker), default(float));
        public float BorderWidth
        {
            get => (float)GetValue(BorderWidthProperty);
            set => SetValue(BorderWidthProperty, value);
        }

        public static readonly BindableProperty CornerRadiusProperty =
            BindableProperty.Create(nameof(CornerRadius), typeof(float), typeof(MainDatePicker), default(float));
        public float CornerRadius
        {
            get => (float)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public static readonly BindableProperty LeftPaddingProperty =
            BindableProperty.Create(nameof(LeftPadding), typeof(float), typeof(MainDatePicker), 8.0f);
        public float LeftPadding
        {
            get => (float)GetValue(LeftPaddingProperty);
            set => SetValue(LeftPaddingProperty, value);
        }

        public static readonly BindableProperty EntryBackgroundColorProperty =
            BindableProperty.Create(nameof(EntryBackgroundColor), typeof(Color), typeof(MainDatePicker), default(Color));
        public Color EntryBackgroundColor
        {
            get => (Color)GetValue(EntryBackgroundColorProperty);
            set
            {
                SetValue(EntryBackgroundColorProperty, value);
                BackgroundColor = Colors.Transparent;
            }
        }
    }
}