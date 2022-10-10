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
    public class SalesController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly SalesProxy _salesService;
        private readonly string _tokenString;

        public SalesController(IHttpContextAccessor httpContextAccessor, SalesProxy salesService)
        {
            _httpContextAccessor = httpContextAccessor;
            _salesService = salesService;
            _tokenString = _httpContextAccessor.HttpContext.Session.GetString("token");
        }

        public IActionResult Index()
        {
            return View("AccessDenied");
        }

        public async Task<IActionResult> GetAllSales()
        {
            try
            {
                if (!string.IsNullOrEmpty(_tokenString))
                {
                    var sales = await _salesService.GetAllSalesAsync(_tokenString);
                    return View(sales);
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
                    var sales = await _salesService.GetSalesByIdAsync(id, _tokenString);
                    return View(sales);
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
        public async Task<ActionResult> Create(Sales sales)
        {
            try
            {
                if (!string.IsNullOrEmpty(_tokenString))
                {
                    if (ModelState.IsValid)
                    {
                        var d = await _salesService.AddSalesAsync(sales, _tokenString);
                        return RedirectToAction("GetAllSales");
                    }
                    else
                    {
                        return View(sales);
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
                    var sales = await _salesService.GetSalesByIdAsync(id, _tokenString);
                    return View(sales);
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
        public async Task<ActionResult> Edit(int id, Sales sales)
        {
            try
            {
                if (!string.IsNullOrEmpty(_tokenString))
                {
                    if (ModelState.IsValid)
                    {
                        var d = await _salesService.UpdateSalesAsync(id, sales, _tokenString);
                        return RedirectToAction("GetAllSales");
                    }
                    else
                    {
                        return View(sales);
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
                    var sales = await _salesService.GetSalesByIdAsync(id, _tokenString);
                    return View(sales);
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
                    var sales = await _salesService.DeleteSalesAsync(id, _tokenString);
                    return RedirectToAction("GetAllSales");
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
