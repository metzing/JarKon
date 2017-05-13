using JarKon.Core;
using JarKon.View;
using JarKon.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Windows.Input;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;

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
                Text = "Kijelentkezés",
                Command = new LogoutCommand()
            });
        }

        private void IOSInit()
        {
            MainPage = new MainPage();
        }
        protected override void OnStart()
        {
            // Handle when your app starts
            UserLoaded += ParkingViewModel.OnUserLoggedIn;
            UserLoaded += MapViewModel.OnUserLoggedIn;
            DataChanged += MapViewModel.OnDataRefreshed;
            DataChanged += CardViewModel.OnDataRefreshed;

            LoginManager.Instance.OnStartAsync();
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
