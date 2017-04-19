using GalaSoft.MvvmLight.Command;
using JarKon.View;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Text;

namespace JarKon.ViewModel
{
    class LoginViewModel
    {
        private static LoginViewModel instance = null;
        public static LoginViewModel Instance
        {
            get
            {
                return instance ?? (instance = new LoginViewModel());
            }
        }

        public RelayCommand LoginCommand;
        public string UserName { get; set; }
        public string Password { get; set; }

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(Login);
            UserName = "mobilTest";
            Password = "MobilTest123";
        }

        private void Login()
        {
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
}
