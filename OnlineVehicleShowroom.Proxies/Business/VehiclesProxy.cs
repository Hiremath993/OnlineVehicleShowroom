using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OnlineVehicleShowroom.Entities.Business;
using OnlineVehicleShowroom.Exceptions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVehicleShowroom.Proxies.Business
{
    public class VehiclesProxy
    {
        private readonly string apiUrl;
        private readonly HttpClient client;

        public VehiclesProxy()
        {
            apiUrl = "https://localhost:44315/api/vehicleapi";
            client = new HttpClient();
        }

        public async Task<List<Vehicle>> GetAllVehiclesAsync(string tokenString)
        {
            List<Vehicle> vehicles = null;
            //string token = (JsonConvert.DeserializeObject(tokenString) as JObject).SelectToken("token").ToString();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenString);

            var response = await client.GetAsync(apiUrl);
            if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                throw new UnauthorizedAccessException();
            }
            string apiResponse = await response.Content.ReadAsStringAsync();
            vehicles = JsonConvert.DeserializeObject<List<Vehicle>>(apiResponse);
            return vehicles;
        }

        public async Task<Vehicle> GetVehicleByIdAsync(int vehicleId, string tokenString)
        {
            Vehicle vehicle = null;
            //string token = (JsonConvert.DeserializeObject(tokenString) as JObject).SelectToken("token").ToString();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenString);

            var response = await client.GetAsync($"{apiUrl}/GetByVehicleID/{vehicleId}");
            if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                throw new UnauthorizedAccessException();
            }
            string apiResponse = await response.Content.ReadAsStringAsync();
            vehicle = JsonConvert.DeserializeObject<Vehicle>(apiResponse);
            return vehicle;
        }

        public async Task<IEnumerable<Vehicle>> GetVehicleByNameAsync(string vehicleName, string tokenString)
        {
            List<Vehicle> vehicle = null;
            //string token = (JsonConvert.DeserializeObject(tokenString) as JObject).SelectToken("token").ToString();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenString);

            var response = await client.GetAsync($"{apiUrl}/GetByVehicleName/{vehicleName}");
            if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                throw new UnauthorizedAccessException();
            }
            string apiResponse = await response.Content.ReadAsStringAsync();
            vehicle = JsonConvert.DeserializeObject<List<Vehicle>>(apiResponse);
            return vehicle;
        }

        public async Task<IEnumerable<Vehicle>> GetVehicleByModelAsync(string vehicleModel, string tokenString)
        {
            List<Vehicle> vehicle = null;
            //string token = (JsonConvert.DeserializeObject(tokenString) as JObject).SelectToken("token").ToString();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenString);

            var response = await client.GetAsync($"{apiUrl}/GetByVehicleModel/{vehicleModel}");
            if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                throw new UnauthorizedAccessException();
            }
            string apiResponse = await response.Content.ReadAsStringAsync();
            vehicle = JsonConvert.DeserializeObject<List<Vehicle>>(apiResponse);
            return vehicle;
        }

        public async Task<IEnumerable<Vehicle>> GetVehicleByShowroomAsync(string showroomName, string tokenString)
        {
            List<Vehicle> vehicle = null;
            //string token = (JsonConvert.DeserializeObject(tokenString) as JObject).SelectToken("token").ToString();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenString);

            var response = await client.GetAsync($"{apiUrl}/GetByShowroom/{showroomName}");
            if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                throw new UnauthorizedAccessException();
            }
            string apiResponse = await response.Content.ReadAsStringAsync();
            vehicle = JsonConvert.DeserializeObject<List<Vehicle>>(apiResponse);
            return vehicle;
        }

        public async Task<IEnumerable<Vehicle>> GetVehicleByDealerAsync(int dealerId, string tokenString)
        {
            List<Vehicle> vehicle = null;
            //string token = (JsonConvert.DeserializeObject(tokenString) as JObject).SelectToken("token").ToString();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenString);

            var response = await client.GetAsync($"{apiUrl}/GetByDealerID/{dealerId}");
            if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                throw new UnauthorizedAccessException();
            }
            string apiResponse = await response.Content.ReadAsStringAsync();
            vehicle = JsonConvert.DeserializeObject<List<Vehicle>>(apiResponse);
            return vehicle;
        }

        public async Task<IEnumerable<Vehicle>> GetVehicleByOrderDateAsync(DateTime orderDate, string tokenString)
        {
            List<Vehicle> vehicle = null;
            //string token = (JsonConvert.DeserializeObject(tokenString) as JObject).SelectToken("token").ToString();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenString);

            var response = await client.GetAsync($"{apiUrl}/GetByOrderDate/{orderDate}");
            if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                throw new UnauthorizedAccessException();
            }
            string apiResponse = await response.Content.ReadAsStringAsync();
            vehicle = JsonConvert.DeserializeObject<List<Vehicle>>(apiResponse);
            return vehicle;
        }

        public async Task<IEnumerable<Vehicle>> GetVehicleByRatingAsync(double rating, string tokenString)
        {
            List<Vehicle> vehicle = null;
            //string token = (JsonConvert.DeserializeObject(tokenString) as JObject).SelectToken("token").ToString();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenString);

            var response = await client.GetAsync($"{apiUrl}/GetByRating/{rating}");
            if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                throw new UnauthorizedAccessException();
            }
            string apiResponse = await response.Content.ReadAsStringAsync();
            vehicle = JsonConvert.DeserializeObject<List<Vehicle>>(apiResponse);
            return vehicle;
        }

        public async Task<Vehicle> AddVehicleAsync(Vehicle vehicle, string tokenString)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(vehicle), Encoding.UTF8, "application/json");

            //string token = (JsonConvert.DeserializeObject(tokenString) as JObject).SelectToken("token").ToString();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenString);

            var response = await client.PostAsync(apiUrl, content);
            if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                throw new UnauthorizedAccessException();
            }
            if (response.StatusCode == HttpStatusCode.Created)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                vehicle = JsonConvert.DeserializeObject<Vehicle>(apiResponse);
                return vehicle;
            }
            else
            {
                throw new OnlineVehicleShowroomException("Error! Customer was not added!");
            }
        }

        public async Task<bool> UpdateVehicleAsync(int vehicleId, Vehicle vehicle, string tokenString)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(vehicle), Encoding.UTF8, "application/json");

            //string token = (JsonConvert.DeserializeObject(tokenString) as JObject).SelectToken("token").ToString();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenString);

            var response = await client.PutAsync($"{apiUrl}/{vehicleId}", content);
            if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                throw new UnauthorizedAccessException();
            }
            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                return true;
            }
            else
            {
                throw new OnlineVehicleShowroomException("Error! Vehicle was not updated!");
            }
        }

        public async Task<bool> DeleteVehicleAsync(int vehicleId, string tokenString)
        {
            //string token = (JsonConvert.DeserializeObject(tokenString) as JObject).SelectToken("token").ToString();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenString);

            var response = await client.DeleteAsync($"{apiUrl}/{vehicleId}");
            if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                throw new UnauthorizedAccessException();
            }
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                throw new OnlineVehicleShowroomException("Error! Vehicle was not deleted!");
            }
        }
    }
}
