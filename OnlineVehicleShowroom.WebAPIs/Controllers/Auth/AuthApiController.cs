using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OnlineVehicleShowroom.Entities.Auth;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVehicleShowroom.WebAPIs.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthApiController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        //Declaring Constructor based Dependency Injection
        public AuthApiController(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        //Declaring asynchronous method for Registering a new user
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Register model)
        {
            //searching the email to verify if it already exists
            var userExists = await _userManager.FindByNameAsync(model.EMail);

            //if user exists
            if (userExists != null)
            {
                return StatusCode(StatusCodes.Status409Conflict,
                    new Response() { Status = "Error", Message = "User already exists!" });
            }

            //creating new user
            var user = new ApplicationUser()
            {
                Email = model.EMail,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.EMail
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            //if user registration failed
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response() { Status = "Error", Message = "User creation failed! Please check user details and try again." });
            }

            //if role does not exists
            if (!await _roleManager.RoleExistsAsync(Roles.CUSTOMERS))
            {
                await _roleManager.CreateAsync(new IdentityRole(Roles.CUSTOMERS));
            }
            //adding role
            await _userManager.AddToRoleAsync(user, Roles.CUSTOMERS);
            return Ok(new Response() { Status = "Success", Message = "User created successfully!" });
        }

        //Declaring asynchronous method for Logging in an user
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login model)
        {
            //searching to check if the user exists or not
            var user = await _userManager.FindByNameAsync(model.UserName);

            //if user exists
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                //adding expiry time
                var expiry = DateTime.UtcNow.AddHours(5).AddMinutes(30).AddMinutes(20);

                //adding list of claims
                var authClaims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Expiration, expiry.ToString())
                };
                foreach (var role in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }
                //creating token
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: expiry,
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    claims = token.Claims
                });
            }

            return Unauthorized();
        }
    }
}
