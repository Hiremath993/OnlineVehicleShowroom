using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OnlineVehicleShowroom.Models;
using OnlineVehicleShowroom.Proxies.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVehicleShowroom.Controllers.Auth
{
    public class AuthController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthProxy _authService;

        // Defining a constructor for Dependency Injection
        public AuthController(IHttpContextAccessor httpContextAccessor, AuthProxy authService)
        {
            _httpContextAccessor = httpContextAccessor;
            _authService = authService;
        }

        // Returning the View for Regsiter for handling Get Request
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // Returning the View for Regsiter for handling Post Request
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            // Checking for valid data
            if (ModelState.IsValid)
            {
                var result = await _authService.RegisterUserAsync(model);
                if (result)
                {
                    return View("Success");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "User creation failed!");
                }
            }
            return View(model);
        }

        // Returning the View for Login for handling Get Request
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // Returning the View for Login for handling Post Request
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var credentials = new
                {
                    UserName = model.EMail,
                    Password = model.Password
                };
                // Checking for valid credentials and tokenString & if valid redirect to Home
                var token = await _authService.LoginAsync(credentials);
                var tokenString = JsonConvert.DeserializeObject<JObject>(token).SelectToken("token").ToString();
                _httpContextAccessor.HttpContext.Session.SetString("token", tokenString);
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        // Returning the View for Logout
        [HttpPost]
        public IActionResult Logout()
        {
            _httpContextAccessor.HttpContext.Session.Clear();
            return View();
        }
    }
}
