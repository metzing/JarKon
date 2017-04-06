using JarKon.Model;
using JarKon.Service;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace JarKon.Core
{
    public class Provider
    {
        public List<Vehicle> Vehicles { get; private set; }
        public List<VehicleState> VehicleStates { get; private set; }
        public User CurrentUser { get; set; }

        public Provider()
        {
            Login();


            Vehicles = new List<Vehicle>();
            VehicleStates = new List<VehicleState>();
        }

        private async void Login()
        {
            try
            {
                VehicleService service = new VehicleService();
                LoginResponse response;
                if (Settings.LoginToken == "")
                {
                    //Log in using creditentials
                    //TODO input fields for this
                    response = await service.Login(new LoginRequest
                    {
                        username = "mobilTest",
                        password = "MobilTest123",
                        clientType = (Device.OS == TargetPlatform.iOS ? "IOS" : "ANDROID"),
                        deviceType = "",
                        deviceId = "test"
                    });
                }
                else
                {
                    response = await service.LoginWithToken(new RenewLoginRequest
                    {
                        token = Settings.LoginToken,

                    });
                }
                Settings.LoginToken = response.token;
            }
            catch (Exception e)
            {
                (App.Current as App).DisplayException(e);
            }
        }
    }

    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        private const string loginTokenKey = "login_token";
        private static readonly string loginTokenDefault = "";

        public static string LoginToken
        {
            get { return AppSettings.GetValueOrDefault(loginTokenKey, loginTokenDefault); }
            set { AppSettings.AddOrUpdateValue(loginTokenKey, value); }
        }
    }
}
