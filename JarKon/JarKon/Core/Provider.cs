using JarKon.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace JarKon.Core
{
    public static class Provider
    {
        public static User CurrentUser { get; set; }
        public static string LoginToken { get; set; }
        public static List<Vehicle> Vehicles { get; set; }
        public static List<VehicleState> VehicleStates { get; set; } 
    }
}
