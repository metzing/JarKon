using System;
using System.Collections.Generic;
using System.Text;

namespace JarKon.Model
{
    public class User
    {
        public int userId { get; set; }
        public string username { get; set; }
        public string name { get; set; }
        public UserSettings settings { get; set; }

    }

    public class UserSettings
    {
        public VehicleViewSettings[] vehicleViewSettings { get; set; }
        public GeneralViewSettings generalViewSettings { get; set; }
        public Permissions permissions { get; set; }
    }

    public class Permissions
    {
        public Functionality[] funtionalities { get; set; }
    }
    public enum Functionality
    {
        PARKING
        //... "bővül"
    }

    public class GeneralViewSettings
    {
        public DefaultLocation defaultLocation { get; set; }
    }

    public class DefaultLocation
    {
        public string address { get; set; }
        public Position[] position { get; set; }
        public int zoom { get; set; }
    }

    public class VehicleViewSettings
    {
        public int vehicleId { get; set; }
        public VehicleDataType[] cellSet { get; set; }
    }
}
