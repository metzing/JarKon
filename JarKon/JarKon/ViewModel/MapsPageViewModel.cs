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
        private static MapsPage mapsPage = ((MainPage)App.Current.MainPage).MapsPage;
        private static Provider Provider { get { return ((App)App.Current).provider; } }

        public static void LoadPins()
        {
            CustomMap map = mapsPage.Map;

            foreach (var vehicle in Provider.Vehicles)
            {
                CustomPin newPin = new CustomPin();
                VehicleState state = Provider.VehicleStates.Find(s => s.vehicleId == vehicle.vehicleId);
                newPin.Pin.Position = new Xamarin.Forms.Maps.Position(state.position.lat, state.position.lng);
                newPin.Pin.Label = vehicle.plateNumber;
                map.CustomPins.Add(newPin);
            }
        }

        internal static void OnDataRefreshed()
        {
            LoadPins();
        }
    }
}

