using JarKon.Model;
using JarKon.Service;
using JarKon.View;
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
        private static Provider instance;
        /// <summary>
        /// Singleton
        /// </summary>
        public static Provider Instance
        {
            get
            {
                return instance ?? (instance = new Provider());
            }
        }
        public int ScreenHeight { get; set; }
        public int ScreenWidth { get; set; }

        public MapPage MapsPage { get; set; }
        public CardsPage CardsPage { get; set; }
        public ParkingPage ParkingPage { get; set; }

        /// <summary>
        /// List of the tracked Vehicles
        /// </summary>
        public List<Vehicle> Vehicles { get; private set; }

        /// <summary>
        /// List of the tracked Vehicles' states
        /// </summary>
        public List<VehicleState> VehicleStates { get; private set; }

        /// <summary>
        /// List of the parking zones from the API
        /// </summary>
        public List<Zone> ParkingZones { get; private set; }

        private User currentUser;
        /// <summary>
        /// The current (logged in) user
        /// </summary>
        public User CurrentUser
        {
            get
            {
                return currentUser;
            }

            set
            {
                currentUser = value;
                if (value != null) (App.Current as App).FireUserLoaded();
            }
        }

        private Provider()
        {
            Vehicles = new List<Vehicle>();
            VehicleStates = new List<VehicleState>();
            ParkingZones = new List<Zone>();
            CurrentUser = null;

            //Create a timer that calls the RefreshVehicles method every ten seconds
            var timer = new System.Threading.Timer(
                (e) =>
                {
                    RefreshVehicles();
                },
                null,
                2000,
                10000);
        }

        /// <summary>
        /// Refreshes the data displayed from the server
        /// </summary>
        public async void RefreshVehicles()
        {
            if (CurrentUser == null) return;

            await GetVehiclesAsync();
            await GetVehicleStatesAsync();
            (App.Current as App).FireDataChanged();
        }

        /// <summary>
        /// Gets the states of the tracked vehicles from the server
        /// </summary>
        /// <returns>List of the states of the tracked vehicles</returns>
        private async Task GetVehicleStatesAsync()
        {
            List<VehicleState> vehicleStates = new List<VehicleState>();
            VehicleService service = new VehicleService();

            var response = await service.GetVehicleStates(new GeneralRequest
            {
                userId = CurrentUser.userId
            });

            lock (VehicleStates)
            {
                VehicleStates.Clear();
                VehicleStates.AddRange(response.states);
            }
        }

        /// <summary>
        /// Gets the tracked vehicles from the server
        /// </summary>
        /// <returns>List of the tracked vehicles</returns>
        private async Task GetVehiclesAsync()
        {
            List<Vehicle> vehicles = new List<Vehicle>();
            VehicleService service = new VehicleService();

            var response = await service.GetVehicles(new GeneralRequest
            {
                userId = CurrentUser.userId
            });

            lock (Vehicles)
            {
                Vehicles.Clear();
                Vehicles.AddRange(response.vehicles);
            }
        }
    }



    /// <summary>
    /// Class for storing settings of simple type
    /// </summary>
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

        /// <summary>
        /// Token for authentication for the server
        /// </summary>
        public static string LoginToken
        {
            get { return AppSettings.GetValueOrDefault(loginTokenKey, loginTokenDefault); }
            set { AppSettings.AddOrUpdateValue(loginTokenKey, value); }
        }
    }
}
