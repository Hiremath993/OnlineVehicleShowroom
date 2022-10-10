using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVehicleShowroom.Proxies.Auth
{
    //Communicates with the Api & calls its functionalities over here
    public class AuthProxy
    {
        private readonly string apiUrl;
        private readonly HttpClient client;

        //Constructor to initialize apiUrl & client object
        public AuthProxy()
        {
            apiUrl = "https://localhost:44315/api/authapi";
            client = new HttpClient();
        }


        public async Task<bool> RegisterUserAsync(dynamic model)
        {
            //Creating and passing bearer token for authorization
            StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{apiUrl}/register", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<string> LoginAsync(dynamic model)
        {
            //Creating and passing bearer token for authorization
            StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{apiUrl}/login", content);
            string token = await response.Content.ReadAsStringAsync();
            return token;
        }
    }
}
