﻿using JarKon.Core;
using JarKon.View.CardDetails;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.Specialized;

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

        public void LoadCards()
        {
            Device.BeginInvokeOnMainThread(
                () =>
                {
                    CardList.BuildCards();
                });
        }
    }
}
