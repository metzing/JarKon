using JarKon.Core;
using JarKon.Model;
using JarKon.View;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
namespace JarKon.ViewModel
{
    public class MapViewModel : BindableObject
    {
        private static MapViewModel instance;
        public static MapViewModel Instance
        {
            get { return instance ?? (instance = new MapViewModel()); }
        }
        public static BindableProperty ShouldShowUserProperty =
            BindableProperty.Create(nameof(ShouldShowUser), typeof(bool), typeof(MapViewModel), false);
        public bool ShouldShowUser
        {
            get { return (bool)GetValue(ShouldShowUserProperty); }
            set { SetValue(ShouldShowUserProperty, value); }
        }
        public void LoadPins()
        {
            var Map = Provider.Instance.MapsPage.Map;
            Map.Pins.Clear();
            foreach (var vehicle in Provider.Instance.Vehicles.ToArray())
            {
                CustomPin newPin = new CustomPin();
                VehicleState state = Provider.Instance.VehicleStates.Find(s => s.vehicleId == vehicle.vehicleId);
                newPin.Vehicle = vehicle;
                newPin.Pin.Position = new Xamarin.Forms.Maps.Position(state.position.lat, state.position.lng);
                newPin.Pin.Label = vehicle.plateNumber;
                newPin.Pin.Clicked += MapPage.ShowCardPopup;
                Map.Pins.Add(newPin);
            }
        }
        internal static void OnDataRefreshed()
        {
            Instance.LoadPins();
        }
        public static void OnUserLoggedIn()
        {
            var pos = Provider.Instance.CurrentUser.settings.generalViewSettings.defaultLocation.position;
            Provider.Instance.MapsPage.Map.MoveToRegion
            (
                MapSpan.FromCenterAndRadius
                (
                    new Xamarin.Forms.Maps.Position
                    (
                        pos.lat,
                        pos.lng
                    ),
                    Distance.FromKilometers(Provider.Instance.CurrentUser.settings.generalViewSettings.defaultLocation.zoom)
                )
            );
        }
        internal void DisableUserLocation()
        {
            ShouldShowUser = false;
        }
        internal void EnableUserLocation()
        {
            ShouldShowUser = true;
        }
    }
    public class CardPopup : PopupPage
    {
        public CardPopup(Vehicle vehicle)
        {
            CloseWhenBackgroundIsClicked = true;
            Content = new ContentView
            {
                Padding = new Thickness(5, Provider.Instance.ScreenHeight / 4),
                Content = new ScrollView
                {
                    Content = CardsViewModel.Instance.GetCardForPopup(vehicle)
                }
            };
        }
    }
}