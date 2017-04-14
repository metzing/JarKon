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
                view = inflater.Inflate(Resource.Layout.CardCollapsed, null);

                var imageView = view.FindViewById<ImageView>(Resource.Id.imageView);
                var textViewTop1 = view.FindViewById<TextView>(Resource.Id.textViewTop1);
                var textViewTop2 = view.FindViewById<TextView>(Resource.Id.textViewTop2);
                var textViewTop3 = view.FindViewById<TextView>(Resource.Id.textViewTop3);

                var textViewGridTop00 = view.FindViewById<TextView>(Resource.Id.textViewGridTop00);
                var textViewGridBottom00 = view.FindViewById<TextView>(Resource.Id.textViewGridBottom00);
                var textViewGridTop01 = view.FindViewById<TextView>(Resource.Id.textViewGridTop01);
                var textViewGridBottom01 = view.FindViewById<TextView>(Resource.Id.textViewGridBottom01);
                var textViewGridTop02 = view.FindViewById<TextView>(Resource.Id.textViewGridTop02);
                var textViewGridBottom02 = view.FindViewById<TextView>(Resource.Id.textViewGridBottom02);

                var textViewGridTop10 = view.FindViewById<TextView>(Resource.Id.textViewGridTop10);
                var textViewGridBottom10 = view.FindViewById<TextView>(Resource.Id.textViewGridBottom10);
                var textViewGridTop11 = view.FindViewById<TextView>(Resource.Id.textViewGridTop11);
                var textViewGridBottom11 = view.FindViewById<TextView>(Resource.Id.textViewGridBottom11);
                var textViewGridTop12 = view.FindViewById<TextView>(Resource.Id.textViewGridTop12);
                var textViewGridBottom12 = view.FindViewById<TextView>(Resource.Id.textViewGridBottom12);



                User currentUser = Provider.Instance.CurrentUser;
                Vehicle vehicle = Provider.Instance.Vehicles.Find(v => v.plateNumber == marker.Title);
                VehicleState vehicleState = Provider.Instance.VehicleStates.Find(vs => vs.vehicleId == vehicle.vehicleId);
                


                const int VEHICLE_DATA_TYPES_NUM = 6;
                VehicleDataType?[] vehicleDataTypes = new VehicleDataType?[VEHICLE_DATA_TYPES_NUM];
               /* for (int i = 0; i < VEHICLE_DATA_TYPES_NUM; i++)
                {
                    Array values = Enum.GetValues(typeof(VehicleDataType));
                    Random random = new Random();
                    VehicleDataType randomVehicleType = (VehicleDataType)values.GetValue(random.Next(values.Length));

                    vehicleDataTypes[i] = VehicleDataType.PLATE_NUMBER;
                }*/

                VehicleViewSettings[] settings = currentUser.settings.vehicleViewSettings;
                foreach(VehicleViewSettings vhSettings in settings)
                {
                    if(vhSettings.vehicleId == vehicle.vehicleId)
                    {
                        vehicleDataTypes = vhSettings.cellSet;
                    }
                }

                for(int i =0; i<VEHICLE_DATA_TYPES_NUM; i++)
                {
                    CardText cardText = new CardText();
                    try
                    {
                       cardText  = GetCardTextByType(vehicleDataTypes[i], vehicle, vehicleState);
                    }catch(NullReferenceException e)
                    {
                        NullReferenceException error = e; 
                        cardText.top = "";
                        cardText.bottom = "";
                    }

                    switch (i)
                    {
                        case 0:
                            textViewGridTop00.Text = cardText.top;
                            textViewGridBottom00.Text = cardText.bottom;
                            break;

                        case 1:
                            textViewGridTop01.Text = cardText.top;
                            textViewGridBottom01.Text = cardText.bottom;
                            break;

                        case 2:
                            textViewGridTop02.Text = cardText.top;
                            textViewGridBottom02.Text = cardText.bottom;
                            break;

                        case 3:
                            textViewGridTop10.Text = cardText.top;
                            textViewGridBottom10.Text = cardText.bottom;
                            break;
                        case 4:
                            textViewGridTop11.Text = cardText.top;
                            textViewGridBottom11.Text = cardText.bottom;
                            break;
                        case 5:
                            textViewGridTop12.Text = cardText.top;
                            textViewGridBottom12.Text = cardText.bottom;
                            break;
                       
                    } 
                }
              



                return view;
            }
            return null;
        }


        public class CardText
        {
            public string top { get; set; }
            public string bottom { get; set; }
        }


        private CardText GetCardTextByType(VehicleDataType? dataType, Vehicle vehicle, VehicleState vehicleState)
        {
            CardText cardText = new CardText();
            

            switch (dataType)
            {
                case VehicleDataType.PLATE_NUMBER:
                    cardText.top = "Plate:";
                    cardText.bottom = vehicle.plateNumber; 
                    break;

                case VehicleDataType.VEHICLE_TYPE:
                    cardText.top = "Type:";
                    cardText.bottom = vehicle.type;
                    break;

                case VehicleDataType.TIME:
                    cardText.top = "Time:";
                    cardText.bottom = vehicleState.time.ToString();  ///TODO epoch time vagy mi gyün?
                    break;

                case VehicleDataType.ADDRESS:
                    cardText.top = "Address:";
                    cardText.bottom = vehicleState.address;
                    break;

                case VehicleDataType.BUSINESS_TRIP:
                    cardText.top = "Businness trip:";
                    if (vehicleState.businessTrip)
                    {
                        cardText.bottom = "True";
                    }
                    else
                    {
                        cardText.bottom = "False";
                    }
                    break;

                case VehicleDataType.MAKE:
                    cardText.top = "Make:";
                    cardText.bottom = vehicle.make;
                    break;

                case VehicleDataType.LAT:
                    cardText.top = "Lat:";
                    cardText.bottom = vehicleState.position.lat.ToString();
                    break;

                case VehicleDataType.LNG:
                    cardText.top = "Lon:";
                    cardText.bottom = vehicleState.position.lng.ToString();
                    break;

                case VehicleDataType.SPEED:
                    cardText.top = "Speed:";
                    cardText.bottom = vehicleState.speed.ToString(); // TODO
                    break;

                case VehicleDataType.RPM:
                    cardText.top = "RPM:";
                    cardText.bottom = vehicleState.rpm.ToString();
                    break;

                case VehicleDataType.IGNITION:
                    cardText.top = "Ignition:";
                    cardText.bottom = vehicleState.ignition.ToString();
                    break;

                case VehicleDataType.MILEAGE_STATE:
                    cardText.top = "Mileage State:";
                    cardText.bottom = vehicleState.mileageState.ToString();
                    break;


                case VehicleDataType.SIGNAL:
                    cardText.top = "Signal:";
                    cardText.bottom = vehicleState.signal.ToString();
                    break;

                case VehicleDataType.EXT_BATT_VOLT:
                    cardText.top = "Ext. battery:";
                    cardText.bottom = vehicleState.extBattVolt.ToString();
                    break;

                case VehicleDataType.INT_BATT_VOLT:
                    cardText.top = "Int. bat volt:";
                    cardText.bottom = vehicleState.intBattVolt.ToString();
                    break;

                case VehicleDataType.FUEL_1:
                    cardText.top = "Fuel_1:";
                    cardText.bottom = vehicleState.fuel1.ToString();
                    break;

                case VehicleDataType.FUEL_2:
                    cardText.top = "Fuel_2:";
                    cardText.bottom = vehicleState.fuel2.ToString();
                    break;

                case VehicleDataType.SUM_BURNED_FUEL:
                    cardText.top = "Sum burned fuel:";
                    cardText.bottom = vehicleState.sumBurnedFuel.ToString();
                    break;

                case VehicleDataType.AXLE_NUM_SET:
                    cardText.top = "Axle:";
                    cardText.bottom = vehicleState.axleNumSet.ToString();
                    break;

                case VehicleDataType.DRIVER:
                    cardText.top = "Driver:";
                    cardText.bottom = vehicleState.driver.ToString();
                    break;

                default:
                    break;
       
            }

            return cardText;

        }

        private CustomPin GetCustomPin(Marker marker)
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
}