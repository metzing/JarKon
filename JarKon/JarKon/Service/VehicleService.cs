using JarKon.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JarKon.Service
{
    /// <summary>
    /// Provides an interface for the API
    /// </summary>
    public class VehicleService
    {
        private Uri BaseURL = new Uri("http://jarkon.hu/");
        private string APIKey = "rdc4YE=sZRH&^G+D4b73cqA2Q-qC*fzJ4SwB2C&=zB#CC@Aa%w3_K2zW?ysU@bPUQxW^P^?3_4fW38TV^5texx@e4XGNBUkwwt^n7Rk-mgxDuM3R4?%L^dfYy8FS=BDm";

        /// <summary>
        /// Base method for HTTP GET requests
        /// </summary>
        /// <typeparam name="T">Type of the object requested</typeparam>
        /// <param name="url">The URL of the endpoint</param>
        /// <returns></returns>
        private async Task<T> GetAsync<T>(Uri url)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("APIKey", APIKey);
                client.DefaultRequestHeaders.Add("Token", Core.Settings.LoginToken);

                var response = await client.GetAsync(url);
                var json = await response.Content.ReadAsStringAsync();

                CheckError(json);

                return JsonConvert.DeserializeObject<T>(json);
            }
        }

        /// <summary>
        /// Base method for HTTP POST requests
        /// </summary>
        /// <typeparam name="T">Type of object to be posted</typeparam>
        /// <param name="url">Endpoint the object is posted to</param>
        /// <param name="data">The actual object posted</param>
        private async Task<LoginResponse> PostAsync<T>(Uri url, T data)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("APIKey", APIKey);

                var json = JsonConvert.SerializeObject(data);
                var httpContent = new StringContent(json);

                var response = await (await client.PostAsync(url, httpContent)).Content.ReadAsStringAsync();


                CheckError(response);

                return JsonConvert.DeserializeObject<LoginResponse>(response);

            }
        }

        /// <summary>
        /// Base method for HTTP PUT requests
        /// </summary>
        /// <typeparam name="T">Type of object to be put</typeparam>
        /// <param name="url">Endpoint the object is put to</param>
        /// <param name="data">The actual object put</param>
        private async Task<LoginResponse> PutAsync<T>(Uri url, T data)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("APIKey", APIKey);

                var json = JsonConvert.SerializeObject(data);
                var httpContent = new StringContent(json);

                var response = await (await client.PutAsync(url, httpContent)).Content.ReadAsStringAsync();

                CheckError(response);

                return JsonConvert.DeserializeObject<LoginResponse>(response);
            }
        }

        /// <summary>
        /// Checks if the server's response was an ErrorResponse, throws an exception if so
        /// </summary>
        /// <param name="json">Response of the server</param>
        private void CheckError(string json)
        {
            var testObj = JObject.Parse(json);

            if ((string)testObj["type"] == "ErrorResponse")
            {
                var error = JsonConvert.DeserializeObject<ErrorResponse>(json);
                throw new ServiceException
                {
                    Name = error.name,
                    Group = error.group,
                    Message = error.message
                };
            }
        }

        /// <summary>
        /// Handles a thrown ServiceException by displaying an alert to the tester
        /// </summary>
        /// <param name="se">Exception thrown</param>
        private void HandleException(ServiceException se)
        {
            Xamarin.Forms.Application.Current.MainPage.DisplayAlert($"Service exception: {se.Name}", se.Message, "OK");
        }
        
        /// <summary>
        /// Sends a ping to the API. For dev purposes, should not be in the final build.
        /// </summary>
        /// <returns></returns>
        public async Task<PingResponse> GetPing()
        {
            PingResponse response = null;
            try
            {
                response = await GetAsync<PingResponse>(new Uri(BaseURL, "api/ping/"));
            }
            catch(ServiceException se)
            {
                HandleException(se);
            }
            return response;
        }

        /// <summary>
        /// Logs in the user using username and password
        /// </summary>
        /// <param name="request"></param>
        /// <returns>New token</returns>
        public async Task<LoginResponse> Login(LoginRequest request)
        {
            LoginResponse response = null;
            try
            {
                response = await PostAsync(new Uri(BaseURL, "api/users/login/"), request);
            }
            catch(ServiceException se)
            {
                HandleException(se);
            }
            return response;
        }

        /// <summary>
        /// Logs in the user using it's token
        /// </summary>
        /// <param name="token">Old token</param>
        /// <returns>New token</returns>
        public async Task<LoginResponse> LoginWithToken(RenewLoginRequest request)
        {
            LoginResponse response = null;
            try
            {
                response = await PutAsync(new Uri(BaseURL, "api/users/login/"), request);
            }
            catch (ServiceException se)
            {
                HandleException(se);
            }
            return response;
        }
        /// <summary>
        /// Get the header of all vehicles
        /// </summary>
        /// <returns>Vehicle headers</returns>
        public async Task<VehicleResponse> GetVehicles(GeneralRequest request)
        {
            VehicleResponse result = null;
            try
            {
                result = await GetAsync<VehicleResponse>(new Uri(BaseURL, $"api/vehicles?userId={request.userId}/"));
            }
            catch (ServiceException se)
            {
                HandleException(se);
            }
            return result;
        }

        /// <summary>
        /// Get the state of a vehicle
        /// </summary>
        /// <param name="vehicleID">ID of the vehicle</param>
        /// <returns>Status of the vehicle</returns>
        public async Task<VehicleStateResponse> GetVehicleState(VehicleStatusRequest request)
        {
            VehicleStateResponse result = null;
            try
            {
                result = await GetAsync<VehicleStateResponse>(new Uri(BaseURL, $"api/vehicles/states?vehicleId={request.vehicleId}/"));
            }
            catch (ServiceException se)
            {
                HandleException(se);
            }
            return result;
        }

        /// <summary>
        /// Gets the state of all of the vehicles
        /// </summary>
        /// <returns>State of all the vehicles</returns>
        public async Task<VehicleStateResponse> GetVehicleStates(GeneralRequest request)
        {
            VehicleStateResponse result = null;
            try
            {
                result = await GetAsync<VehicleStateResponse>(new Uri(BaseURL, $"api/states?userId={request.userId}/"));
            }
            catch (ServiceException se)
            {
                HandleException(se);
            }
            return result;
        }
    }

    /// <summary>
    /// Indicates an error with API usage, or reachability. For testing purposes.
    /// </summary>
    public class ServiceException : Exception
    {
        public string Name { get; set; }
        public string Group { get; set; }
        new public string Message { get; set; }
    }
}
