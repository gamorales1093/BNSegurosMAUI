using System;
namespace BNSegurosMAUI.Views.Components
{
	public class CustomFlyoutTemplateSelector : DataTemplateSelector
    {
        public DataTemplate Normal { get; set; }
        public DataTemplate Menu { get; set; }
        public DataTemplate Hidden { get; set; }
        public DataTemplate Separator { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return ((CustomFlyoutItem)item).Type switch
            {
                CustomFlyoutType.Menu => Menu,
                CustomFlyoutType.Hidden => Hidden,
                CustomFlyoutType.Separator => Separator,
                _ => Normal
            };
        }
    }
}

