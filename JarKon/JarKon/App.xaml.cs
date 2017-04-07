﻿using JarKon.Core;
using JarKon.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace JarKon
{
    public partial class App : Application
    {
        public delegate void DataRefreshedDelegate();
        public event DataRefreshedDelegate DataRefreshed;

        public App()
        {
            InitializeComponent();

            MainPage = new JarKon.View.MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            Provider.Instance.OnStartAsync();

            DataRefreshed += MapsPageViewModel.OnDataRefreshed;
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        internal void OnDataChanged()
        {
            DataRefreshed();
        }

        public void DisplayException(Exception e)
        {
            Current.MainPage.DisplayAlert("Exception:", e.Message, "OK");
        }
    }
}
