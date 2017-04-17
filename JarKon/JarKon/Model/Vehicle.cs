namespace JarKon.Model
{
    public class Vehicle
    {
        public int vehicleId { get; set; }
        public string plateNumber { get; set; }
        public string make { get; set; }
        public string type { get; set; }
    }

    public class VehicleState
    {
        public int vehicleId { get; set; }
        public int time { get; set; }
        public Position position { get; set; }
        public string address { get; set; }
        public int? speed { get; set; }
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
        public string driver { get; set; }
    }

    public class Position
    {
        public float lat { get; set; }
        public float lng { get; set; }
    }

    public enum VehicleDataType
    {
        PLATE_NUMBER,
        VEHICLE_TYPE,
        TIME,
        ADDRESS,
        BUSINESS_TRIP,
        MAKE,
        LAT,
        LNG,
        SPEED,
        RPM,
        IGNITION,
        MILEAGE_STATE,
        SIGNAL,
        EXT_BATT_VOLT,
        INT_BATT_VOLT,
        FUEL_1,
        FUEL_2,
        SUM_BURNED_FUEL,
        AXLE_NUM_SET,
        DRIVER
    }
}

