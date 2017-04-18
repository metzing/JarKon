using JarKon.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace JarKon.View.CardDetails
{
    public class CardListView : ContentView
    {
        bool cardsBuilt = false;
        public CardListView()
        {
            Content = new Label
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                Text = "Loading..."
            };

            CardsViewModel.Instance.CardDataSource.CollectionChanged += RefreshCards;
        }

        private void RefreshCards(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (cardsBuilt) return;

            Device.BeginInvokeOnMainThread(
                () =>
                {
                    BuildCards();
                });
        }

        private void BuildCards()
        {
            if (CardsViewModel.Instance.CardDataSource.Count == 3) cardsBuilt = true;

            var container = new StackLayout();

            foreach (var item in CardsViewModel.Instance.CardDataSource.ToList())
            {
                container.Children.Add(BuildCard(item));
            }
            Content = container;
        }

        public static Xamarin.Forms.View BuildCard(CardData data)
        {
            var Header = BuildHeader(data);
            var SelectedDetails = BuildSelectedDetails(data);

            var NotSelectedDetails = new ExpandedView(data.ExpandedTextList);
            NotSelectedDetails.IsVisible = false;

            var vHeaderButton = new AccordionButton();
            vHeaderButton.AssosiatedContent = NotSelectedDetails;


            StackLayout content = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.White,
                Padding = 5,
                Children =
                    {
                        Header,
                        SelectedDetails,
                        vHeaderButton,
                        NotSelectedDetails
                    }
            };

            return new Frame
            {
                Padding = 5,
                OutlineColor = Color.Gray,
                Content = content
            };
        }

        private static Xamarin.Forms.View BuildSelectedDetails(CardData vSingleItem)
        {
            var container = new StackLayout();
            for (int i = 0; i < 2; i++)
            {
                var line = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    BackgroundColor = Color.White,
                };
                for (int j = 0; j < 3; j++)
                {
                    line.Children.Add(new SelectedDetailView(vSingleItem.SelectedDetails[i * 3 + j], container.Width / 3 - 5));
                }
                container.Children.Add(line);
            }
            return container;
        }

        private static Xamarin.Forms.View BuildHeader(CardData vSingleItem)
        {
            return new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                BackgroundColor = Color.White,

                Children =
                {
                    new Image
                    {
                        Source = vSingleItem.HeaderImageSource,
                        VerticalOptions = LayoutOptions.StartAndExpand,
                        HorizontalOptions = LayoutOptions.StartAndExpand
                    },
                    new StackLayout
                    {
                        Orientation = StackOrientation.Vertical,
                        VerticalOptions = LayoutOptions.EndAndExpand,
                        Children =
                        {
                            new Label { Text = vSingleItem.PlateNumber,
                                TextColor = Color.Black,
                                FontSize = 24,
                                FontAttributes = FontAttributes.Bold,
                                HorizontalOptions = LayoutOptions.EndAndExpand,

                            },

                            new Label { Text = "2015.04.08 10:46",
                                TextColor = Color.Black,
                                HorizontalOptions = LayoutOptions.EndAndExpand,

                            },
                            new Label { Text = "Cím megjelenítése",
                                TextColor = Color.Black,
                                HorizontalOptions = LayoutOptions.EndAndExpand,

                            }
                        }
                    }
                }
            };
        }
    }
}


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

public class AccordionButton : StackLayout
{
    #region Private Variables
    bool mExpand = false;
    #endregion

    #region Properties
    public bool Expand
    {
        get { return mExpand; }
        set { mExpand = value; }
    }
    public ContentView AssosiatedContent
    { get; set; }
    public Image ArrowImage
    { get; set; }
    #endregion


    public AccordionButton()
    {
        Orientation = StackOrientation.Vertical;
        BackgroundColor = Color.White;
        HorizontalOptions = LayoutOptions.FillAndExpand;
        VerticalOptions = LayoutOptions.CenterAndExpand;
        Children.Add(ArrowImage = new Image
        {
            HeightRequest = 25,
            WidthRequest = 25,
            Source = "expand_arrow.png",
        });

        var tapGestureRecognizer = new TapGestureRecognizer();
        tapGestureRecognizer.Tapped += (s, e) =>
        {
            var vSenderButton = (AccordionButton)s;

            if (vSenderButton.Expand)
            {
                vSenderButton.Expand = false;
                vSenderButton.AssosiatedContent.IsVisible = false;
                ArrowImage.Source = "expand_arrow.png";

            }
            else
            {
                vSenderButton.Expand = true;
                vSenderButton.AssosiatedContent.IsVisible = true;
                ArrowImage.Source = "collapse_arrow.png";
            }

        };
        GestureRecognizers.Add(tapGestureRecognizer);

    }

}

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
