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
    public class ShowroomsController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ShowroomsProxy _showroomsService;
        private readonly string _tokenString;

        public ShowroomsController(IHttpContextAccessor httpContextAccessor, ShowroomsProxy showroomsService)
        {
            _httpContextAccessor = httpContextAccessor;
            _showroomsService = showroomsService;
            _tokenString = _httpContextAccessor.HttpContext.Session.GetString("token");
        }

        public IActionResult Index()
        {
            return View("AccessDenied");
        }

        public async Task<IActionResult> GetAllShowrooms()
        {
            try
            {
                if (!string.IsNullOrEmpty(_tokenString))
                {
                    var showrooms = await _showroomsService.GetAllShowroomsAsync(_tokenString);
                    return View(showrooms);
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
                    var showroom = await _showroomsService.GetShowroomByIdAsync(id, _tokenString);
                    return View(showroom);
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
        public async Task<ActionResult> Create(Showroom showroom)
        {
            try
            {
                if (!string.IsNullOrEmpty(_tokenString))
                {
                    if (ModelState.IsValid)
                    {
                        var d = await _showroomsService.AddShowroomAsync(showroom, _tokenString);
                        return RedirectToAction("GetAllShowrooms");
                    }
                    else
                    {
                        return View(showroom);
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
                    var showroom = await _showroomsService.GetShowroomByIdAsync(id, _tokenString);
                    return View(showroom);
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
        public async Task<ActionResult> Edit(int id, Showroom showroom)
        {
            try
            {
                if (!string.IsNullOrEmpty(_tokenString))
                {
                    if (ModelState.IsValid)
                    {
                        var d = await _showroomsService.UpdateShowroomAsync(id, showroom, _tokenString);
                        return RedirectToAction("GetAllShowrooms");
                    }
                    else
                    {
                        return View(showroom);
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
                    var showroom = await _showroomsService.GetShowroomByIdAsync(id, _tokenString);
                    return View(showroom);
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
                    var showroom = await _showroomsService.DeleteShowroomAsync(id, _tokenString);
                    return RedirectToAction("GetAllShowrooms");
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
