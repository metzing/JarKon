using JarKon.Droid;
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

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace JarKon.Droid
{
    class CustomMapRenderer : MapRenderer, GoogleMap.IInfoWindowAdapter, IOnMapReadyCallback
    {
        GoogleMap map;
        List<CustomPin> customPins;
        bool isDrawn;

        public Android.Views.View GetInfoContents(Marker marker)
        {
            var inflater = Android.App.Application.Context.GetSystemService("layout_inflater") as Android.Views.LayoutInflater;
            if (inflater != null)
            {
                Android.Views.View view;

                var customPin = GetCustomPin(marker);
                if (customPin == null)
                {
                    throw new Exception("Custom pin not found");
                }

                //TODO inflate real view instead
                view = inflater.Inflate(Resource.Layout.TempMapLayout, null);

                var textView = view.FindViewById<TextView>(Resource.Id.temp_map_textview);

                Vehicle vehicle = Provider.Instance.Vehicles.Find(v => v.plateNumber == marker.Title);

                if (textView != null)
                    textView.Text = Provider.Instance.VehicleStates.Find(vs => vs.vehicleId == vehicle.vehicleId).driver;

                return view;
            }
            return null;
        }

        private object GetCustomPin(Marker marker)
        {
            return customPins.Find(p => marker.Title == p.Pin.Label);
        }

        public Android.Views.View GetInfoWindow(Marker marker)
        {
            return null;
        }

        private void OnInfoWindowClick(object sender, GoogleMap.InfoWindowClickEventArgs e)
        {
            //TODO swap between collapsed and uncollapsed
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            map = googleMap;
            map.InfoWindowClick += OnInfoWindowClick;
            map.SetInfoWindowAdapter(this);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                map.InfoWindowClick -= OnInfoWindowClick;
            }

            if (e.NewElement != null)
            {
                var formsMap = (CustomMap)e.NewElement;
                customPins = formsMap.CustomPins;
                ((MapView)Control).GetMapAsync(this);
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName.Equals("VisibleRegion") && !isDrawn)
            {
                map.Clear();

                foreach (var pin in customPins)
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
            return BitmapDescriptorFactory.FromAsset("vehicle_bubble_13_green.png");
        }
    }
}