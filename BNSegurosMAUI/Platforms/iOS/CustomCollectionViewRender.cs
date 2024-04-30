using System;
using BNSegurosMAUI.Views.Controls;
using CoreGraphics;

using Microsoft.Maui.Controls.Handlers.Items;
using UIKit;

namespace BNSegurosMAUI.Platforms.iOS
{

    internal sealed class CustomCollectionViewRenderer : CollectionViewHandler
    {
        private static readonly IPropertyMapper<CustomCollectionView, CustomCollectionViewRenderer> PropertyMapper = new PropertyMapper<CustomCollectionView, CustomCollectionViewRenderer>(Mapper)
        {
            [nameof(CustomCollectionView.ItemSizingStrategy)] = MapSizeng,

        };

        

        public CustomCollectionViewRenderer() : base((PropertyMapper)PropertyMapper)
        {
        }

        private static void MapSizeng(CustomCollectionViewRenderer renderer, CustomCollectionView view)
        {
            
        }

        protected override ItemsViewController<ReorderableItemsView> CreateController(ReorderableItemsView itemsView, ItemsViewLayout layout)
        {
            // itemsView.SizeChanged += ItemsView_SizeChanged;

            return new CustomItemsViewController(itemsView, layout);
        }

    }


    
}

