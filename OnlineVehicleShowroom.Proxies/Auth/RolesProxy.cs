using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OnlineVehicleShowroom.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVehicleShowroom.Proxies.Auth
{
    //Communicates with the Roles Api & calls its functionalities over here
    public class RolesProxy
    {
        private readonly string apiUrl;
        private readonly HttpClient client;

        //Constructor to initialize apiUrl & client object
        public RolesProxy()
        {
            apiUrl = "https://localhost:44315/api/rolesapi";
            client = new HttpClient();
        }

        //Gives list of all the roles present in database
        public async Task<List<IdentityRole>> GetRolesAsync(string tokenString)
        {
            List<IdentityRole> roles = null;
            //Creating and passing bearer token for authorization
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenString);

            //Checking valid url
            var response = await client.GetAsync(apiUrl);
            if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                throw new UnauthorizedAccessException();
            }
            string apiResponse = await response.Content.ReadAsStringAsync();
            roles = JsonConvert.DeserializeObject<List<IdentityRole>>(apiResponse);

            return roles;
        }

        //declaring a method for adding new role
        public async Task<bool> AddRoleAsync(string name, string tokenString)
        {
            //Creating and passing bearer token for authorization
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenString);

            //Checking valid url
            var response = await client.PostAsync($"{apiUrl}/{name}", null);
            return (response.StatusCode == HttpStatusCode.OK);
        }

        //declaring a Method for Deleting a Role by id
        public async Task<bool> DeleteRoleAsync(string roleId, string tokenString)
        {
            //Creating and passing bearer token for authorization
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenString);
            //Checking valid url
            var response = await client.DeleteAsync($"{apiUrl}/{roleId}");
            return (response.StatusCode == HttpStatusCode.OK);
        }

        //Declaring a Method to Get a Role by Id
        public async Task<RoleEdit> GetRoleByIdAsync(string roleId, string tokenString)
        {
            RoleEdit role = null;
            //Creating and passing bearer token for authorization
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenString);
            //Checking valid url
            var response = await client.GetAsync($"{apiUrl}/{roleId}");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                role = JsonConvert.DeserializeObject<RoleEdit>(apiResponse);
            }
            return role;
        }

        //Declaring a Method for Updating a Role by id
        public async Task<bool> UpdateRoleAsync(RoleModification role, string tokenString)
        {
            //Creating and passing bearer token for authorization
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenString);

            StringContent content = new StringContent(JsonConvert.SerializeObject(role), Encoding.UTF8, "application/json");
            //Checking valid url
            var response = await client.PutAsync(apiUrl, content);
            return (response.StatusCode == HttpStatusCode.OK);
        }
    }
}
