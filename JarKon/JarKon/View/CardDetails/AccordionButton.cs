using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace JarKon.View.CardDetails
{
    public class AccordionButton : Button
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
            BackgroundColor = Color.White;
            HorizontalOptions = LayoutOptions.FillAndExpand;
            VerticalOptions = LayoutOptions.CenterAndExpand;
            Image = "expand_arrow.png";
            

            Clicked += (s, e) =>
            {
                var vSenderButton = (AccordionButton)s;

                if (vSenderButton.Expand)
                {
                    vSenderButton.Expand = false;
                    vSenderButton.AssosiatedContent.IsVisible = false;
                    Image = "expand_arrow.png";

                }
                else
                {
                    vSenderButton.Expand = true;
                    vSenderButton.AssosiatedContent.IsVisible = true;
                    Image = "collapse_arrow.png";
                }

            };
        }

    }
}
