using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineVehicleShowroom.Entities.Business;
using OnlineVehicleShowroom.Proxies.Business;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Grpc.Core;
using OnlineVehicleShowroom.Models;
using OnlineVehicleShowroom.DataAccessLayer;
using Microsoft.AspNetCore.Hosting;

namespace OnlineVehicleShowroom.Controllers.Business
{
    public class VehiclesController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly VehiclesProxy _vehiclesService;
        private readonly string _tokenString;
        private readonly OVSDbContext _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public VehiclesController(IHttpContextAccessor httpContextAccessor, VehiclesProxy vehiclesService, OVSDbContext dbContext, IWebHostEnvironment webHostEnvironment)
        {
            _httpContextAccessor = httpContextAccessor;
            _vehiclesService = vehiclesService;
            _tokenString = _httpContextAccessor.HttpContext.Session.GetString("token");
            _dbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View("AccessDenied");
        }

        public async Task<IActionResult> GetAllVehicles()
        {
            try
            {
                if (!string.IsNullOrEmpty(_tokenString))
                {
                    var vehicles = await _vehiclesService.GetAllVehiclesAsync(_tokenString);
                    return View(vehicles);
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

        public async Task<ActionResult> DetailsByID(int id)
        {
            try
            {
                if (!string.IsNullOrEmpty(_tokenString))
                {
                    var vehicle = await _vehiclesService.GetVehicleByIdAsync(id, _tokenString);
                    return View(vehicle);
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

        public async Task<ActionResult> DetailsByName(string id)
        {
            try
            {
                if (!string.IsNullOrEmpty(_tokenString))
                {
                    var vehicle = await _vehiclesService.GetVehicleByNameAsync(id, _tokenString);
                    return View("GetAllVehicles",vehicle);
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

        public async Task<ActionResult> DetailsByModel(string id)
        {
            try
            {
                if (!string.IsNullOrEmpty(_tokenString))
                {
                    var vehicle=await _vehiclesService.GetVehicleByModelAsync(id, _tokenString);                     
                    return View("GetAllVehicles",vehicle);
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

        public async Task<ActionResult> DetailsByDealer(int id)
        {
            try
            {
                if (!string.IsNullOrEmpty(_tokenString))
                {
                    var vehicle = await _vehiclesService.GetVehicleByDealerAsync(id, _tokenString);
                    return View("GetAllVehicles",vehicle);
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

        public async Task<ActionResult> DetailsByShowroom(string id)
        {
            try
            {
                if (!string.IsNullOrEmpty(_tokenString))
                {
                    var vehicle = await _vehiclesService.GetVehicleByShowroomAsync(id, _tokenString);
                    return View("GetAllVehicles", vehicle);
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

        public async Task<ActionResult> DetailsByOrderDate(DateTime id)
        {
            try
            {
                if (!string.IsNullOrEmpty(_tokenString))
                {
                    var vehicle = await _vehiclesService.GetVehicleByOrderDateAsync(id, _tokenString);
                    return View("GetAllVehicles", vehicle);
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

        public async Task<ActionResult> DetailsByRating(double id)
        {
            try
            {
                if (!string.IsNullOrEmpty(_tokenString))
                {
                    var vehicle = await _vehiclesService.GetVehicleByRatingAsync(id, _tokenString);
                    return View("GetAllVehicles", vehicle);
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
        public async Task<ActionResult> Create(Vehicle vehicle)
        {
            try
            {
                if (!string.IsNullOrEmpty(_tokenString))
                {
                    if (ModelState.IsValid)
                    {
                        var d = await _vehiclesService.AddVehicleAsync(vehicle, _tokenString);
                        return RedirectToAction("GetAllVehicles");
                    }
                    else
                    {
                        return View(vehicle);
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
                    var vehicle = await _vehiclesService.GetVehicleByIdAsync(id, _tokenString);
                    return View(vehicle);
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
        public async Task<ActionResult> Edit(int id, Vehicle vehicle)
        {
            try
            {
                if (!string.IsNullOrEmpty(_tokenString))
                {
                    if (ModelState.IsValid)
                    {
                        var d = await _vehiclesService.UpdateVehicleAsync(id, vehicle, _tokenString);
                        return RedirectToAction("GetAllVehicles");
                    }
                    else
                    {
                        return View(vehicle);
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
                    var vehicle = await _vehiclesService.GetVehicleByIdAsync(id, _tokenString);
                    return View(vehicle);
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
                    var vehicle = await _vehiclesService.DeleteVehicleAsync(id, _tokenString);
                    return RedirectToAction("GetAllVehicles");
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


        public IActionResult New()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(VehicleViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadedFile(model);

                Vehicle vehicle = new Vehicle
                {
                    VehicleID = model.VehicleID,
                    VehicleName = model.VehicleName,
                    VehicleModel = model.VehicleModel,
                    Cost = model.Cost,
                    DealerID = model.DealerID,
                    Description = model.Description,
                    Rating = model.Rating,
                    TotalStock = model.TotalStock,
                    VehicleImage = uniqueFileName,
                };
                _dbContext.Add(vehicle);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction("GetAllVehicles");
            }
            return View();
        }

        private string UploadedFile(VehicleViewModel model)
        {
            string uniqueFileName = null;

            if (model.VehiclePicture != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.VehiclePicture.FileName;
                //uniqueFileName = model.VehiclePicture.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.VehiclePicture.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}
