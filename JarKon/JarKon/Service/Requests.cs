﻿using System;
using System.Collections.Generic;
using System.Text;

namespace JarKon.Service
{
    public class GeneralRequest
    {
        public int userId { get; set; }
    }

    public class VehicleStatusRequest
    {
        public int vehicleId { get; set; }
    }
    public class RenewLoginRequest
    {
        public string token { get; set; }
    }

    public class LoginRequest
    {
        public string username { get; set; }
        public string password { get; set; }
        public ClientType clientType { get; set; }
        public string deviceType { get; set; }
        public string deviceId { get; set; }
    }
    public enum ClientType
    {
        WEB,
        ANDROID,
        IOS
    }

    
}
