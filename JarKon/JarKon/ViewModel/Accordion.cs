using System;
using Xamarin.Forms;
using System.Collections.Generic;

namespace Jarkon.ViewModel
{
    public class Accordion : ContentView
    {
        #region Private Variables
        List<AccordionSource> mDataSource;
        bool mFirstExpaned = false;
        StackLayout mMainLayout;
        #endregion

        public Accordion()
        {
            var mMainLayout = new StackLayout();
            Content = mMainLayout;
            DataBind();
        }

        public Accordion(List<AccordionSource> aSource)
        {
            mDataSource = aSource;
            DataBind();
        }

        #region Properties
        public List<AccordionSource> DataSource
        {
            get { return mDataSource; }
            set { mDataSource = value; }
        }
        public bool FirstExpaned
        {
            get { return mFirstExpaned; }
            set { mFirstExpaned = value; }
        }
        #endregion

        public void DataBind()
        {
           
            var vMainLayout = new StackLayout();
            vMainLayout.HeightRequest = 0;
            if (mDataSource != null)
            {
                foreach (var vSingleItem in mDataSource)
                {
                    
          
                    vMainLayout.HeightRequest += 300;

                    // var vTopRight = new AccordionTopRightView();
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

                    };



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

                    var vAccordionContent = new ContentView()
                    {
                        Content = vSingleItem.ContentItems,
                        IsVisible = false
                    };


                    vHeaderButton.AssosiatedContent = vAccordionContent;

                    /*vMainLayout.Children.Add(vHeaderViewLayout);
                    vMainLayout.Children.Add(item);
                    vMainLayout.Children.Add(item2);
                    vMainLayout.Children.Add(vHeaderButton);
                    vMainLayout.Children.Add(vAccordionContent);*/

                    StackLayout content = new StackLayout
                    {
                        Orientation = StackOrientation.Vertical,
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        BackgroundColor = Color.White,
                        Padding=5,
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
                    

                }
            }
            mMainLayout = vMainLayout;
            Content = mMainLayout;
        }

        void OnAccordionButtonClicked(object sender, EventArgs args)
        {
            /*foreach (var vChildItem in mMainLayout.Children) {
				if (vChildItem.GetType() == typeof(ContentView)) vChildItem.IsVisible = false;
				if (vChildItem.GetType () == typeof(AccordionButton)) {
					var vButton = (AccordionButton)vChildItem;
					vButton.Expand = false;
                }*/

            //}
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
            Children.Add(ArrowImage = new Image {
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
       
        public AccordionCardView (string textTop, string textBottom)
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

    public class CardText
    {
        public string top { get; set; }
        public string bottom { get; set; }
    }

    public class AccordionSource
    {
        public string HeaderImageSource { get; set; }
        public Color HeaderTextColor { get; set; }
        public Color HeaderBackGroundColor { get; set; }
        public View ContentItems { get; set; }
        public CardText[] CardTextArray{ get; set; }
        
    }
}