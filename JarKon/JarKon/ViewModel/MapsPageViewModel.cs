using Jarkon.ViewModel;
using JarKon.Core;
using JarKon.Model;
using JarKon.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace JarKon.ViewModel
{
    public static class MapsPageViewModel
    {
        private static CustomMap map { get { return (App.Current as App).MapsPage.Map; } }
        public static void LoadPins()
        {
            map.Pins.Clear();

            foreach (var vehicle in Provider.Instance.Vehicles)
            {
                CustomPin newPin = new CustomPin();
                VehicleState state = Provider.Instance.VehicleStates.Find(s => s.vehicleId == vehicle.vehicleId);
                newPin.Vehicle = vehicle;
                newPin.Pin.Position = new Xamarin.Forms.Maps.Position(state.position.lat, state.position.lng);
                newPin.Pin.Label = vehicle.plateNumber;
                map.Pins.Add(newPin);
            }
        }

        internal static void OnDataRefreshed()
        {
            LoadPins();
        }

        public static void OnUserLoaded()
        {
            var pos = Provider.Instance.CurrentUser.settings.generalViewSettings.defaultLocation.position;
            (App.Current as App).MapsPage.Map.MoveToRegion
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
    }
}

