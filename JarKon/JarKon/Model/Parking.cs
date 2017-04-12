using System;
using System.Collections.Generic;
using System.Text;

namespace JarKon.Model
{
    public class Parking
    {
        public Zone zone { get; set; }
        public string maxEndTime { get; set; }
    }

    public class Zone
    {
        public int zoneCode { get; set; }
        public string zoneName { get; set; }
        public double zoneTarif { get; set; }
        public bool isFixTicketZone { get; set; }
    }
}
