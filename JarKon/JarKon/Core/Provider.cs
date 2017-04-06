using JarKon.Model;
using JarKon.Service;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace JarKon.Core
{
    public class Provider
    {
        private List<Vehicle> vehicles;

        public List<Vehicle> Vehicles
        {
            get { return vehicles; }
            set
            {
                vehicles = value;
                (App.Current as App).OnDataChanged();
            }
        }

        private List<VehicleState> vehicleStates;

        public List<VehicleState> VehicleStates
        {
            get { return vehicleStates; }
            set
            {
                vehicleStates = value;
                (App.Current as App).OnDataChanged();
            }
        }

        public User CurrentUser { get; set; }

        public Provider()
        {
            vehicles = new List<Vehicle>();
            vehicleStates = new List<VehicleState>();
        }

        public async void LoadFromCloud()
        {
            Vehicles = await GetVehiclesAsync();
            VehicleStates = await GetVehicleStatesAsync();
        }

        private async Task<List<VehicleState>> GetVehicleStatesAsync()
        {

            List<VehicleState> vehicleStates = new List<VehicleState>();
            VehicleService service = new VehicleService();

            var response = await service.GetVehicleStates(new GeneralRequest
            {
                userId = CurrentUser.userId
            });

            vehicleStates.AddRange(response.states);

            return vehicleStates;
        }

        private async Task<List<Vehicle>> GetVehiclesAsync()
        {
            List<Vehicle> vehicles = new List<Vehicle>();
            VehicleService service = new VehicleService();

            var response = await service.GetVehicles(new GeneralRequest
            {
                userId = CurrentUser.userId
            });

            vehicles.AddRange(response.vehicles);

            return vehicles;
        }

        public async Task Login()
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
#if __ANDROID__
                        clientType = "ANDROID",
#elif __IOS__
                        clientType = "IOS",
#endif
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
                CurrentUser = response.user;

                LoadFromCloud();
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
