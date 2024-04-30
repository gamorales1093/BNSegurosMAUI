using System;
using Foundation;
using Microsoft.Maui.Controls.Handlers.Items;
using UIKit;

namespace BNSegurosMAUI.Platforms.iOS
{
    internal sealed class CustomItemsViewController : ReorderableItemsViewController<ReorderableItemsView>
    {
        // protected override Boolean IsHorizontal => false;

        public  CustomItemsViewController(ReorderableItemsView itemsView, ItemsViewLayout itemsLayout)
            : base(itemsView, itemsLayout)
        {
        }

        
        public override void PressesChanged(NSSet<UIPress> presses, UIPressesEvent evt)
        {
            base.PressesChanged(presses, evt);
        }

        protected override UICollectionViewDelegateFlowLayout CreateDelegator()
        {
            return new CustomItemsViewDelegator(ItemsViewLayout, this);
        }
    }
}

