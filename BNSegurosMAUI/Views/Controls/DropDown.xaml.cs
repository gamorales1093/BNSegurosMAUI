using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using BNSegurosMAUI.Models;
using BNSegurosMAUI.ViewModels;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace BNSegurosMAUI.Views.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DropDown
    {
        public DropDown()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty HeaderTextProperty = BindableProperty.Create(nameof(HeaderText), typeof(string), typeof(DropDown), string.Empty, BindingMode.TwoWay);

        public static readonly BindableProperty ListSourceProperty = BindableProperty.Create(nameof(ListSource), typeof(ObservableCollection<ItemDropDown>), typeof(DropDown), null, BindingMode.TwoWay);

        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(DropDown), default(Color));

        public string HeaderText
        {
            get
            {
                return (string)GetValue(HeaderTextProperty);
            }
            set
            {
                if (value != null)
                    SetValue(HeaderTextProperty, value);
            }
        }

        public ObservableCollection<ItemDropDown> ListSource
        {
            get
            {
                return (ObservableCollection<ItemDropDown>)GetValue(ListSourceProperty);
            }
            set
            {
                if (value != null)
                {
                    SetValue(ListSourceProperty, value);
                }
            }
        }

        public Color BorderColor
        {
            get
            {
                return (Color)GetValue(BorderColorProperty);
            }
            set
            {
                if (value != null)
                    SetValue(BorderColorProperty, value);
            }
        }

        private async void HeaderTapGestureRecognizerTapped(object sender, EventArgs e)
        {
            if (!BottomStack.IsVisible)
            {
                headerImage.Source = "icchevronbigdown.png";

                BottomStack.IsVisible = true;
                await BottomStack.TranslateTo(0, 0, 250);
            }
            else
            {
                headerImage.Source = "icchevronbig.png";

                BottomStack.IsVisible = false;
                await BottomStack.TranslateTo(0, -10, 250);
            }
        }

        private void ItemTapGestureRecognizerTapped(object sender, EventArgs args)
        {
            var SenderTemp = (Label)sender;
            Console.WriteLine("Sender: {0}", SenderTemp);
            HeaderText = SenderTemp.Text;
            headerImage.Source = "icchevronbig.png";

            BottomStack.IsVisible = false;
            BottomStack.TranslateTo(0, -10, 250);

            RequestInsurancePageViewModel viewModel = BindingContext as RequestInsurancePageViewModel;
            viewModel.DropDownUnfocused(HeaderText);
        }
    }
}
