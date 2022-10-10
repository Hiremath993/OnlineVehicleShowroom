using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineVehicleShowroom.Entities.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVehicleShowroom.WebAPIs.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = Roles.ADMINS)]
    public class RolesApiController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        //Declaring Constructor based Dependency Injection
        public RolesApiController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        //Declaring a Get method to list all roles
        [HttpGet]
        public async Task<IEnumerable<IdentityRole>> GetRoles()
        {
            return await Task.Run<IEnumerable<IdentityRole>>(() => _roleManager.Roles);
        }

        //Declaring a Post method to add a new role
        [HttpPost("{name}")]
        public async Task<IActionResult> PostRole([Required][FromRoute] string name)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(name));
                if (result.Succeeded)
                {
                    return Ok();
                }
                else
                {
                    await Errors(result);
                }
            }
            return BadRequest(ModelState);
        }

        //Declaring a Delete method to delete a role by its id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                var result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return Ok();
                }
                else
                {
                    await Errors(result);
                    return BadRequest(_roleManager.Roles);
                }
            }
            else
            {
                return NotFound();
            }
        }

        //Declaring a Get method to search a role by its id
        [HttpGet("{id}")]
        public async Task<ActionResult<RoleEdit>> GetRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            List<IdentityUser> members = new List<IdentityUser>();
            List<IdentityUser> nonMembers = new List<IdentityUser>();
            foreach (var user in _userManager.Users)
            {
                var list = await _userManager.IsInRoleAsync(user, role.Name) ? members : nonMembers;
                list.Add(user);
            }
            return Ok(new RoleEdit()
            {
                Role = role,
                Members = members,
                NonMembers = nonMembers
            });
        }

        //Declaring a Put method to edit a role
        [HttpPut]
        public async Task<IActionResult> PutRole([FromBody] RoleModification model)
        {
            IdentityResult result = null;
            if (ModelState.IsValid)
            {
                foreach (var userid in model.AddIds ?? new string[] { })
                {
                    var user = await _userManager.FindByIdAsync(userid);
                    if (user != null)
                    {
                        result = await _userManager.AddToRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                        {
                            await Errors(result);
                        }
                    }
                }

                foreach (var userid in model.DeleteIds ?? new string[] { })
                {
                    var user = await _userManager.FindByIdAsync(userid);
                    if (user != null)
                    {
                        result = await _userManager.RemoveFromRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                        {
                            await Errors(result);
                        }
                    }
                }
            }
            if (ModelState.IsValid)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        //Declaring a helper method
        private async Task Errors(IdentityResult result)
        {
            await Task.Run(() =>
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            });
        }
    }
}
