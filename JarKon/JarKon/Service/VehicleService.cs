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
    public class ServiceException : Exception
    {
        public string Name { get; set; }
        public string Group { get; set; }
    }

    public class VehicleService
    {
        private async Task<T> GetAsync<T>(Uri url)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("APIKey", "rdc4YE=sZRH&^G+D4b73cqA2QqC*fzJ4SwB2C&=zB#CC@Aa%w3_K2zW?ysU@bPUQxW ^ P ^?3_4fW38TV ^ 5texx@e4XGNBUkwwt ^ n7RkmgxDuM3R4 ?% L ^ dfYy8FS = BDm");
                var response = await client.GetAsync(url);
                var json = await response.Content.ReadAsStringAsync();

                if (CheckError(json))
                {
                    var jobject = JObject.Parse(json);
                    throw new ServiceException
                    {
                        Name = (string)jobject["name"],
                        Group = (string)jobject["group"]
                    };
                }

                return JsonConvert.DeserializeObject<T>(json);

            }
        }

        private bool CheckError(string json)
        {
            var testObj = JObject.Parse(json);
            JToken token;

            return testObj.TryGetValue("group", out token);
        }

        public async Task<List<Vehicle>> GetVehicles()
        {
            return null;
        }

    }
}
