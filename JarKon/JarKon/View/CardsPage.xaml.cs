using JarKon.Core;
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JarKon.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CardsPage : ContentPage
    {

        public CardsPage()
        {
            InitializeComponent();
            Provider.Instance.CardsPage = this;
        }

        public Accordion Accordion
        {
            get
            {
                return MainOne;
            }
        }
    }

    public class Accordion : ContentView
    {
        #region Private Variables
        bool mFirstExpaned = false;
        StackLayout mMainLayout;
        #endregion


        public new List<AccordionSource> mDataSource { get; set; }


        public Accordion()
        {
            var mMainLayout = new StackLayout();
            Content = mMainLayout;
            mDataSource = new List<AccordionSource>();
            //DataBind();
        }

        public Accordion(List<AccordionSource> aSource)
        {
            var mMainLayout = new StackLayout();
            Content = mMainLayout;
            mDataSource = aSource;
          //  DataBind();
        }


        public bool FirstExpaned
        {
            get { return mFirstExpaned; }
            set { mFirstExpaned = value; }
        }

        public void DataBind()
        {
              var vMainLayout = new StackLayout();
              vMainLayout.HeightRequest = 0;
              if (mDataSource != null)
              {
                  foreach (var vSingleItem in mDataSource)
                  {
                      vMainLayout.HeightRequest += 300;
                      var vHeaderViewLayout = new StackLayout()
                      {
                          Orientation = StackOrientation.Horizontal,
                          BackgroundColor = Color.White,

                          Children = {
                              new AccordionImage
                              {
                                  Source = vSingleItem.HeaderImageSource,
                                  VerticalOptions = LayoutOptions.StartAndExpand
                              },
                              new StackLayout
                              {
                                  Orientation = StackOrientation.Vertical,
                                  VerticalOptions = LayoutOptions.EndAndExpand,

                                         Children = {
                                            new Label { Text = "DEMO-2",
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

                      };//end header



                      var item = new StackLayout
                      {
                          Orientation = StackOrientation.Horizontal,
                          VerticalOptions = LayoutOptions.FillAndExpand,
                          HorizontalOptions = LayoutOptions.FillAndExpand,
                          BackgroundColor = Color.White,
                          Children =
                                {


                                    new AccordionCardView(vSingleItem.CardTextArray[0].top,vSingleItem.CardTextArray[0].bottom),
                                    new AccordionCardView(vSingleItem.CardTextArray[1].top,vSingleItem.CardTextArray[1].bottom),
                                    new AccordionCardView(vSingleItem.CardTextArray[2].top,vSingleItem.CardTextArray[2].bottom),
                                }


                      };

                      var item2 = new StackLayout
                      {
                          Orientation = StackOrientation.Horizontal,
                          VerticalOptions = LayoutOptions.FillAndExpand,
                          HorizontalOptions = LayoutOptions.FillAndExpand,
                          BackgroundColor = Color.White,
                          Children =
                                {

                                    new AccordionCardView(vSingleItem.CardTextArray[3].top,vSingleItem.CardTextArray[3].bottom),
                                    new AccordionCardView(vSingleItem.CardTextArray[4].top,vSingleItem.CardTextArray[4].bottom),
                                    new AccordionCardView(vSingleItem.CardTextArray[5].top,vSingleItem.CardTextArray[5].bottom),
                                }


                      };

                      var vHeaderButton = new AccordionButton();

                      var expanded = new ExpandedView(vSingleItem.ExpandedTextArray);

                      var vAccordionContent = new ContentView()
                      {
                          Content = expanded,
                          IsVisible = false
                      };


                      vHeaderButton.AssosiatedContent = vAccordionContent;


                      StackLayout content = new StackLayout
                      {
                          Orientation = StackOrientation.Vertical,
                          VerticalOptions = LayoutOptions.FillAndExpand,
                          HorizontalOptions = LayoutOptions.FillAndExpand,
                          BackgroundColor = Color.White,
                          Padding = 5,
                          Children =
                                {
                                    vHeaderViewLayout,
                                    item,
                                    item2,
                                    vHeaderButton,
                                    vAccordionContent
                                }
                      };

                      StackLayout frameGap = new StackLayout()
                      {
                          Orientation = StackOrientation.Vertical,
                          VerticalOptions = LayoutOptions.FillAndExpand,
                          HorizontalOptions = LayoutOptions.FillAndExpand,
                          BackgroundColor = Color.FromHex("dedede"),
                          Padding = 1,
                          Children =
                                {
                                    content
                                }

                      };

                      StackLayout frame = new StackLayout()
                      {
                          Orientation = StackOrientation.Vertical,
                          VerticalOptions = LayoutOptions.FillAndExpand,
                          HorizontalOptions = LayoutOptions.FillAndExpand,
                          BackgroundColor = Color.Gray,
                          Children =
                                {
                                    frameGap
                                }

                      };


                      vMainLayout.Children.Add(frame);


                  }//end foreach
              }//end if

              mMainLayout = vMainLayout;
              Content = mMainLayout;
          }//end databind



         void OnAccordionButtonClicked(object sender, EventArgs args)
         {

             var vSenderButton = (AccordionButton)sender;

             if (vSenderButton.Expand)
             {
                 vSenderButton.Expand = false;
                 vSenderButton.AssosiatedContent.IsVisible = false;

             }
             else
             {
                 vSenderButton.Expand = true;
                 vSenderButton.AssosiatedContent.IsVisible = true;
             }

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


        public class AccordionImage : Image
        {
            public AccordionImage()
            {
                HorizontalOptions = LayoutOptions.StartAndExpand;

            }

        }


        public class AccordionCardView : StackLayout
        {

            public AccordionCardView(string textTop, string textBottom)
            {
                Label labelTop = new Label
                {
                    Text = textTop,
                    TextColor = Color.Black,
                    HorizontalOptions = LayoutOptions.StartAndExpand

                };


                Label labelBottom = new Label
                {
                    Text = textBottom,
                    TextColor = Color.Black,
                    FontAttributes = FontAttributes.Bold,
                    HorizontalOptions = LayoutOptions.EndAndExpand,

                };

                Orientation = StackOrientation.Vertical;
                VerticalOptions = LayoutOptions.FillAndExpand;
                HorizontalOptions = LayoutOptions.FillAndExpand;
                BackgroundColor = Color.FromHex("f4f4f3");
                Padding = 5;
                Children.Add(labelTop);
                Children.Add(labelBottom);

            }
        }

        public class AccordionCardLabelTop : Label
        {

            public AccordionCardLabelTop(string textTop)
            {

                Text = textTop;
                TextColor = Color.Black;
                HorizontalOptions = LayoutOptions.StartAndExpand;
            }
        }

        public class AccordationCardLabelBottom : Label
        {
            public AccordationCardLabelBottom(string textBottom)
            {
                Text = textBottom;
                TextColor = Color.Black;
                FontAttributes = FontAttributes.Bold;
                HorizontalOptions = LayoutOptions.EndAndExpand;

            }
        }


        public class AccordionTopRightView : StackLayout
        {
            public Label label1;
            public Label label2;
            public Label label3;

            public AccordionTopRightView()
            {
                HorizontalOptions = LayoutOptions.EndAndExpand;

            }

        }

        public class ExpandedView : StackLayout
        {

            public ExpandedView(CardText[] cardtexts)
            {
                for (int i = 0; i < cardtexts.Length; i++)
                {

                    StackLayout stack = new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        BackgroundColor = Color.White
                    };

                    Label labelTop = new Label
                    {
                        Text = cardtexts[i].top,
                        TextColor = Color.Black,
                        HorizontalOptions = LayoutOptions.StartAndExpand

                    };


                    Label labelBottom = new Label
                    {
                        Text = cardtexts[i].bottom,
                        TextColor = Color.Black,
                        FontAttributes = FontAttributes.Bold,
                        HorizontalOptions = LayoutOptions.EndAndExpand,

                    };

                    stack.Children.Add(labelTop);
                    stack.Children.Add(labelBottom);
                    Children.Add(stack);

                }
            }
        }


        public class CardText
        {
            public string top { get; set; }
            public string bottom { get; set; }
        }

        public class AccordionSource
        {
            public string HeaderImageSource { get; set; }
            //public Color HeaderTextColor { get; set; }
            // public Color HeaderBackGroundColor { get; set; }
            // public Xamarin.Forms.View ContentItems { get; set; }
            public CardText[] CardTextArray { get; set; }
            public CardText[] ExpandedTextArray { get; set; }

        }


    }