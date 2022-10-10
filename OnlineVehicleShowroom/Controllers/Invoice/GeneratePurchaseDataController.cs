using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineVehicleShowroom.Entities.Invoice;
using OnlineVehicleShowroom.Proxies.Invoice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVehicleShowroom.Controllers.Invoice
{
    public class GeneratePurchaseDataController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly GeneratePurchaseDataProxy _generatePurchaseDataService;
        private readonly string _tokenString;

        public GeneratePurchaseDataController(IHttpContextAccessor httpContextAccessor, GeneratePurchaseDataProxy generatePurchaseDataService)
        {
            _httpContextAccessor = httpContextAccessor;
            _generatePurchaseDataService = generatePurchaseDataService;
            _tokenString = _httpContextAccessor.HttpContext.Session.GetString("token");
        }

        public IActionResult Index()
        {
            return View("AccessDenied");
        }

        public async Task<ActionResult> Details(int id)
        {
            try
            {
                if (!string.IsNullOrEmpty(_tokenString))
                {
                    var generatePurchaseData = await _generatePurchaseDataService.GetPurchaseDataByIdAsync(id, _tokenString);
                    return View("Details", generatePurchaseData);
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

        public async Task<IActionResult> GetAllGeneratePurchaseData()
        {
            try
            {
                if (!string.IsNullOrEmpty(_tokenString))
                {
                    var generatePurchaseData = await _generatePurchaseDataService.GetAllGeneratePurchaseDataAsync(_tokenString);
                    return View(generatePurchaseData);
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
        public async Task<ActionResult> Create(GeneratePurchaseData generatePurchaseData)
        {
            try
            {
                if (!string.IsNullOrEmpty(_tokenString))
                {
                    if (ModelState.IsValid)
                    {
                        var d = await _generatePurchaseDataService.AddGeneratePurchaseDataAsync(generatePurchaseData, _tokenString);
                        return RedirectToAction("GetAllGeneratePurchaseData");
                    }
                    else
                    {
                        return View(generatePurchaseData);
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
                    var generatePurchaseData = await _generatePurchaseDataService.GetPurchaseDataByIdAsync(id, _tokenString);
                    return View(generatePurchaseData);
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
        public async Task<ActionResult> Edit(int id, GeneratePurchaseData generatePurchaseData)
        {
            try
            {
                if (!string.IsNullOrEmpty(_tokenString))
                {
                    if (ModelState.IsValid)
                    {
                        var d = await _generatePurchaseDataService.UpdateGeneratePurchaseDataAsync(id, generatePurchaseData, _tokenString);
                        return RedirectToAction("GetAllGeneratePurchaseData");
                    }
                    else
                    {
                        return View(generatePurchaseData);
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
                    var generatePurchaseData = await _generatePurchaseDataService.GetPurchaseDataByIdAsync(id, _tokenString);
                    return View(generatePurchaseData);
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
                    var generatePurchaseData = await _generatePurchaseDataService.DeleteGeneratePurchaseDataAsync(id, _tokenString);
                    return RedirectToAction("GetAllGeneratePurchaseData");
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
