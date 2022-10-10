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
    public class SalesProxy
    {
        private readonly string apiUrl;
        private readonly HttpClient client;

        public SalesProxy()
        {
            apiUrl = "https://localhost:44315/api/salesapi";
            client = new HttpClient();
        }

        public async Task<List<Sales>> GetAllSalesAsync(string tokenString)
        {
            List<Sales> sales = null;
            //string token = (JsonConvert.DeserializeObject(tokenString) as JObject).SelectToken("token").ToString();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenString);

            var response = await client.GetAsync(apiUrl);
            if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                throw new UnauthorizedAccessException();
            }
            string apiResponse = await response.Content.ReadAsStringAsync();
            sales = JsonConvert.DeserializeObject<List<Sales>>(apiResponse);
            return sales;
        }

        public async Task<Sales> GetSalesByIdAsync(int salesId, string tokenString)
        {
            Sales sales = null;
            //string token = (JsonConvert.DeserializeObject(tokenString) as JObject).SelectToken("token").ToString();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenString);

            var response = await client.GetAsync($"{apiUrl}/{salesId}");
            if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                throw new UnauthorizedAccessException();
            }
            string apiResponse = await response.Content.ReadAsStringAsync();
            sales = JsonConvert.DeserializeObject<Sales>(apiResponse);
            return sales;
        }

        public async Task<Sales> AddSalesAsync(Sales sales, string tokenString)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(sales), Encoding.UTF8, "application/json");

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
                sales = JsonConvert.DeserializeObject<Sales>(apiResponse);
                return sales;
            }
            else
            {
                throw new OnlineVehicleShowroomException("Error! Sales was not added!");
            }
        }

        public async Task<bool> UpdateSalesAsync(int salesId, Sales sales, string tokenString)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(sales), Encoding.UTF8, "application/json");

            //string token = (JsonConvert.DeserializeObject(tokenString) as JObject).SelectToken("token").ToString();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenString);

            var response = await client.PutAsync($"{apiUrl}/{salesId}", content);
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
                throw new OnlineVehicleShowroomException("Error! Sales was not updated!");
            }
        }

        public async Task<bool> DeleteSalesAsync(int salesId, string tokenString)
        {
            //string token = (JsonConvert.DeserializeObject(tokenString) as JObject).SelectToken("token").ToString();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenString);

            var response = await client.DeleteAsync($"{apiUrl}/{salesId}");
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
                throw new OnlineVehicleShowroomException("Error! Sales was not deleted!");
            }
        }
    }
}
