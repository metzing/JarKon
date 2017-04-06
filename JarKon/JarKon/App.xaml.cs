using JarKon.Core;
using JarKon.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace JarKon
{
    public partial class App : Application
    {
        public Provider provider { get; private set; }
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
            provider = new Provider();
            DataRefreshed += MapsPageViewModel.OnDataRefreshed;

            provider.Login();
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
