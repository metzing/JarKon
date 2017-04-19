using JarKon.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace JarKon.View.CardDetails
{
    public class ExpandedView : ContentView
    {
        public ExpandedView(List<DetailText> cardtexts)
        {
            var stack = new StackLayout();
            foreach (DetailText cardItem in cardtexts)
            {
                StackLayout entry = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    BackgroundColor = Color.White,
                    Children =
                {
                    new Label
                    {
                        Text = cardItem.top,
                        TextColor = Color.Black,
                        HorizontalOptions = LayoutOptions.StartAndExpand,
                        FontAttributes = FontAttributes.Bold
                    },
                    new Label
                    {
                        Text = cardItem.bottom,
                        TextColor = Color.Black,

                        HorizontalOptions = LayoutOptions.EndAndExpand,
                    }
                }
                };

                stack.Children.Add(entry);
            }
            Content = stack;
        }
    }
}
