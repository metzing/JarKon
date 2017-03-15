using JarKon.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Maps;

namespace JarKon.ViewModel
{
    class CardsViewModel
    {
        public static List<Vehicle> Vehicles = GetDummyData();


        public static List<Vehicle> GetDummyData()
        {
            List<Vehicle> retu = new List<Vehicle> {(

            new Vehicle
            {
                ID = 1,
                Position = new Position(47.472999, 19.052566),
                Speed = 69,
                Ignition = true,
                InternalBatteryVoltage = 1,
                ExternalBatteryVoltage = 2,
                DriverName = "John Doe"
            }),

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

            return retu;
        }

    }
}
