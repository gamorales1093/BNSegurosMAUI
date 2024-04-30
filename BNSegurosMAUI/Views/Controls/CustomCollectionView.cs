/*
 * READ ME:
 * 
 * This class is being added because there is a documented bug on Xamarin.Forms iOS (pre v5.0.0) that messes up
 * with the CollectionView cells' height.
 * 
 * https://github.com/xamarin/Xamarin.Forms/issues/8583
 * 
 * Solution is being taken from this post: https://github.com/xamarin/Xamarin.Forms/issues/10842#issuecomment-664670915
 * 
 * Make sure to remove CustomCollectionViewRenderer.cs, CustomItemsViewController.cs, CustomItemsViewDelegator.cs when there is
 * an available fix and the version of Xamarin.Forms on the project is updated.
 * 
 */

using System;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace BNSegurosMAUI.Views.Controls
{
    public class CustomCollectionView : CollectionView
    {
    }
}
