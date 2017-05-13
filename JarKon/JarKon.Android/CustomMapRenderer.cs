/*using JarKon.Droid;
using JarKon.View;
using Xamarin.Forms;
using Xamarin.Forms.Maps.Android;
using Android.Gms.Maps;
using System.Collections.Generic;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Platform.Android;
using System;
using Android.Gms.Maps.Model;
using Android.Views;
using System.ComponentModel;
using Android.Widget;
using JarKon.Model;
using JarKon.Core;
using Android;
using Android.Support.V4.App;
using Android.Content.PM;
using JarKon.ViewModel;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace JarKon.Droid
{
    class CustomMapRenderer : MapRenderer, GoogleMap.IInfoWindowAdapter, IOnMapReadyCallback
    {
        GoogleMap map;
        List<CustomPin> customPins;

        public Android.Views.View GetInfoContents(Marker marker)
        {
            MapPage.ShowCardPopup(GetCustomPin(marker), new EventArgs());
            return null;
        }


        public class CardText
        {
            public string top { get; set; }
            public string bottom { get; set; }
        }


        private CustomPin GetCustomPin(Marker marker)
        {
            return customPins.Find(p => marker.Title == p.Pin.Label);
        }

        public Android.Views.View GetInfoWindow(Marker marker)
        {
            return null;
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            map = googleMap;
            map.SetInfoWindowAdapter(this);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
            }

            if (e.NewElement != null)
            {
                var formsMap = (CustomMap)e.NewElement;
                Control.GetMapAsync(this);
                customPins = formsMap.Pins;
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (map == null) return;

            if (e.PropertyName.Equals("VisibleRegion"))
            {
                map.Clear();

                foreach (var pin in customPins.ToArray())
                {
                    var marker = new MarkerOptions();
                    marker.SetPosition(new LatLng(pin.Pin.Position.Latitude, pin.Pin.Position.Longitude));
                    marker.SetTitle(pin.Pin.Label);
                    marker.SetIcon(GetIcon(pin));

                    map.AddMarker(marker);
                }
            }
        }

        private BitmapDescriptor GetIcon(CustomPin pin)
        {
            string iconName = "vehicle_bubble_13_";

            var vehicleState = Provider.Instance.VehicleStates.Find(vs => vs.vehicleId == pin.Vehicle.vehicleId);

            if (!vehicleState.ignition)
            {
                iconName += "blue";
            }
            else if (vehicleState.speed < 5)
            {
                iconName += "orange";
            }
            else
            {
                iconName += "green";
            }

            iconName += ".png";
            return BitmapDescriptorFactory.FromAsset(iconName);
        }
    }
}*/