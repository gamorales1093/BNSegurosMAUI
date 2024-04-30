using System;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace BNSegurosMAUI.Views.Controls
{
    public class MainSearchBar : SearchBar
    {
        public static readonly BindableProperty BorderColorProperty =
            BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(MainSearchBar), default(Color));
        public Color BorderColor
        {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }

        public static readonly BindableProperty BorderWidthProperty =
            BindableProperty.Create(nameof(BorderWidth), typeof(float), typeof(MainSearchBar), default(float));
        public float BorderWidth
        {
            get => (float)GetValue(BorderWidthProperty);
            set => SetValue(BorderWidthProperty, value);
        }

        public static readonly BindableProperty CornerRadiusProperty =
            BindableProperty.Create(nameof(CornerRadius), typeof(float), typeof(MainSearchBar), default(float));
        public float CornerRadius
        {
            get => (float)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public static readonly BindableProperty LeftPaddingProperty =
            BindableProperty.Create(nameof(LeftPadding), typeof(float), typeof(MainSearchBar), 8.0f);
        public float LeftPadding
        {
            get => (float)GetValue(LeftPaddingProperty);
            set => SetValue(LeftPaddingProperty, value);
        }

        public static readonly BindableProperty RightPaddingProperty =
            BindableProperty.Create(nameof(RightPadding), typeof(float), typeof(MainSearchBar), 8.0f);
        public float RightPadding
        {
            get => (float)GetValue(RightPaddingProperty);
            set => SetValue(RightPaddingProperty, value);
        }

        public static readonly BindableProperty HasShadowProperty =
            BindableProperty.Create(nameof(HasShadow), typeof(bool), typeof(MainSearchBar), default(bool));
        public bool HasShadow
        {
            get => (bool)GetValue(HasShadowProperty);
            set => SetValue(HasShadowProperty, value);
        }

        /*
         * There is a bug on Xamarin.Forms EntryRenderer where the view background color stays on all of the view's
         * area even if we set a rounded corner, so we can always see the sharp corners underneath.
         * So we set "BackgroundColor" to transparent and use EntryBackgroundColor as our real background color.
         */
        public static readonly BindableProperty EntryBackgroundColorProperty =
            BindableProperty.Create(nameof(EntryBackgroundColor), typeof(Color), typeof(MainSearchBar), default(Color));
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
