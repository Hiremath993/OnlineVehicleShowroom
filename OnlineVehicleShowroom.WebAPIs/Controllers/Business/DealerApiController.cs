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
    public class DealerApiController : ControllerBase
    {
        private readonly IRepository<Dealer> _repository;

        //Declaring Constructor based Dependency Injection
        public DealerApiController(IRepository<Dealer> repository)
        {
            _repository = repository;
        }

        //Declaring a Get method to list all Dealers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dealer>>> Get()
        {
            return await _repository.GetAllAsync();
        }

        //Declaring a Get method to search a Dealer by its id
        [HttpGet("{id}")]
        //[Authorize(Roles ="")]
        public async Task<ActionResult<Dealer>> Get(int id)
        {
            var dealer = await _repository.GetAsync(id);

            if (dealer == null)
            {
                return NotFound();
            }

            return dealer;
        }

        //Declaring a Post method to Add a new Dealer
        [HttpPost]
        public async Task<ActionResult<Dealer>> Post([FromBody] Dealer dealer)
        {
            try
            {
                var result = await _repository.AddAsync(dealer);
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

            return CreatedAtAction("Get", new { id = dealer.DealerID }, dealer);
        }

        //Declaring a Put method to edit a Dealer detail by its id
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Dealer dealer)
        {
            if (id != dealer.DealerID)
            {
                return BadRequest();
            }

            try
            {
                await _repository.UpdateAsync(dealer);
            }
            catch (Exception)
            {
                if (!await DealerExistsAsync(id))
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

        //Declaring a Delete method to delete a Dealer by its id
        [HttpDelete("{id}")]
        public async Task<ActionResult<Dealer>> Delete(int id)
        {
            var dealer = await _repository.GetAsync(id);
            if (dealer == null)
            {
                return NotFound();
            }
            try
            {
                dealer = await _repository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return BadRequest(ModelState);
            }
            return dealer;
        }

        //Declaring a helper method
        private async Task<bool> DealerExistsAsync(int id)
        {
            var dealers = await _repository.GetAllAsync();
            return dealers.Any(d => d.DealerID == id);
        }
    }
}
