using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineVehicleShowroom.Entities.Auth;
using OnlineVehicleShowroom.Entities.Business;
using OnlineVehicleShowroom.Exceptions;
using OnlineVehicleShowroom.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVehicleShowroom.WebAPIs.Controllers.Business
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admins,Customers")]
    public class VehicleApiController : ControllerBase
    {
        private readonly IRepository<Vehicle> _repository;
        private readonly IRepository<Showroom> _repositoryShowroom;
        private readonly IRepository<Sales> _repositorySales;

        //Declaring Constructor based Dependency Injection
        public VehicleApiController(IRepository<Vehicle> repository, IRepository<Showroom> repositoryShowroom, IRepository<Sales> repositorySales)
        {
            _repository = repository;
            _repositoryShowroom = repositoryShowroom;
            _repositorySales = repositorySales;
        }

        //Declaring a Get method to list all Vehicles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vehicle>>> Get()
        {
            return await _repository.GetAllAsync();
        }

        //Declaring a Get method to search a Vehicle by its id
        [HttpGet("[action]/{vehicleId}")]
        public async Task<ActionResult<Vehicle>> GetByVehicleID(int vehicleId)
        {
            var vehicle = await _repository.GetAsync(vehicleId);

            if (vehicle == null)
            {
                return NotFound();
            }

            return vehicle;
        }

        //Declaring a Get method to search a Vehicle by its name
        [HttpGet("[action]/{vehicleName}")]
        public async Task<ActionResult<IEnumerable<Vehicle>>> GetByVehicleName(string vehicleName)
        {
            var vehicles = await _repository.GetAllAsync();

            var result = from v in vehicles
                         where v.VehicleName == vehicleName
                         select v.VehicleID;

            List<Vehicle> vehicle = new List<Vehicle>();
            foreach (var item in result)
            {
                vehicle.Add(await _repository.GetAsync(item));
            }

            if (vehicle == null)
            {
                return NotFound();
            }

            return vehicle;
        }

        //Declaring a Get method to search a Vehicle by its showroom
        [HttpGet("[action]/{showroomName}")]
        public async Task<ActionResult<IEnumerable<Vehicle>>> GetByShowroom(string showroomName)
        {
            var vehicles = await _repository.GetAllAsync();
            var showrooms = await _repositoryShowroom.GetAllAsync();

            var result = (from v in vehicles
                         join s in showrooms
                         on v.DealerID equals s.DealerID
                         where s.Name == showroomName
                         select v.VehicleID).ToList();

            List<Vehicle> vehicle = new List<Vehicle>();

            foreach (var item in result)
            {
                vehicle.Add(await _repository.GetAsync(item));
            }
            if (vehicle == null)
            {
                return NotFound();
            }
            return vehicle;
        }

        //Declaring a Get method to search a Vehicle by its dealer id
        [HttpGet("[action]/{dealerId}")]
        public async Task<ActionResult<IEnumerable<Vehicle>>> GetByDealerID(int dealerId)
        {
            var vehicles = await _repository.GetAllAsync();
            var showrooms = await _repositoryShowroom.GetAllAsync();

            var result = from v in vehicles
                         where v.DealerID == dealerId
                         select v.VehicleID;

            List<Vehicle> vehicle = new List<Vehicle>();

            foreach (var item in result)
            {
                vehicle.Add(await _repository.GetAsync(item));
            }
            if (vehicle == null)
            {
                return NotFound();
            }
            return vehicle;
        }

        //Declaring a Get method to search a Vehicle by its model
        [HttpGet("[action]/{model}")]
        public async Task<ActionResult<IEnumerable<Vehicle>>> GetByVehicleModel(string model)
        {
            var vehicles = await _repository.GetAllAsync();

            var result = from v in vehicles
                         where v.VehicleModel == model
                         select v.VehicleID;

            List<Vehicle> vehicle = new List<Vehicle>();
            foreach (var item in result)
            {
                vehicle.Add(await _repository.GetAsync(item));
            }

            if (vehicle == null)
            {
                return NotFound();
            }

            return vehicle;
        }

        //Declaring a Get method to search a Vehicle by its order date
        [HttpGet("[action]/{orderDate}")]
        public async Task<ActionResult<IEnumerable<Vehicle>>> GetByOrderDate(DateTime orderDate)
        {
            var vehicles = await _repository.GetAllAsync();
            var sales = await _repositorySales.GetAllAsync();

            var result = (from v in vehicles
                          join s in sales
                          on v.VehicleID equals s.VehicleID
                          where s.OrderDate == orderDate
                          select v.VehicleID).ToList();

            List<Vehicle> vehicle = new List<Vehicle>();

            foreach (var item in result)
            {
                vehicle.Add(await _repository.GetAsync(item));
            }
            if (vehicle == null)
            {
                return NotFound();
            }
            return vehicle;
        }

        //Declaring a Get method to search a Vehicle by its rating
        [HttpGet("[action]/{rating}")]
        public async Task<ActionResult<IEnumerable<Vehicle>>> GetByRating(double rating)
        {
            var vehicles = await _repository.GetAllAsync();

            var result = from v in vehicles
                         where v.Rating == rating
                         select v.VehicleID;

            List<Vehicle> vehicle = new List<Vehicle>();
            foreach (var item in result)
            {
                vehicle.Add(await _repository.GetAsync(item));
            }

            if (vehicle == null)
            {
                return NotFound();
            }

            return vehicle;
        }

        //Declaring a Post method to Add a new Vehicle
        [HttpPost]
        public async Task<ActionResult<Vehicle>> Post([FromBody] Vehicle vehicle)
        {
            try
            {
                var result = await _repository.AddAsync(vehicle);
                if (result == null)
                {
                    return BadRequest();
                }
            }
            catch (OnlineVehicleShowroomException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return BadRequest(ModelState);
            }

            return CreatedAtAction("Get", new { id = vehicle.VehicleID }, vehicle);
        }

        //Declaring a Put method to edit a Vehicle detail by its id
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Vehicle vehicle)
        {
            if (id != vehicle.VehicleID)
            {
                return BadRequest();
            }

            try
            {
                await _repository.UpdateAsync(vehicle);
            }
            catch (Exception)
            {
                if (!await VehicleExistsAsync(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        //Declaring a Delete method to delete a Vehicle by its id
        [HttpDelete("{id}")]
        public async Task<ActionResult<Vehicle>> Delete(int id)
        {
            var vehicle = await _repository.GetAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            try
            {
                vehicle = await _repository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return BadRequest(ModelState);
            }
            return vehicle;
        }

        //Declaring a helper method
        private async Task<bool> VehicleExistsAsync(int id)
        {
            var vehicles = await _repository.GetAllAsync();
            return vehicles.Any(v => v.VehicleID == id);
        }
    }
}
