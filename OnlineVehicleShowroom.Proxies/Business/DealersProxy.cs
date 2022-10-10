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
    public class DealersProxy
    {
        private readonly string apiUrl;
        private readonly HttpClient client;

        public DealersProxy()
        {
            apiUrl = "https://localhost:44315/api/dealerapi";
            client = new HttpClient();
        }

        public async Task<List<Dealer>> GetAllDealersAsync(string tokenString)
        {
            List<Dealer> dealers = null;
            //string token = (JsonConvert.DeserializeObject(tokenString) as JObject).SelectToken("token").ToString();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenString);

            var response = await client.GetAsync(apiUrl);
            if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                throw new UnauthorizedAccessException();
            }
            string apiResponse = await response.Content.ReadAsStringAsync();
            dealers = JsonConvert.DeserializeObject<List<Dealer>>(apiResponse);
            return dealers;
        }

        public async Task<Dealer> GetDealerByIdAsync(int dealerId, string tokenString)
        {
            Dealer dealer = null;
            //string token = (JsonConvert.DeserializeObject(tokenString) as JObject).SelectToken("token").ToString();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenString);

            var response = await client.GetAsync($"{apiUrl}/{dealerId}");
            if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                throw new UnauthorizedAccessException();
            }
            string apiResponse = await response.Content.ReadAsStringAsync();
            dealer = JsonConvert.DeserializeObject<Dealer>(apiResponse);
            return dealer;
        }

        public async Task<Dealer> AddDealerAsync(Dealer dealer, string tokenString)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(dealer), Encoding.UTF8, "application/json");

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
                dealer = JsonConvert.DeserializeObject<Dealer>(apiResponse);
                return dealer;
            }
            else
            {
                throw new OnlineVehicleShowroomException("Error! Dealer was not added!");
            }
        }

        public async Task<bool> UpdateDealerAsync(int dealerId, Dealer dealer, string tokenString)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(dealer), Encoding.UTF8, "application/json");

            //string token = (JsonConvert.DeserializeObject(tokenString) as JObject).SelectToken("token").ToString();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenString);

            var response = await client.PutAsync($"{apiUrl}/{dealerId}", content);
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
                throw new OnlineVehicleShowroomException("Error! Dealer was not updated!");
            }
        }

        public async Task<bool> DeleteDealerAsync(int dealerId, string tokenString)
        {
            //string token = (JsonConvert.DeserializeObject(tokenString) as JObject).SelectToken("token").ToString();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenString);

            var response = await client.DeleteAsync($"{apiUrl}/{dealerId}");
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
                throw new OnlineVehicleShowroomException("Error! Dealer was not deleted!");
            }
        }
    }
}
