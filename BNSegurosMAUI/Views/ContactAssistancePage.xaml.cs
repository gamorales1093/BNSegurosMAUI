using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace BNSegurosMAUI.Views
{
    public partial class ContactAssistancePage : ContentPage
    {
        public ContactAssistancePage()
        {
            InitializeComponent();
        }
        
        void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ItemList.SelectedItem = null;
        }
    }
}