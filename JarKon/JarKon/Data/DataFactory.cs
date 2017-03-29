using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;
using JarKon.Model;

namespace JarKon.Data
{
    public static class DataFactory
    {

        #region StringList
        public static List<string> StringList = new List<string>
        {
            "One", "2", "3","négy","5","6","8","9", "10"
        };
        #endregion

        #region ObservableStringList
        public static ObservableCollection<string> ObservableStringList = new ObservableCollection<string>
        {
            "One", "2", "3","négy","5","6","8","9", "10"
        };
        #endregion

        #region VehicleList
        public static ObservableCollection<Vehicle> VehicleList = new ObservableCollection<Vehicle> {
        new Vehicle
            {
                ID = 1,
                Position = new Position(47.472999, 19.052566),
                Speed = 69,
                Ignition = true,
                InternalBatteryVoltage = 1,
                ExternalBatteryVoltage = 2,
                DriverName = "John Doe"
            },

            new Vehicle
            {
                ID = 2,
                Position = new Position(47.408770, 19.017055),
                Speed = 420,
                Ignition = false,
                InternalBatteryVoltage = 3,
                ExternalBatteryVoltage = 4,
                DriverName = "Pikachu"
            },
            new Vehicle
            {
                ID = 2,
                Position = new Position(47.408770, 19.017055),
                Speed = 420,
                Ignition = false,
                InternalBatteryVoltage = 3,
                ExternalBatteryVoltage = 4,
                DriverName = "Pikachu"
            },
            new Vehicle
            {
                ID = 2,
                Position = new Position(47.408770, 19.017055),
                Speed = 420,
                Ignition = false,
                InternalBatteryVoltage = 3,
                ExternalBatteryVoltage = 4,
                DriverName = "Pikachu"
            },
            new Vehicle
            {
                ID = 2,
                Position = new Position(47.408770, 19.017055),
                Speed = 420,
                Ignition = false,
                InternalBatteryVoltage = 3,
                ExternalBatteryVoltage = 4,
                DriverName = "Pikachu"
            },
            new Vehicle
            {
                ID = 2,
                Position = new Position(47.408770, 19.017055),
                Speed = 420,
                Ignition = false,
                InternalBatteryVoltage = 3,
                ExternalBatteryVoltage = 4,
                DriverName = "Pikachu"
            },
            new Vehicle
            {
                ID = 2,
                Position = new Position(47.408770, 19.017055),
                Speed = 420,
                Ignition = false,
                InternalBatteryVoltage = 3,
                ExternalBatteryVoltage = 4,
                DriverName = "Pikachu"
            },
            new Vehicle
            {
                ID = 2,
                Position = new Position(47.408770, 19.017055),
                Speed = 420,
                Ignition = false,
                InternalBatteryVoltage = 3,
                ExternalBatteryVoltage = 4,
                DriverName = "Pikachu"
            }
            };
        #endregion
    }

}


