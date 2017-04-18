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
using JarKon.View.CardDetails;

namespace JarKon.ViewModel

{
    class CardsViewModel
    {
        private static CardsViewModel instance;
        public static CardsViewModel Instance
        {
            get
            {
                return instance ?? (instance = new CardsViewModel());
            }
        }

        public ObservableCollection<CardData> CardDataSource;

        private CardsViewModel()
        {
            CardDataSource = new ObservableCollection<CardData>();
        }

        internal static void OnDataRefreshed()
        {
            LoadVehicles();
        }

        public static void LoadVehicles()
        {
            Instance.CardDataSource.Clear();

            List<Vehicle> vehicles = Provider.Instance.Vehicles;

            foreach (var vehicle in vehicles)
            {
                Instance.CardDataSource.Add(CreateDataSource(vehicle));
            }
        }

        private static CardData CreateDataSource(Vehicle vehicle)
        {
            VehicleState vehicleState = Provider.Instance.VehicleStates.Find(vs => vs.vehicleId == vehicle.vehicleId);

            List<DetailText> cardTextList = new List<DetailText>();
            var selectedDetails = Provider.Instance.CurrentUser.settings.vehicleViewSettings.Find(p => p.vehicleId == vehicle.vehicleId).cellSet;

            foreach (var item in selectedDetails)
            {
                DetailText cardText;

                try
                {
                    cardText = GetCardTextByType(item, vehicle, vehicleState);
                }
                catch (NullReferenceException e)
                {
                    cardText = new DetailText();
                    cardText.top = "";
                    cardText.bottom = "";
                }

                cardTextList.Add(cardText);
            }

            List<DetailText> expandTextList = new List<DetailText>();

            var notSelectedDetails = Enum.GetValues(typeof(VehicleDataType)).Cast<VehicleDataType>().ToList();
            notSelectedDetails.RemoveAll(p => selectedDetails.Contains(p));

            foreach (var item in notSelectedDetails)
            {
                try
                {
                    DetailText t = GetCardTextByType(item, vehicle, vehicleState);
                    expandTextList.Add(t);
                }
                catch (NullReferenceException e)
                {
                    NullReferenceException ef = e;
                }
            }

            return new CardData()
            {
                HeaderImageSource = "Icon.png",
                SelectedDetails = cardTextList,
                ExpandedTextList = expandTextList,
                PlateNumber = vehicle.plateNumber
            };
        }

        internal Xamarin.Forms.View GetCardForPopup(Vehicle vehicle)
        {
            return CardListView.BuildCard(CreateDataSource(vehicle)); 
        }

        public static DetailText GetCardTextByType(VehicleDataType? dataType, Vehicle vehicle, VehicleState vehicleState)
        {
            DetailText cardText = new DetailText();

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

    public class CardData
    {
        public string HeaderImageSource { get; set; }
        public string PlateNumber { get; set; }
        public List<DetailText> SelectedDetails { get; set; }
        public List<DetailText> ExpandedTextList { get; set; }

    }

    public class DetailText
    {
        public string top { get; set; }
        public string bottom { get; set; }
        public DetailText()
        {
            top = "";
            bottom = "";
        }
    }
}
