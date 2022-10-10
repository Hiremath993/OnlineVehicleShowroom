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
    public class ShowroomsProxy
    {
        private readonly string apiUrl;
        private readonly HttpClient client;

        public ShowroomsProxy()
        {
            apiUrl = "https://localhost:44315/api/showroomapi";
            client = new HttpClient();
        }

        public async Task<List<Showroom>> GetAllShowroomsAsync(string tokenString)
        {
            List<Showroom> showrooms = null;
            //string token = (JsonConvert.DeserializeObject(tokenString) as JObject).SelectToken("token").ToString();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenString);

            var response = await client.GetAsync(apiUrl);
            if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                throw new UnauthorizedAccessException();
            }
            string apiResponse = await response.Content.ReadAsStringAsync();
            showrooms = JsonConvert.DeserializeObject<List<Showroom>>(apiResponse);
            return showrooms;
        }

        public async Task<Showroom> GetShowroomByIdAsync(int showroomId, string tokenString)
        {
            Showroom showroom = null;
            //string token = (JsonConvert.DeserializeObject(tokenString) as JObject).SelectToken("token").ToString();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenString);

            var response = await client.GetAsync($"{apiUrl}/{showroomId}");
            if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                throw new UnauthorizedAccessException();
            }
            string apiResponse = await response.Content.ReadAsStringAsync();
            showroom = JsonConvert.DeserializeObject<Showroom>(apiResponse);
            return showroom;
        }

        public async Task<Showroom> AddShowroomAsync(Showroom showroom, string tokenString)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(showroom), Encoding.UTF8, "application/json");

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
                showroom = JsonConvert.DeserializeObject<Showroom>(apiResponse);
                return showroom;
            }
            else
            {
                throw new OnlineVehicleShowroomException("Error! Showroom was not added!");
            }
        }

        public async Task<bool> UpdateShowroomAsync(int showroomId, Showroom showroom, string tokenString)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(showroom), Encoding.UTF8, "application/json");

            //string token = (JsonConvert.DeserializeObject(tokenString) as JObject).SelectToken("token").ToString();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenString);

            var response = await client.PutAsync($"{apiUrl}/{showroomId}", content);
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
                throw new OnlineVehicleShowroomException("Error! Showroom was not updated!");
            }
        }

        public async Task<bool> DeleteShowroomAsync(int showroomId, string tokenString)
        {
            //string token = (JsonConvert.DeserializeObject(tokenString) as JObject).SelectToken("token").ToString();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenString);

            var response = await client.DeleteAsync($"{apiUrl}/{showroomId}");
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
                throw new OnlineVehicleShowroomException("Error! Showroom was not deleted!");
            }
        }
    }
}
