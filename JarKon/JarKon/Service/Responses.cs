using JarKon.Model;

namespace JarKon.Service
{
    public class Response
    {
        public string type { get; set; }
    }

    public class PingResponse : Response
    {
        public int time { get; set; }
    }
    public class ErrorResponse : Response
    {
        public string name { get; set; }
        public string group { get; set; }
        public string message { get; set; }
        public string trace { get; set; }
    }

    public class LoginResponse : Response
    {
        public string token { get; set; }
        public User user { get; set; }
    }

    public class UserResponse : Response
    {
        public User[] users { get; set; }
    }

    public class UserSettingsResponse : Response
    {
        public UserSettings settings { get; set; }
    }

    public class VehicleResponse : Response
    {
        public Vehicle[] vehicles { get; set; }
    }

    public class VehicleStateResponse : Response
    {
        public VehicleState[] states { get; set; }
    }
}
