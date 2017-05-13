using GalaSoft.MvvmLight.Command;
using JarKon.Model;
using JarKon.Service;
using JarKon.View;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace JarKon.Core
{
    class LoginManager
    {
        private static LoginManager instance;
        public static LoginManager Instance
        {
            get { return instance ?? (instance = new LoginManager()); }
        }

        public RelayCommand LoginCommand { get; set; }
        public RelayCommand LogoutCommand { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }

        private LoginManager()
        {
            LoginCommand = new RelayCommand(() => Login(UserName, Password));
            LogoutCommand = new RelayCommand(() => LogOut());

            UserName = "mobilTest";
            Password = "MobilTest123";
        }

        public async Task OnStartAsync()
        {
            if (Settings.LoginToken == "")
            {
                (App.Current as App).MainPage.Navigation.PushPopupAsync(new LoginPopUp());
            }
            else
            {
                Provider.Instance.CurrentUser = await Login(Settings.LoginToken);
            }
        }

        public async Task Login(string username, string password)
        {
            try
            {
                var service = new VehicleService();
                var response = await service.Login(new LoginRequest
                {
                    username = username,
                    password = password,
#if __ANDROID__
                    clientType = "ANDROID",
#elif __IOS__
                    clientType = "IOS",
#endif
                    deviceType = "",
                    deviceId = "test"
                });
                Provider.Instance.CurrentUser = response.user;
                Settings.LoginToken = response.token;
                (App.Current as App).MainPage.Navigation.PopPopupAsync();
            }
            catch (Exception e)
            {
                Settings.LoginToken = "";
                (App.Current as App).DisplayException(e);
            }
        }

        public async Task<User> Login(string token)
        {
            try
            {
                var service = new VehicleService();
                var response = await service.LoginWithToken(new RenewLoginRequest
                {
                    token = token
                });
                Settings.LoginToken = response.token;
                return response.user;
            }
            catch (Exception e)
            {
                Settings.LoginToken = "";
                (App.Current as App).DisplayException(e);
                return null;
            }
        }

        public void LogOut()
        {
            Settings.LoginToken = "";
            Provider.Instance.CurrentUser = null;
        }
    }

    public class LoginPopUp : PopupPage
    {
        public LoginPopUp()
        {
            CloseWhenBackgroundIsClicked = false;

            Content = new LoginView();
        }
    }
    class LogoutCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            LoginManager.Instance.LogOut();
            (App.Current as App).MainPage.Navigation.PushPopupAsync(new LoginPopUp());
        }
    }
}