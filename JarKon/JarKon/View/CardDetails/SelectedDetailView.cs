using JarKon.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace JarKon.View.CardDetails
{
    public class SelectedDetailView : StackLayout
    {
        public SelectedDetailView(DetailText detail, double width)
        {
            Label labelTop = new Label
            {
                Text = detail.top,
                TextColor = Color.Black,
                HorizontalOptions = LayoutOptions.Start

            };


            Label labelBottom = new Label
            {
                Text = detail.bottom,
                TextColor = Color.Black,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.End,

            };

            Orientation = StackOrientation.Vertical;
            VerticalOptions = LayoutOptions.FillAndExpand;
            HorizontalOptions = LayoutOptions.FillAndExpand;
            WidthRequest = width;
            BackgroundColor = Color.FromHex("f4f4f3");
            Padding = 5;
            Children.Add(labelTop);
            Children.Add(labelBottom);

        }
    }
}
