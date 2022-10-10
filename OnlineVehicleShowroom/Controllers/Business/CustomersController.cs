using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineVehicleShowroom.Entities.Business;
using OnlineVehicleShowroom.Proxies.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVehicleShowroom.Controllers.Business
{
    public class CustomersController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly CustomersProxy _customersService;
        private readonly string _tokenString;

        public CustomersController(IHttpContextAccessor httpContextAccessor, CustomersProxy customersService)
        {
            _httpContextAccessor = httpContextAccessor;
            _customersService = customersService;
            _tokenString = _httpContextAccessor.HttpContext.Session.GetString("token");
        }

        public IActionResult Index()
        {
            return View("AccessDenied");
        }

        public async Task<IActionResult> GetAllCustomers()
        {
            try
            {
                if (!string.IsNullOrEmpty(_tokenString))
                {
                    var customers = await _customersService.GetAllCustomersAsync(_tokenString);
                    return View(customers);
                }
                else
                {
                    throw new UnauthorizedAccessException();
                }
            }
            catch (UnauthorizedAccessException)
            {
                return RedirectToAction("Index");
            }
        }

        public async Task<ActionResult> Details(int id)
        {
            try
            {
                if (!string.IsNullOrEmpty(_tokenString))
                {
                    var customer = await _customersService.GetCustomerByIdAsync(id, _tokenString);
                    return View(customer);
                }
                else
                {
                    throw new UnauthorizedAccessException();
                }
            }
            catch (UnauthorizedAccessException)
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult Create()
        {
            try
            {
                if (!string.IsNullOrEmpty(_tokenString))
                {
                    return View();
                }
                else
                {
                    throw new UnauthorizedAccessException();
                }
            }
            catch (UnauthorizedAccessException)
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Customer customer)
        {
            try
            {
                if (!string.IsNullOrEmpty(_tokenString))
                {
                    if (ModelState.IsValid)
                    {
                        var d = await _customersService.AddCustomerAsync(customer, _tokenString);
                        return RedirectToAction("GetAllCustomers");
                    }
                    else
                    {
                        return View(customer);
                    }
                }
                else
                {
                    throw new UnauthorizedAccessException();
                }
            }
            catch (UnauthorizedAccessException)
            {
                return RedirectToAction("Index");
            }
        }

        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                if (!string.IsNullOrEmpty(_tokenString))
                {
                    var customer = await _customersService.GetCustomerByIdAsync(id, _tokenString);
                    return View(customer);
                }
                else
                {
                    throw new UnauthorizedAccessException();
                }
            }
            catch (UnauthorizedAccessException)
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Customer customer)
        {
            try
            {
                if (!string.IsNullOrEmpty(_tokenString))
                {
                    if (ModelState.IsValid)
                    {
                        var d = await _customersService.UpdateCustomerAsync(id, customer, _tokenString);
                        return RedirectToAction("GetAllCustomers");
                    }
                    else
                    {
                        return View(customer);
                    }
                }
                else
                {
                    throw new UnauthorizedAccessException();
                }
            }
            catch (UnauthorizedAccessException)
            {
                return RedirectToAction("Index");
            }
        }

        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (!string.IsNullOrEmpty(_tokenString))
                {
                    var customer = await _customersService.GetCustomerByIdAsync(id, _tokenString);
                    return View(customer);
                }
                else
                {
                    throw new UnauthorizedAccessException();
                }
            }
            catch (UnauthorizedAccessException)
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                if (!string.IsNullOrEmpty(_tokenString))
                {
                    var customer = await _customersService.DeleteCustomerAsync(id, _tokenString);
                    return RedirectToAction("GetAllCustomers");
                }
                else
                {
                    throw new UnauthorizedAccessException();
                }
            }
            catch (UnauthorizedAccessException)
            {
                return RedirectToAction("Index");
            }
        }
    }
}
