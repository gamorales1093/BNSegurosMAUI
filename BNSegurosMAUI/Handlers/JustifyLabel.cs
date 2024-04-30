
using System.Runtime.CompilerServices;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;

namespace BNSegurosMAUI.Handlers
{
    public class JustifiedLabel : Label
    {
        public static readonly BindableProperty JustifyTextProperty =
            BindableProperty.Create(
                propertyName: nameof(JustifyText),
                returnType: typeof(bool),
                declaringType: typeof(JustifiedLabel),
                defaultValue: true,
                defaultBindingMode: BindingMode.OneWay
            );

        public bool JustifyText
        {
            get => (bool)GetValue(JustifyTextProperty);
            set => SetValue(JustifyTextProperty, value);
        }
    }

    public partial class MainSearch : SearchBar
    {
        public static readonly BindableProperty EnterTextProperty = BindableProperty.Create(propertyName: "Placeholder",
        returnType: typeof(string),
           declaringType: typeof(MainSearch),
           defaultValue: default(string));
        public string Placeholder { get; set; }

        public static readonly BindableProperty BorderColorProperty =
            BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(MainSearch), default(Color));
        public Color BorderColor
        {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }

        public static readonly BindableProperty BorderWidthProperty =
            BindableProperty.Create(nameof(BorderWidth), typeof(float), typeof(MainSearch), default(float));
        public float BorderWidth
        {
            get => (float)GetValue(BorderWidthProperty);
            set => SetValue(BorderWidthProperty, value);
        }

