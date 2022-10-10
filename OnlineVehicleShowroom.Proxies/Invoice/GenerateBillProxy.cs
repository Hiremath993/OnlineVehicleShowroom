using Newtonsoft.Json;
using OnlineVehicleShowroom.Entities.Invoice;
using OnlineVehicleShowroom.Exceptions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVehicleShowroom.Proxies.Invoice
{
    public class GenerateBillProxy
    {
        private readonly string apiUrl;
        private readonly HttpClient client;

        public GenerateBillProxy()
        {
            apiUrl = "https://localhost:44315/api/generatebillapi";
            client = new HttpClient();
        }

        public async Task<GenerateBill> GetBillByIdAsync(int orderId, string tokenString)
        {
            GenerateBill generateBill = null;
            //string token = (JsonConvert.DeserializeObject(tokenString) as JObject).SelectToken("token").ToString();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenString);

            var response = await client.GetAsync($"{apiUrl}/{orderId}");
            if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                throw new UnauthorizedAccessException();
            }
            string apiResponse = await response.Content.ReadAsStringAsync();
            generateBill = JsonConvert.DeserializeObject<GenerateBill>(apiResponse);
            return generateBill;
        }

        public async Task<List<GenerateBill>> GetAllGenerateBillAsync(string tokenString)
        {
            List<GenerateBill> generateBill = null;
            //string token = (JsonConvert.DeserializeObject(tokenString) as JObject).SelectToken("token").ToString();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenString);

            var response = await client.GetAsync(apiUrl);
            if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                throw new UnauthorizedAccessException();
            }
            string apiResponse = await response.Content.ReadAsStringAsync();
            generateBill = JsonConvert.DeserializeObject<List<GenerateBill>>(apiResponse);
            return generateBill;
        }

        

        public async Task<GenerateBill> AddGenerateBillAsync(GenerateBill generateBill, string tokenString)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(generateBill), Encoding.UTF8, "application/json");

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
                generateBill = JsonConvert.DeserializeObject<GenerateBill>(apiResponse);
                return generateBill;
            }
            else
            {
                throw new OnlineVehicleShowroomException("Error! GenerateBill was not added!");
            }
        }

        public async Task<bool> UpdateGenerateBillAsync(int orderId, GenerateBill generateBill, string tokenString)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(generateBill), Encoding.UTF8, "application/json");

            //string token = (JsonConvert.DeserializeObject(tokenString) as JObject).SelectToken("token").ToString();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenString);

            var response = await client.PutAsync($"{apiUrl}/{orderId}", content);
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
                throw new OnlineVehicleShowroomException("Error! GenerateBill was not updated!");
            }
        }

        public async Task<bool> DeleteGenerateBillAsync(int orderId, string tokenString)
        {
            //string token = (JsonConvert.DeserializeObject(tokenString) as JObject).SelectToken("token").ToString();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenString);

            var response = await client.DeleteAsync($"{apiUrl}/{orderId}");
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
                throw new OnlineVehicleShowroomException("Error! GenerateBill was not deleted!");
            }
        }
    }
}
