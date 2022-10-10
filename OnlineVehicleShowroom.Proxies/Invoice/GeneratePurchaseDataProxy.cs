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
    public class GeneratePurchaseDataProxy
    {
        private readonly string apiUrl;
        private readonly HttpClient client;

        public GeneratePurchaseDataProxy()
        {
            apiUrl = "https://localhost:44315/api/generatepurchasedataapi";
            client = new HttpClient();
        }

        public async Task<GeneratePurchaseData> GetPurchaseDataByIdAsync(int purchaseId, string tokenString)
        {
            GeneratePurchaseData generatePurchaseData = null;
            //string token = (JsonConvert.DeserializeObject(tokenString) as JObject).SelectToken("token").ToString();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenString);

            var response = await client.GetAsync($"{apiUrl}/{purchaseId}");
            if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                throw new UnauthorizedAccessException();
            }
            string apiResponse = await response.Content.ReadAsStringAsync();
            generatePurchaseData = JsonConvert.DeserializeObject<GeneratePurchaseData>(apiResponse);
            return generatePurchaseData;
        }

        public async Task<List<GeneratePurchaseData>> GetAllGeneratePurchaseDataAsync(string tokenString)
        {
            List<GeneratePurchaseData> generatePurchaseData = null;
            //string token = (JsonConvert.DeserializeObject(tokenString) as JObject).SelectToken("token").ToString();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenString);

            var response = await client.GetAsync(apiUrl);
            if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                throw new UnauthorizedAccessException();
            }
            string apiResponse = await response.Content.ReadAsStringAsync();
            generatePurchaseData = JsonConvert.DeserializeObject<List<GeneratePurchaseData>>(apiResponse);
            return generatePurchaseData;
        }



        public async Task<GeneratePurchaseData> AddGeneratePurchaseDataAsync(GeneratePurchaseData generatePurchaseData, string tokenString)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(generatePurchaseData), Encoding.UTF8, "application/json");

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
                generatePurchaseData = JsonConvert.DeserializeObject<GeneratePurchaseData>(apiResponse);
                return generatePurchaseData;
            }
            else
            {
                throw new OnlineVehicleShowroomException("Error! GeneratePurchaseData was not added!");
            }
        }

        public async Task<bool> UpdateGeneratePurchaseDataAsync(int orderId, GeneratePurchaseData generatePurchaseData, string tokenString)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(generatePurchaseData), Encoding.UTF8, "application/json");

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
                throw new OnlineVehicleShowroomException("Error! GeneratePurchaseData was not updated!");
            }
        }

        public async Task<bool> DeleteGeneratePurchaseDataAsync(int orderId, string tokenString)
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
                throw new OnlineVehicleShowroomException("Error! GeneratePurchaseData was not deleted!");
            }
        }
    }
}
