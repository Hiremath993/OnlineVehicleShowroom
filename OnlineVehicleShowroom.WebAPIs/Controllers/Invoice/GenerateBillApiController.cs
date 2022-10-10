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
    public class GenerateBillApiController : ControllerBase
    {
        private readonly IRepository<Vehicle> _repositoryVehicle;
        private readonly IRepository<Showroom> _repositoryShowroom;
        private readonly IRepository<GenerateBill> _repositoryGenerateBill;
        private readonly IRepository<Customer> _repositoryCustomer;
        private readonly IRepository<Sales> _repositorySales;

        //Declaring Constructor based Dependency Injection
        public GenerateBillApiController(IRepository<Vehicle> repositoryVehicle, IRepository<Showroom> repositoryShowroom, IRepository<GenerateBill> repositoryGenerateBill, IRepository<Customer> repositoryCustomer, IRepository<Sales> repositorySales)
        {
            _repositoryVehicle = repositoryVehicle;
            _repositoryShowroom = repositoryShowroom;
            _repositoryGenerateBill = repositoryGenerateBill;
            _repositoryCustomer = repositoryCustomer;
            _repositorySales = repositorySales;
        }


        // GET: GenerateBillApiController/Details/5
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
        //                  where Sa.SalesID == id
        //                  select (Sa.SalesID, V.VehicleID, C.CustomerName, Sh.Name, Sa.Cost, Sa.OrderDate, Sa.DeliveryDate)).ToList();

        //    if (result == null)
        //    {
        //        return (IEnumerable<ActionResult>)NotFound();
        //    }
        //    return (IEnumerable<ActionResult>)result;
        //}

        //Declaring a Get method to search a Generated Bill by its id
        [HttpGet("{id}")]
        //[Authorize(Roles ="")]
        public async Task<ActionResult<GenerateBill>> Get(int id)
        {
            var generateBill = await _repositoryGenerateBill.GetAsync(id);

            if (generateBill == null)
            {
                return NotFound();
            }

            return generateBill;
        }

        //Declaring a Get method to list all Generated Bills
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GenerateBill>>> Get()
        {
            return await _repositoryGenerateBill.GetAllAsync();
        }


        //Declaring a Post method to Add a new Bill
        [HttpPost]
        public async Task<ActionResult<GenerateBill>> Post([FromBody] GenerateBill generateBill)
        {
            try
            {
                var result = await _repositoryGenerateBill.AddAsync(generateBill);
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

            return CreatedAtAction("Get", new { id = generateBill.OrderID }, generateBill);
        }

        //Declaring a Put method to edit a Generated Bill detail by its id
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] GenerateBill generateBill)
        {
            if (id != generateBill.OrderID)
            {
                return BadRequest();
            }

            try
            {
                await _repositoryGenerateBill.UpdateAsync(generateBill);
            }
            catch (Exception)
            {
                if (!await GenerateBillExistsAsync(id))
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

        //Declaring a Delete method to delete a Generated Bill by its id
        [HttpDelete("{id}")]
        public async Task<ActionResult<GenerateBill>> Delete(int id)
        {
            var generateBill = await _repositoryGenerateBill.GetAsync(id);
            if (generateBill == null)
            {
                return NotFound();
            }
            try
            {
                generateBill = await _repositoryGenerateBill.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return BadRequest(ModelState);
            }
            return generateBill;
        }

        //Declaring a helper method
        private async Task<bool> GenerateBillExistsAsync(int id)
        {
            var generateBill = await _repositoryGenerateBill.GetAllAsync();
            return generateBill.Any(s => s.OrderID == id);
        }
    }
}
