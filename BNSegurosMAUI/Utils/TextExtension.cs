using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace BNSegurosMAUI.Utils
{
    [ContentProperty("Text")]
    public class TextExtension : IMarkupExtension
    {
        private const string ResourceId = "BNSegurosMAUI.Resources.Text.TextResources";
        public string Text { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Text == null)
            {
                return null;
            }
            ResourceManager resourceManager = new ResourceManager(ResourceId, typeof(TextExtension).GetTypeInfo().Assembly);
            return resourceManager.GetString(Text, CultureInfo.CurrentCulture);
        }
    }
}
