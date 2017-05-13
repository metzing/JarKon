﻿using JarKon.Core;
using JarKon.Model;
using JarKon.View;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
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
            lock (Provider.Instance.Vehicles) lock (Provider.Instance.VehicleStates)
                {
                    foreach (var vehicle in Provider.Instance.Vehicles.ToArray())
                    {
                        Pin newPin = new Pin();
                        VehicleState state = Provider.Instance.VehicleStates.Find(s => s.vehicleId == vehicle.vehicleId);
                        newPin.Label = vehicle.plateNumber;
                        newPin.Position = new Xamarin.Forms.GoogleMaps.Position(state.position.lat,state.position.lng);
                        Map.Pins.Add(newPin);
                    }
                }
        }
        internal static void OnDataRefreshed()
        {
            Device.BeginInvokeOnMainThread(() =>
                Instance.LoadPins()
            );
        }
        public static void OnUserLoggedIn()
        {
            var pos = Provider.Instance.CurrentUser.settings.generalViewSettings.defaultLocation.position;
            Provider.Instance.MapsPage.Map.MoveToRegion
            (
                MapSpan.FromCenterAndRadius
                (
                    new Xamarin.Forms.GoogleMaps.Position
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
                    Content = CardViewModel.Instance.GetCardForPopup(vehicle)
                }
            };
        }
    }
}