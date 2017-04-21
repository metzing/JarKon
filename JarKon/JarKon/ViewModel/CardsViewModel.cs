﻿using JarKon.Model;
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
    class CardViewModel
    {
        private static CardViewModel instance;
        public static CardViewModel Instance
        {
            get
            {
                return instance ?? (instance = new CardViewModel());
            }
        }
        public ObservableCollection<CardData> CardDataSource;
        private CardViewModel()
        {
            CardDataSource = new ObservableCollection<CardData>();
        }
        internal static void OnDataRefreshed()
        {
            LoadVehicles();
            Provider.Instance.CardsPage.LoadCards();
        }
        public static void LoadVehicles()
        {
            lock (Instance.CardDataSource)
            {
                Instance.CardDataSource.Clear();
                lock (Provider.Instance.Vehicles) lock (Provider.Instance.VehicleStates)
                    {
                        List<Vehicle> vehicles = Provider.Instance.Vehicles;
                        foreach (var vehicle in vehicles)
                        {
                            Instance.CardDataSource.Add(CreateDataSource(vehicle));
                        }
                    }
            }
            Provider.Instance.CardsPage.LoadCards();
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

            string ImageSource = GetImageSource(vehicle);

            return new CardData()
            {
                VehicleID = vehicle.vehicleId,
                HeaderImageSource = ImageSource,
                SelectedDetails = cardTextList,
                ExpandedTextList = expandTextList,
                PlateNumber = vehicle.plateNumber
            };
        }

        private static string GetImageSource(Vehicle vehicle)
        {
            string iconName = "vehicle_13_";

            var vehicleState = Provider.Instance.VehicleStates.Find(vs => vs.vehicleId == vehicle.vehicleId);

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
            return iconName;
        }

        internal Xamarin.Forms.View GetCardForPopup(Vehicle vehicle)
        {
            Card card = new Card();
            card.SetData(CreateDataSource(vehicle));
            return card;
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
        public int VehicleID { get; set; }
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