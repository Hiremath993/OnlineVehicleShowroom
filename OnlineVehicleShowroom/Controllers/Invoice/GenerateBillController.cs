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
    public class GenerateBillController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly GenerateBillProxy _generateBillService;
        private readonly string _tokenString;

        public GenerateBillController(IHttpContextAccessor httpContextAccessor, GenerateBillProxy generateBillService)
        {
            _httpContextAccessor = httpContextAccessor;
            _generateBillService = generateBillService;
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
                    var generateBill = await _generateBillService.GetBillByIdAsync(id, _tokenString);
                    return View("Details",generateBill);
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

        public async Task<IActionResult> GetAllGenerateBill()
        {
            try
            {
                if (!string.IsNullOrEmpty(_tokenString))
                {
                    var generateBill = await _generateBillService.GetAllGenerateBillAsync(_tokenString);
                    return View(generateBill);
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
        public async Task<ActionResult> Create(GenerateBill generateBill)
        {
            try
            {
                if (!string.IsNullOrEmpty(_tokenString))
                {
                    if (ModelState.IsValid)
                    {
                        var d = await _generateBillService.AddGenerateBillAsync(generateBill, _tokenString);
                        return RedirectToAction("GetAllGenerateBill");
                    }
                    else
                    {
                        return View(generateBill);
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
                    var generateBill = await _generateBillService.GetBillByIdAsync(id, _tokenString);
                    return View(generateBill);
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
        public async Task<ActionResult> Edit(int id, GenerateBill generateBill)
        {
            try
            {
                if (!string.IsNullOrEmpty(_tokenString))
                {
                    if (ModelState.IsValid)
                    {
                        var d = await _generateBillService.UpdateGenerateBillAsync(id, generateBill, _tokenString);
                        return RedirectToAction("GetAllGenerateBill");
                    }
                    else
                    {
                        return View(generateBill);
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
                    var generateBill = await _generateBillService.GetBillByIdAsync(id, _tokenString);
                    return View(generateBill);
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
                    var generateBill = await _generateBillService.DeleteGenerateBillAsync(id, _tokenString);
                    return RedirectToAction("GetAllGenerateBill");
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
