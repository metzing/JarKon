using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Jarkon.ViewModel;
using Jarkon;



namespace JarKon.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CardsPage : ContentPage
    {
        public class SimpleObject
        {
            public string TextValue
            { get; set; }
            public string DataValue
            { get; set; }
        }

        public CardsPage()
        {
            InitializeComponent();
            MainOne.DataSource = GetSampleData();
            MainOne.DataBind();

        }

        void OnListItemClicked(object o, ItemTappedEventArgs e)
        {

            var vListItem = e.Item as SimpleObject;
            var vMessage = "You Clicked on " + vListItem.TextValue + " With Value " + vListItem.DataValue;
            DisplayAlert("Message", vMessage, "Ok");

        }

        public List<AccordionSource> GetSampleData()
        {
            var vResult = new List<AccordionSource>();


            #region StackLayout
            for (int i = 0; i < 16; i++)
            {
                var vViewLayout = new StackLayout()
                {
                    Children = {
                    new Label { Text = "Km óra állás: 408875 KM", TextColor = Color.Black },
                    new Label { Text = "Külső akku. fesz: 13V",TextColor = Color.Black },
                    new Label { Text = "Belső akku. fesz: 4V",TextColor = Color.Black }
                }
                };
                #endregion


                var vSecond = new AccordionSource
                    ()
                {
                    HeaderImageSource = "Icon.png",
                    HeaderTextColor = Color.White,
                    HeaderBackGroundColor = Color.Black,
                    ContentItems = vViewLayout
                };

                vResult.Add(vSecond);
            }

            return vResult;
        }
    }
}
