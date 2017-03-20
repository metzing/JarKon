using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Maps;

namespace JarKon.Model
{
    public class Vehicle
    {
        public int vehicleId { get; set; }
        public int time { get; set; }
        public Position position { get; set; }
        public string address { get; set; }
        public int speed { get; set; }
        public object rpm { get; set; }
        public float mileageState { get; set; }
        public bool ignition { get; set; }
        public bool businessTrip { get; set; }
        public int signal { get; set; }
        public float extBattVolt { get; set; }
        public float intBattVolt { get; set; }
        public object fuel1 { get; set; }
        public object fuel2 { get; set; }
        public object sumBurnedFuel { get; set; }
        public object axleNumSet { get; set; }
        public object driver { get; set; }
    }

    public class Position
    {
        public float lat { get; set; }
        public float lng { get; set; }
    }

}

