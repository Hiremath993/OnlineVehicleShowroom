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
    public class DealersController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly DealersProxy _dealersService;
        private readonly string _tokenString;

        public DealersController(IHttpContextAccessor httpContextAccessor, DealersProxy dealersService)
        {
            _httpContextAccessor = httpContextAccessor;
            _dealersService = dealersService;
            _tokenString = _httpContextAccessor.HttpContext.Session.GetString("token");
        }

        public IActionResult Index()
        {
            return View("AccessDenied");
        }

        public async Task<IActionResult> GetAllDealers()
        {
            try
            {
                if (!string.IsNullOrEmpty(_tokenString))
                {
                    var dealers = await _dealersService.GetAllDealersAsync(_tokenString);
                    return View(dealers);
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
                    var dealer = await _dealersService.GetDealerByIdAsync(id, _tokenString);
                    return View(dealer);
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
        public async Task<ActionResult> Create(Dealer dealer)
        {
            try
            {
                if (!string.IsNullOrEmpty(_tokenString))
                {
                    if (ModelState.IsValid)
                    {
                        var d = await _dealersService.AddDealerAsync(dealer, _tokenString);
                        return RedirectToAction("GetAllDealers");
                    }
                    else
                    {
                        return View(dealer);
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
                    var dealer = await _dealersService.GetDealerByIdAsync(id, _tokenString);
                    return View(dealer);
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
        public async Task<ActionResult> Edit(int id, Dealer dealer)
        {
            try
            {
                if (!string.IsNullOrEmpty(_tokenString))
                {
                    if (ModelState.IsValid)
                    {
                        var d = await _dealersService.UpdateDealerAsync(id, dealer, _tokenString);
                        return RedirectToAction("GetAllDealers");
                    }
                    else
                    {
                        return View(dealer);
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
                    var dealer = await _dealersService.GetDealerByIdAsync(id, _tokenString);
                    return View(dealer);
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
                    var dealer = await _dealersService.DeleteDealerAsync(id, _tokenString);
                    return RedirectToAction("GetAllDealers");
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
