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
    class MapsPageViewModel
    {
        private MapsPage mapsPage = ((MainPage)App.Current.MainPage).MapsPage;
        private Pin previousPin;
        private Provider Provider { get { return ((App)App.Current).provider; } }

        public void LoadPins()
        {
            Map map = mapsPage.Map;

            foreach (var vehicle in Provider.Vehicles)
            {
                Pin newPin = new Pin();
                VehicleState state = Provider.VehicleStates.Find(s => s.vehicleId == vehicle.vehicleId);
                newPin.Position = new Xamarin.Forms.Maps.Position(state.position.lat, state.position.lng);
                newPin.Label = vehicle.plateNumber;
                map.Pins.Add(newPin);

            }
        }
    }
}

