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
    public class SalesApiController : ControllerBase
    {
        private readonly IRepository<Sales> _repository;

        //Declaring Constructor based Dependency Injection
        public SalesApiController(IRepository<Sales> repository)
        {
            _repository = repository;
        }

        //Declaring a Get method to list all Sales
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sales>>> Get()
        {
            return await _repository.GetAllAsync();
        }

        //Declaring a Get method to search a Sales by its id
        [HttpGet("{id}")]
        //[Authorize(Roles ="")]
        public async Task<ActionResult<Sales>> Get(int id)
        {
            var sales = await _repository.GetAsync(id);

            if (sales == null)
            {
                return NotFound();
            }

            return sales;
        }

        //Declaring a Post method to Add a new Sales
        [HttpPost]
        public async Task<ActionResult<Sales>> Post([FromBody] Sales sales)
        {
            try
            {
                var result = await _repository.AddAsync(sales);
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

            return CreatedAtAction("Get", new { id = sales.SalesID }, sales);
        }

        //Declaring a Put method to edit a Sales detail by its id
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Sales sales)
        {
            if (id != sales.SalesID)
            {
                return BadRequest();
            }

            try
            {
                await _repository.UpdateAsync(sales);
            }
            catch (Exception)
            {
                if (!await SalesExistsAsync(id))
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

        //Declaring a Delete method to delete a Sales by its id
        [HttpDelete("{id}")]
        public async Task<ActionResult<Sales>> Delete(int id)
        {
            var sales = await _repository.GetAsync(id);
            if (sales == null)
            {
                return NotFound();
            }
            try
            {
                sales = await _repository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return BadRequest(ModelState);
            }
            return sales;
        }

        //Declaring a helper method
        private async Task<bool> SalesExistsAsync(int id)
        {
            var sales = await _repository.GetAllAsync();
            return sales.Any(s => s.SalesID == id);
        }
    }
}
