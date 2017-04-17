using JarKon.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using JarKon.Core;
using JarKon.ViewModel;


using JarKon.View;
using JarKon;

using Xamarin.Forms;


namespace JarKon.ViewModel

{
    class CardsViewModel
    {
        int VEHICLE_DATA_TYPES_NUM = 6;
        int EXPANDED_DATA_TYPES_NUM = 4;

        public static bool canLoadVehicles = true;

        private static Accordion  CPAccordion { get { return Provider.Instance.CardsPage.Accordion; } }

        public static void LoadVehicles()
        {
                Device.BeginInvokeOnMainThread(() =>
                {
                    if (canLoadVehicles)
                    {
                        CPAccordion.mDataSource.Clear();
                        CPAccordion.mDataSource = GetSampleData();
                        CPAccordion.DataBind();
                        canLoadVehicles = false;

                    }
                });

             
            
        }

        internal static void OnDataRefreshed()
        {
           //TODO hogy csak egyszer fusson le, de igazából ez a callback hívódik meg valamiért sokszor
            LoadVehicles();
            

        }


        public static List<AccordionSource> GetSampleData()
        {
            var vResult = new List<AccordionSource>();

            User currentUser = Provider.Instance.CurrentUser;
            List<Vehicle> vehicles = Provider.Instance.Vehicles;

            int VEHICLE_DATA_TYPES_NUM = 6;
            int EXPANDED_DATA_TYPES_NUM = 4;


            foreach (Vehicle vehicle in vehicles)
            {


                CardText[] cardTextList = new CardText[VEHICLE_DATA_TYPES_NUM];
                List<CardText> expandTextList = new List<CardText>();
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
                        NullReferenceException error = e;
                        cardText.top = "";
                        cardText.bottom = "";
                    }

                    cardTextList[i] = cardText;
                }

        
                List<VehicleDataType> asList = Enum.GetValues(typeof(VehicleDataType)).Cast<VehicleDataType>().ToList();

                foreach (VehicleDataType vdt in asList)
                {
                    int i = 0;
                    if (!vehicleDataTypes.Contains(vdt))
                    {
                        try
                        {
                            CardText t = GetCardTextByType(vdt, vehicle, vehicleState);
                            expandTextList.Add(t);
                        }catch(NullReferenceException e)
                        {
                            NullReferenceException ef = e;
                        }

                    }
                }


                var vSecond = new AccordionSource()
                {
                    HeaderImageSource = "Icon.png",
                    // = Color.White,
                    // HeaderBackGroundColor = Color.Black,
                    // ContentItems = vViewLayout,
                    CardTextArray = cardTextList,
                    ExpandedTextList = expandTextList,
                    PlateNumber = vehicle.plateNumber


                };

                vResult.Add(vSecond);

            }
            return vResult;
        }

        

        public static CardText GetCardTextByType(VehicleDataType? dataType, Vehicle vehicle, VehicleState vehicleState)
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


   /* public static ObservableCollection<VehicleState> GetDummyData()
        {
            ObservableCollection<VehicleState> retu = new ObservableCollection<VehicleState> {(

            new VehicleState
            {
                vehicleId = 1,
                position = new Position{
                    lat=47.472999f,
                    lng=19.052566f},
                speed = 69,
                ignition = true,
                extBattVolt = 1,
                intBattVolt = 2,
                driver = "John Doe"
            }),

            new VehicleState
            {
                vehicleId = 2,
                position = new Position{
                    lat=47.408770f,
                    lng =19.017055f},
                speed = 420,
                ignition = false,
                intBattVolt = 3,
                extBattVolt = 4,
                driver = "Pikachu"
            },
            new VehicleState
            {
                vehicleId = 2,
                position = new Position{
                    lat=47.408770f,
                    lng =19.017055f},
                speed = 420,
                ignition = false,
                intBattVolt = 3,
                extBattVolt = 4,
                driver = "Pikachu"
            },
            new VehicleState
            {
                vehicleId = 2,
                position = new Position{
                    lat=47.408770f,
                    lng =19.017055f},
                speed = 420,
                ignition = false,
                intBattVolt = 3,
                extBattVolt = 4,
                driver = "Pikachu"
            },
            new VehicleState
            {
                vehicleId = 2,
                position = new Position{
                    lat=47.408770f,
                    lng =19.017055f},
                speed = 420,
                ignition = false,
                intBattVolt = 3,
                extBattVolt = 4,
                driver = "Pikachu"
            }
            };

            return retu;
        }

    }*/
}
