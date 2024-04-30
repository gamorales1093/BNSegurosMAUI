using System;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace BNSegurosMAUI.Models
{
	public class CarouselItem
	{
        public string Image { get; set; }
        public int Id { get; set; }
        public Color Color { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}

