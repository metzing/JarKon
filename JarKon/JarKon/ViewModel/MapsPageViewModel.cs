using JarKon.Model;
using JarKon.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms.GoogleMaps;

namespace JarKon.ViewModel
{
    class MapsPageViewModel
    {
        public ObservableCollection<VehicleState> vehicles = CardsViewModel.GetDummyData();

        private MapsPage mapsPage = ((MainPage)App.Current.MainPage).MapsPage;

        public void LoadPins()
        {
            Map map = mapsPage.Map;

            foreach (var vehicle in vehicles)
            {
                Pin newPin = new Pin();
                newPin.Position = new Xamarin.Forms.GoogleMaps.Position(vehicle.position.lat, vehicle.position.lng);
                newPin.Label = vehicle.driver;
                map.Pins.Add(newPin);
            }
        }
    }
}
