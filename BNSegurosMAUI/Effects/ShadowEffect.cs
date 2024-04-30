using System;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace BNSegurosMAUI.Effects
{
    public class ShadowEffect : RoutingEffect
    {
        public Color Color { get; set; }
        public float Radius { get; set; }
        public float DistanceX { get; set; }
        public float DistanceY { get; set; }

        public ShadowEffect() : base("BNSegurosMAUI.SimpleShadowEffect")
        {
        }
    }
}
