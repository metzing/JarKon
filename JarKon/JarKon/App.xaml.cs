using JarKon.Core;
using JarKon.View;
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
        public delegate void EventDelegate();
        public event EventDelegate DataChanged;
        public event EventDelegate UserLoaded;

        

        public App()
        {
            InitializeComponent();
#if __ANDROID__
            AndroidInit();
#elif __IOS__
            IOSInit(); 
#endif
        }

        private void AndroidInit()
        {
            MainPage = new NavigationPage(new JarKon.View.MainPage());

            MainPage.ToolbarItems.Add(new ToolbarItem
            {
                Order = ToolbarItemOrder.Secondary,
                Text = "Menu Item 1"
            });
        }

        private void IOSInit()
        {
            MainPage = new MainPage();
        }
        protected override void OnStart()
        {
            // Handle when your app starts
            Provider.Instance.OnStart();
            
            UserLoaded += MapViewModel.OnUserLoggedIn;
            UserLoaded += ParkingViewModel.OnUserLoggedIn;
            DataChanged += MapViewModel.OnDataRefreshed;
            DataChanged += CardsViewModel.OnDataRefreshed;
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        public void FireDataChanged()
        {
            DataChanged();
        }

        public void FireUserLoaded()
        {
            UserLoaded();
        }

        public void DisplayException(Exception e)
        {
            Current.MainPage.DisplayAlert("Exception:", e.Message, "OK");
        }

        public static void DisplayAlert(string title, string message, string cancel)
        {
            Current.MainPage.DisplayAlert(title, message, cancel);
        }
    }
}