        public static readonly BindableProperty CornerRadiusProperty =
            BindableProperty.Create(nameof(CornerRadius), typeof(float), typeof(MainSearch), default(float));
        public float CornerRadius
        {
            get => (float)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public static readonly BindableProperty LeftPaddingProperty =
            BindableProperty.Create(nameof(LeftPadding), typeof(float), typeof(MainSearch), 8.0f);
        public float LeftPadding
        {
            get => (float)GetValue(LeftPaddingProperty);
            set => SetValue(LeftPaddingProperty, value);
        }

        public static readonly BindableProperty EntryBackgroundColorProperty =
            BindableProperty.Create(nameof(EntryBackgroundColor), typeof(Color), typeof(MainSearch), default(Color));
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

    public class CustomListView : ListView
    {
       
    }
    public class ExpanderViewCell : Microsoft.Maui.Controls.ViewCell
    {
        public static event Action ViewCellSizeChangedEvent;

        public static readonly BindableProperty ExpandedViewProperty =
            BindableProperty.Create(nameof(ExpandedView), typeof(View), typeof(ExpanderViewCell));

        public static readonly BindableProperty HeaderViewProperty =
            BindableProperty.Create(nameof(HeaderView), typeof(View), typeof(ExpanderViewCell));

        public static readonly BindableProperty IsExpandedProperty =
            BindableProperty.Create(nameof(IsExpanded), typeof(bool), typeof(ExpanderViewCell), false);

        public static readonly BindableProperty DividerViewProperty =
            BindableProperty.Create(nameof(DividerView), typeof(View), typeof(ExpanderViewCell));

        public View ExpandedView
        {
            get { return (View)GetValue(ExpandedViewProperty); }
            set { SetValue(ExpandedViewProperty, value); }
        }

        public View HeaderView
        {
            get { return (View)GetValue(HeaderViewProperty); }
            set { SetValue(HeaderViewProperty, value); }
        }

        public View DividerView
        {
            get { return (View)GetValue(DividerViewProperty); }
            set { SetValue(DividerViewProperty, value); }
        }

        public bool IsExpanded
        {
            get { return (bool)GetValue(IsExpandedProperty); }
            set { SetValue(IsExpandedProperty, value); }
        }

        public static readonly BindableProperty UpdateExpandedOnTappedProperty = BindableProperty.CreateAttached(
            propertyName: "UpdateExpandedOnTapped",
            returnType: typeof(bool),
            declaringType: typeof(ExpanderViewCell),
            defaultValue: false,
            propertyChanged: OnUpdateExpandedOnTappedChanged);

        public static void OnUpdateExpandedOnTappedChanged(BindableObject bindable, object oldValue, object newValue)
        {
            // Once the view is attached to the visual tree, we can add the gesture recognizer
            if ((bindable is not View view))
            {
                return;
            }

            /*view.Effects.Add(new OnAttachedListenerEffect
            {
                OnAttachedToWindow = () =>
                {
                    var closestExpanderViewCell = view.ClosestAncestor<ExpanderViewCell>();
                    if (closestExpanderViewCell is not null)
                    {
                        if (newValue is bool updateExpandedOnTapped)
                        {
                            if (updateExpandedOnTapped)
                            {
                                view.GestureRecognizers.Add(new TapGestureRecognizer
                                {
                                    Command = new Command(() => closestExpanderViewCell.IsExpanded = !closestExpanderViewCell.IsExpanded)
                                });
                            }
                        }
                    }
                }
            }
            );*/
        }

        public static void SetUpdateExpandedOnTapped(BindableObject bindable, bool value) => bindable.SetValue(UpdateExpandedOnTappedProperty, value);

        public static bool GetUpdateExpandedOnTapped(BindableObject bindable) => (bool)bindable.GetValue(UpdateExpandedOnTappedProperty);

        private void UpdateExpandedOnTapped(object sender, EventArgs e)
        {
            IsExpanded = !IsExpanded;
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();

            if (Parent != null)
            {
                ConfigureContent();
            }
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(IsExpanded))
            {
                OnExpandedChanged();
            }
        }
        private void ConfigureContent()
        {
            var expanderGrid = new Grid
            {
                RowDefinitions =
            {
                new RowDefinition( GridLength.Auto ),
                new RowDefinition( GridLength.Auto ),
                new RowDefinition( GridLength.Auto ),
            }
            };

            if (HeaderView is not null)
            {
                expanderGrid.Add(HeaderView, 0, 0);
                HeaderView.GestureRecognizers.Add(new TapGestureRecognizer
                {
                    Command = new Command(() => IsExpanded = !IsExpanded)
                });
            }

            if (ExpandedView is not null)
            {
                expanderGrid.Add(ExpandedView, 0, 1);
                ExpandedView.SetBinding(View.IsVisibleProperty, new Binding(nameof(IsExpanded), source: this));
            }

            if (DividerView is not null)
            {
                expanderGrid.Add(DividerView, 0, 2);
            }

            View = expanderGrid;
        }

        private void OnExpandedChanged()
        {
            if (Parent is not ListView listView)
            {
                return;
            }

#if IOS
        if( listView.Handler is Handlers.ListViewHandler listViewHandler )
        {
            listViewHandler.RaiseViewCellSizeChangedEvent();
        }
#endif
        }
    }

    public class MyJustifiedLabelHandler : LabelHandler
    {
        private static readonly IPropertyMapper<JustifiedLabel, MyJustifiedLabelHandler> PropertyMapper = new PropertyMapper<JustifiedLabel, MyJustifiedLabelHandler>(Mapper)
        {
            [nameof(JustifiedLabel.JustifyText)] = MapJustificationMode
        };

        public MyJustifiedLabelHandler() : base(PropertyMapper)
        {
        }


        public static void MapJustificationMode(LabelHandler handler, Label label)
        {



#if ANDROID
            /* if (Build.VERSION.SdkInt >= BuildVersionCodes.O) // O == API level 26.0
             {

               //  handler.PlatformView.JustificationMode = JustificationMode.None;// JustificationMode.InterWord;



             }*/

#elif IOS
                handler.PlatformView.TextAlignment = UIKit.UITextAlignment.Justified;
#endif

        }
    }
}

