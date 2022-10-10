using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineVehicleShowroom.Entities.Auth;
using OnlineVehicleShowroom.Proxies.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVehicleShowroom.Controllers.Auth
{
    public class RolesController : Controller
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly RolesProxy _rolesService;
        private readonly string _tokenString;

        // Defining a constructor for Dependency Injection
        public RolesController(IHttpContextAccessor httpContextAccessor, RolesProxy rolesService)
        {
            _httpContextAccessor = httpContextAccessor;
            _rolesService = rolesService;
            _tokenString = _httpContextAccessor.HttpContext.Session.GetString("token");
        }

        public IActionResult Index() => View("AccessDenied");


        [HttpGet]
        public async Task<IActionResult> Roles()
        {
            //Checking for valid tokenString
            //If empty the user is not signed in
            if (!string.IsNullOrEmpty(_tokenString))
            {
                try
                {
                    // if valid call the proxy class
                    var roles = await _rolesService.GetRolesAsync(_tokenString);
                    return View(roles);
                }
                catch (UnauthorizedAccessException)
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create([Required] string name)
        {
            //Checking for valid tokenString and data
            if (ModelState.IsValid && !string.IsNullOrEmpty(_tokenString))
            {
                // if valid call the proxy class 
                var result = await _rolesService.AddRoleAsync(name, _tokenString);
                if (result)
                {
                    return RedirectToAction("Roles");
                }
                else
                {
                    return RedirectToAction("Error", "Home");
                }
            }
            return View(name);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            //Checking for valid tokenString and data
            if (!string.IsNullOrEmpty(_tokenString))
            {
                // if valid call the proxy class 
                var result = await _rolesService.DeleteRoleAsync(id, _tokenString);
                if (result)
                {
                    return RedirectToAction("Roles");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "No role found!");
                    var roles = await _rolesService.GetRolesAsync(_tokenString);
                    return View("Roles", roles);
                }
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            //Checking for valid tokenString and data
            if (!string.IsNullOrEmpty(_tokenString))
            {
                // if valid call the proxy class 
                var role = await _rolesService.GetRoleByIdAsync(id, _tokenString);
                return View(role);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(RoleModification model)
        {
            bool result = false;
            //Checking for valid tokenString and data
            if (ModelState.IsValid && !string.IsNullOrEmpty(_tokenString))
            {
                // if valid call the proxy class 
                result = await _rolesService.UpdateRoleAsync(model, _tokenString);
            }
            if (result)
            {
                return RedirectToAction(nameof(Roles));
            }
            else
            {
                return await Update(model.RoleId);
            }
        }
    }
}
