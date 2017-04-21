using JarKon.Core;
using JarKon.View.CardDetails;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.Specialized;
using System.Collections.Generic;
using JarKon.Model;
using JarKon.ViewModel;

namespace JarKon.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CardsPage : ContentPage
    {
        public Dictionary<int, Card> Cards;
        private StackLayout CardListView;
        public CardsPage()
        {
            InitializeComponent();
            Provider.Instance.CardsPage = this;
            CardListView = new StackLayout();
            Cards = new Dictionary<int, Card>();
        }

        public async void LoadCards()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                CardContainer.Content = CardListView;
                lock (CardViewModel.Instance.CardDataSource)
                {
                    foreach (var item in CardViewModel.Instance.CardDataSource)
                    {
                        if (!Cards.ContainsKey(item.VehicleID))
                        {
                            var card = new Card();
                            Cards.Add(item.VehicleID, card);
                            CardListView.Children.Add(card);
                        }

                        Card corresponding = Cards[item.VehicleID];

                        corresponding.SetData(item);
                    }
                }
            });
        }
    }
}
