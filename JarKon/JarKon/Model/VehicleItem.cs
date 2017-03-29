using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Maps;

namespace JarKon.Model
{
    public class VehicleItem
    {
        public int ID { get; set; }
        public Position Position { get; set; }
        public double Speed { get; set; }
        public bool Ignition { get; set; }
        public double InternalBatteryVoltage { get; set; }
        public double ExternalBatteryVoltage { get; set; }
        public string DriverName { get; set; }
        public Vehicle Vehicle { get; set;}
    }
}
