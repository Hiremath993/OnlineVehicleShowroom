using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineVehicleShowroom.Entities.Auth;
using OnlineVehicleShowroom.Entities.Business;
using OnlineVehicleShowroom.Entities.Invoice;
using OnlineVehicleShowroom.Exceptions;
using OnlineVehicleShowroom.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVehicleShowroom.WebAPIs.Controllers.Invoice
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admins,Customers")]
    public class GeneratePurchaseDataApiController : ControllerBase
    {
        private readonly IRepository<Vehicle> _repositoryVehicle;
        private readonly IRepository<Showroom> _repositoryShowroom;
        private readonly IRepository<Sales> _repositorySales;
        private readonly IRepository<Customer> _repositoryCustomer;
        private readonly IRepository<GeneratePurchaseData> _repositoryGeneratePurchaseData;

        //Declaring Constructor based Dependency Injection
        public GeneratePurchaseDataApiController(IRepository<Vehicle> repositoryVehicle, IRepository<Showroom> repositoryShowroom, IRepository<Sales> repositorySales, IRepository<Customer> repositoryCustomer, IRepository<GeneratePurchaseData> repositoryGeneratePurchaseData)
        {
            _repositoryVehicle = repositoryVehicle;
            _repositoryShowroom = repositoryShowroom;
            _repositorySales = repositorySales;
            _repositoryCustomer = repositoryCustomer;
            _repositoryGeneratePurchaseData = repositoryGeneratePurchaseData;
        }


        // GET: GeneratePurchaseDataApiController/Details/5
        //[HttpGet("{id}")]
        //public async Task<IEnumerable<ActionResult>> DetailsAsync(int id)
        //{
        //    var vehicles = await _repositoryVehicle.GetAllAsync();
        //    var showrooms = await _repositoryShowroom.GetAllAsync();
        //    var sales = await _repositorySales.GetAllAsync();
        //    var customers = await _repositoryCustomer.GetAllAsync();

        //    var result = (from Sa in sales
        //                  join V in vehicles on Sa.VehicleID equals V.VehicleID
        //                  join Sh in showrooms on Sa.ShowroomID equals Sh.ShowroomID
        //                  join C in customers on Sa.CustomerID equals C.CustomerID
        //                  where Sa.CustomerID == id
        //                  select (Sa.SalesID, V.VehicleID, C.CustomerID, C.CustomerName, Sh.Name, Sa.Cost, Sa.OrderDate, Sa.DeliveryDate)).ToList();

        //    if (result == null)
        //    {
        //        return (IEnumerable<ActionResult>)NotFound();
        //    }
        //    return (IEnumerable<ActionResult>)result;
        //}

        //Declaring a Get method to search a Generated Purchase Data by its id
        [HttpGet("{id}")]
        //[Authorize(Roles ="")]
        public async Task<ActionResult<GeneratePurchaseData>> Get(int id)
        {
            var generatePurchaseData = await _repositoryGeneratePurchaseData.GetAsync(id);

            if (generatePurchaseData == null)
            {
                return NotFound();
            }

            return generatePurchaseData;
        }

        //Declaring a Get method to list all Generated Purchase Data
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GeneratePurchaseData>>> Get()
        {
            return await _repositoryGeneratePurchaseData.GetAllAsync();
        }


        //Declaring a Post method to Add a new Purchase Data
        [HttpPost]
        public async Task<ActionResult<GeneratePurchaseData>> Post([FromBody] GeneratePurchaseData generatePurchaseData)
        {
            try
            {
                var result = await _repositoryGeneratePurchaseData.AddAsync(generatePurchaseData);
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

            return CreatedAtAction("Get", new { id = generatePurchaseData.PurchaseID }, generatePurchaseData);
        }

        //Declaring a Put method to edit a Generated Purchase Data detail by its id
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] GeneratePurchaseData generatePurchaseData)
        {
            if (id != generatePurchaseData.PurchaseID)
            {
                return BadRequest();
            }

            try
            {
                await _repositoryGeneratePurchaseData.UpdateAsync(generatePurchaseData);
            }
            catch (Exception)
            {
                if (!await GeneratePurchaseDataExistsAsync(id))
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

        //Declaring a Delete method to delete a Generated Purchase Data by its id
        [HttpDelete("{id}")]
        public async Task<ActionResult<GeneratePurchaseData>> Delete(int id)
        {
            var generatePurchaseData = await _repositoryGeneratePurchaseData.GetAsync(id);
            if (generatePurchaseData == null)
            {
                return NotFound();
            }
            try
            {
                generatePurchaseData = await _repositoryGeneratePurchaseData.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return BadRequest(ModelState);
            }
            return generatePurchaseData;
        }

        //Declaring a helper method
        private async Task<bool> GeneratePurchaseDataExistsAsync(int id)
        {
            var generatePurchaseData = await _repositoryGeneratePurchaseData.GetAllAsync();
            return generatePurchaseData.Any(s => s.PurchaseID == id);
        }
    }
}
