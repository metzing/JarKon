using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Jarkon.ViewModel;
using Jarkon;
using JarKon.Model;
using JarKon.Core;

namespace JarKon.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CardsPage : ContentPage
    {
        public class SimpleObject
        {
            public string TextValue
            { get; set; }
            public string DataValue
            { get; set; }
        }

        public CardsPage()
        {
            InitializeComponent();
            Provider.Instance.CardsPage = this;
        }

        void OnListItemClicked(object o, ItemTappedEventArgs e)
        {

            var vListItem = e.Item as SimpleObject;
            var vMessage = "You Clicked on " + vListItem.TextValue + " With Value " + vListItem.DataValue;
            DisplayAlert("Message", vMessage, "Ok");

        }

        public List<AccordionSource> GetSampleData()
        {
            var vResult = new List<AccordionSource>();

            User currentUser = Provider.Instance.CurrentUser;
            List<Vehicle> vehicles = Provider.Instance.Vehicles;
            int VEHICLE_DATA_TYPES_NUM = 6;
            CardText[] cardTextList = new CardText[VEHICLE_DATA_TYPES_NUM];
        
            foreach (Vehicle vehicle in vehicles)
            {
                VehicleState vehicleState = Provider.Instance.VehicleStates.Find(vs => vs.vehicleId == vehicle.vehicleId);
               
                VehicleDataType?[] vehicleDataTypes = new VehicleDataType?[VEHICLE_DATA_TYPES_NUM];
                
                VehicleViewSettings[] settings = currentUser.settings.vehicleViewSettings;
                foreach (VehicleViewSettings vhSettings in settings)
                {
                    if (vhSettings.vehicleId == vehicle.vehicleId)
                    {
                        vehicleDataTypes = vhSettings.cellSet;
                    }
                }

                for (int i = 0; i < VEHICLE_DATA_TYPES_NUM; i++)
                {

                    CardText cardText = new CardText();

                    try
                    {
                        cardText = GetCardTextByType(vehicleDataTypes[i], vehicle, vehicleState);
                    }
                    catch (NullReferenceException e)
                    {
                        cardText.top = "";
                        cardText.bottom = "";
                    }

                    cardTextList[i] =  cardText;
                }

               #region StackLayout
               var vViewLayout = new StackLayout()
               {
                   Children = {
                    new Label { Text = "Km óra állás: 408875 KM", TextColor = Color.Black },
                    new Label { Text = "Külső akku. fesz: 13V",TextColor = Color.Black },
                    new Label { Text = "Belső akku. fesz: 4V",TextColor = Color.Black }
                   }
               };
               #endregion

                var vSecond = new AccordionSource
                        ()
                    {
                        HeaderImageSource = "Icon.png",
                        HeaderTextColor = Color.White,
                        HeaderBackGroundColor = Color.Black,
                        ContentItems = vViewLayout,
                        CardTextArray = cardTextList,
                       

                    };

                   vResult.Add(vSecond);
                
            }
            return vResult;
        }

        internal static void OnDataRefreshed()
        {
           /* CardsPage.MainOne.DataSource = GetSampleData();
            MainOne.DataBind();*/
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
    }
}
