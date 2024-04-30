using System;
using Google.Android.Material.BottomNavigation;
using Microsoft.Maui.Controls.Handlers.Compatibility;
using Microsoft.Maui.Controls.Platform.Compatibility;
using Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific;
using ShellItem = Microsoft.Maui.Controls.ShellItem;

namespace BNSegurosMAUI.Platforms.Android
{
    public partial class CustomShellRenderer : ShellRenderer
    {

        protected override IShellToolbarAppearanceTracker CreateToolbarAppearanceTracker()
        {
            return new CustomToolbarAppearanceTracker();
        }

        protected override IShellFlyoutRenderer CreateShellFlyoutRenderer()
        {
            return base.CreateShellFlyoutRenderer();
        }

        protected override IShellBottomNavViewAppearanceTracker CreateBottomNavViewAppearanceTracker(ShellItem shellItem)
        {
            /* foreach (var item in shellItem.Items)
              {
                  item.FlyoutIcon.
              }
              shellItem.Items*/
            return new CustomBottomNavViewAppearanceTracker();
        }
    }
    public class CustomToolbarAppearanceTracker : IShellToolbarAppearanceTracker
    {
        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public void ResetAppearance(AndroidX.AppCompat.Widget.Toolbar toolbar, IShellToolbarTracker toolbarTracker)
        {
            //throw new NotImplementedException();
        }

        public void SetAppearance(AndroidX.AppCompat.Widget.Toolbar toolbar, IShellToolbarTracker toolbarTracker, ShellAppearance appearance)
        {
           
            toolbar.SetBackgroundColor(global::Android.Graphics.Color.Red);
        }
    }

    public class CustomFloyoutRender : IShellFlyoutRenderer
    {
        public global::Android.Views.View AndroidView => null;

        public void AttachFlyout(IShellContext context, global::Android.Views.View content)
        {
            content.LayoutDirection = (global::Android.Views.LayoutDirection)LayoutDirection.RightToLeft;
        }
    }

    public class CustomBottomNavViewAppearanceTracker : IShellBottomNavViewAppearanceTracker
    {
        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public void ResetAppearance(BottomNavigationView bottomView)
        {
            //throw new NotImplementedException();
        }

        public void SetAppearance(BottomNavigationView bottomView, IShellAppearanceElement appearance)
        {
            bottomView.LayoutDirection = (global::Android.Views.LayoutDirection)LayoutDirection.RightToLeft;
        }
    }

    public partial class CustomShellTabBarAppearanceTracker : IShellToolbarAppearanceTracker
    {
        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public void ResetAppearance(AndroidX.AppCompat.Widget.Toolbar toolbar, IShellToolbarTracker toolbarTracker)
        {
            //throw new NotImplementedException();
        }

        public void SetAppearance(AndroidX.AppCompat.Widget.Toolbar toolbar, IShellToolbarTracker toolbarTracker, ShellAppearance appearance)
        {
           //toolbar.NavigationIcon.
        }
    }
}

