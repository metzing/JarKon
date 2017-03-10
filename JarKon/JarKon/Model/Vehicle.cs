using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Maps;

namespace JarKon.Model
{
    public class Vehicle
    {
        public int ID { get; set; }
        public Position Position { get; set; }
        public float Speed { get; set; }
        public bool Ignition { get; set; }
        public float InternalBatteryVoltage { get; set; }
        public float ExternalBatteryVoltage { get; set; }

        public static List<Vehicle> GetDummyData()
        {
            List<Vehicle> retu = new List<Vehicle>();

            retu.Add(new Vehicle
            {
                ID = 1,
                Position = new Position(47.472999, 19.052566),
                Speed = 69,
                Ignition = true,
                InternalBatteryVoltage = 1,
                ExternalBatteryVoltage = 2
            });

            retu.Add(new Vehicle
            {
                ID = 2,
                Position = new Position(47.408770, 19.017055),
                Speed = 420,
                Ignition = false,
                InternalBatteryVoltage = 3,
                ExternalBatteryVoltage = 4
            });

            return retu;
        }
    }
}
