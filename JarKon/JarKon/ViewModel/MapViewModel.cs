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
    public static class MapViewModel
    {
        private static CustomMap Map { get { return Provider.Instance.MapsPage.Map; } }
        public static void LoadPins()
        {
            Map.Pins.Clear();

            foreach (var vehicle in Provider.Instance.Vehicles.ToArray())
            {
                CustomPin newPin = new CustomPin();
                VehicleState state = Provider.Instance.VehicleStates.Find(s => s.vehicleId == vehicle.vehicleId);
                newPin.Vehicle = vehicle;
                newPin.Pin.Position = new Xamarin.Forms.Maps.Position(state.position.lat, state.position.lng);
                newPin.Pin.Label = vehicle.plateNumber;
                Map.Pins.Add(newPin);
            }
        }

        internal static void OnDataRefreshed()
        {
            LoadPins();
        }

        public static void OnUserLoggedIn()
        {
            var pos = Provider.Instance.CurrentUser.settings.generalViewSettings.defaultLocation.position;
            Map.MoveToRegion
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

