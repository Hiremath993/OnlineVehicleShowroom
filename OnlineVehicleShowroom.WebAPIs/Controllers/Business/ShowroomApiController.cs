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
    public class ShowroomApiController : ControllerBase
    {
        private readonly IRepository<Showroom> _repository;

        //Declaring Constructor based Dependency Injection
        public ShowroomApiController(IRepository<Showroom> repository)
        {
            _repository = repository;
        }

        //Declaring a Get method to list all Showrooms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Showroom>>> Get()
        {
            return await _repository.GetAllAsync();
        }

        //Declaring a Get method to search a Showroom by its id
        [HttpGet("{id}")]
        //[Authorize(Roles ="")]
        public async Task<ActionResult<Showroom>> Get(int id)
        {
            var showroom = await _repository.GetAsync(id);

            if (showroom == null)
            {
                return NotFound();
            }

            return showroom;
        }

        //Declaring a Post method to Add a new Showroom
        [HttpPost]
        public async Task<ActionResult<Showroom>> Post([FromBody] Showroom showroom)
        {
            try
            {
                var result = await _repository.AddAsync(showroom);
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

            return CreatedAtAction("Get", new { id = showroom.ShowroomID }, showroom);
        }

        //Declaring a Put method to edit a Showroom detail by its id
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Showroom showroom)
        {
            if (id != showroom.ShowroomID)
            {
                return BadRequest();
            }

            try
            {
                await _repository.UpdateAsync(showroom);
            }
            catch (Exception)
            {
                if (!await ShowroomExistsAsync(id))
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

        //Declaring a Delete method to delete a Showroom by its id
        [HttpDelete("{id}")]
        public async Task<ActionResult<Showroom>> Delete(int id)
        {
            var showroom = await _repository.GetAsync(id);
            if (showroom == null)
            {
                return NotFound();
            }
            try
            {
                showroom = await _repository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return BadRequest(ModelState);
            }
            return showroom;
        }

        //Declaring a helper method
        private async Task<bool> ShowroomExistsAsync(int id)
        {
            var showrooms = await _repository.GetAllAsync();
            return showrooms.Any(s => s.ShowroomID == id);
        }
    }
}
