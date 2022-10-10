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
    [Authorize(Roles = Roles.ADMINS)]
    public class CustomerApiController : ControllerBase
    {
        private readonly IRepository<Customer> _repository;

        //Declaring Constructor based Dependency Injection
        public CustomerApiController(IRepository<Customer> repository)
        {
            _repository = repository;
        }

        //Declaring a Get method to list all Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> Get()
        {
            return await _repository.GetAllAsync();
        }

        //Declaring a Get method to search a Customer by its id
        [HttpGet("{id}")]
        //[Authorize(Roles ="")]
        public async Task<ActionResult<Customer>> Get(int id)
        {
            var customer = await _repository.GetAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        //Declaring a Post method to add a new Customer
        [HttpPost]
        public async Task<ActionResult<Customer>> Post([FromBody] Customer customer)
        {
            try
            {
                var result = await _repository.AddAsync(customer);
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

            return CreatedAtAction("Get", new { id = customer.CustomerID }, customer);
        }

        //Declaring a Put method to edit a Customer details
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Customer customer)
        {
            if (id != customer.CustomerID)
            {
                return BadRequest();
            }

            try
            {
                await _repository.UpdateAsync(customer);
            }
            catch (Exception)
            {
                if (!await CustomerExistsAsync(id))
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

        //Declaring a Delete method to delete a Customer by its id
        [HttpDelete("{id}")]
        public async Task<ActionResult<Customer>> Delete(int id)
        {
            var customer = await _repository.GetAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            try
            {
                customer = await _repository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return BadRequest(ModelState);
            }
            return customer;
        }

        //Declaring a helper method
        private async Task<bool> CustomerExistsAsync(int id)
        {
            var customers = await _repository.GetAllAsync();
            return customers.Any(c => c.CustomerID == id);
        }
    }
}
