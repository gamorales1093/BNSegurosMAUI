using System;
using BNSegurosMAUI.Models;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace BNSegurosMAUI.Views.Controls
{
	public class SinesterStepsTemplateSelector : DataTemplateSelector
	{

        public DataTemplate StepTemplate { get; set; }
        public DataTemplate TextTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (((Step)item).Description == "footer_text")
            {
                return TextTemplate;
            }
            else
            {
                return StepTemplate;
            }
        }
    }
}

