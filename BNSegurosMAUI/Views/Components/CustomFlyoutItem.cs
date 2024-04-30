using System;
namespace BNSegurosMAUI.Views.Components
{
    public enum CustomFlyoutType
    {
        Normal,
        Menu,
        Hidden,
        Separator
    }

    public class CustomFlyoutItem : FlyoutItem
    {
        public CustomFlyoutType Type
        {
            get { return (CustomFlyoutType)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }

        public static readonly BindableProperty TypeProperty = BindableProperty.Create(
            propertyName: "Type",
            returnType: typeof(CustomFlyoutType),
            declaringType: typeof(CustomFlyoutItem),
            defaultValue: CustomFlyoutType.Normal
        );

        //public string Description { get; set; } = "";

        public static readonly BindableProperty DescriptionProperty = BindableProperty.Create(
            "Description",
            typeof(string),
            typeof(CustomFlyoutItem), "", BindingMode.TwoWay);

        public string Description
        {
            get => (string)GetValue(DescriptionProperty);
            set => SetValue(DescriptionProperty, value);
        }


    }
}

