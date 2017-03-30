using Jarkon.ViewModel;
using JarKon.Core;
using JarKon.Model;
using JarKon.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

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
                newPin.Position = new Xamarin.Forms.GoogleMaps.Position(state.position.lat, state.position.lng);
                newPin.Label = vehicle.plateNumber;
                newPin.Icon = GetIconFor(vehicle);
                map.Pins.Add(newPin);
            }
            map.PinClicked += OnPinClicked;
        }



        private BitmapDescriptor GetIconFor(Vehicle vehicle)
        {
            return BitmapDescriptorFactory.FromBundle("vehicle_bubble_13_green.png");
        }

        private void OnPinClicked(object sender, PinClickedEventArgs e)
        {
            e.Pin.Icon = BitmapDescriptorFactory.FromView(new ContentView()
            {
                WidthRequest = 200,
                HeightRequest = 100,
                BackgroundColor = Color.White,
                Content = new Label
                {
                    Text = "Content",
                    TextColor = Color.Black
                }
            });

            if (previousPin != null)
                previousPin.Icon = GetIconFor(Provider.Vehicles.Find(v => v.plateNumber == previousPin.Label));
            previousPin = e.Pin;
        }
    }
}
