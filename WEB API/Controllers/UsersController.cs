using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Models;
using DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using WEB_API.Models;
using Microsoft.AspNetCore.Identity;
using IdentityModel.Client;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace WEB_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private ICustomers Customers { get; set; }
        private readonly IMapper mapper;
        private readonly Customers customers = new Customers();
        private readonly User users = new User();
        private readonly UserManager<User> userManager;
        private IConfiguration configuration;
        public UsersController(UserManager<User> userManager, IMapper mapper, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.configuration = configuration;
        }
        /*public CustomersController(ICustomers Customers, IMapper mapper)
        {
            this.Customers = Customers;
            this.mapper = mapper;
        }
        */
        [HttpPost]
        public async Task<ActionResult<User>> Authenticate(APIUser personalInfo)
        {
            if (!ModelState.IsValid)
                return (ValidationProblem(ModelState));
            var user = await userManager.FindByNameAsync(personalInfo.Name);
           // var password = user.PasswordHash.Length;
            //var password = userManager.PasswordHasher.HashPassword(mapper.Map<User>(personalInfo), personalInfo.PasswordHash);
            var isUser = await userManager.CheckPasswordAsync(user, personalInfo.PasswordHash);
            if (user.Name != null && isUser && user.IsValid!="deleted")
            {
                return Ok(user);
            }
            else if(user.IsValid=="deleted")
            {
                return Unauthorized("This user is deleted");
            }
            //ModelState.AddModelError("", "Invalid name or password");

            return Unauthorized("Invalid name or password");
        }

        [HttpPost]
        public async Task<ActionResult> Register(APIUser personalInfo)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);
            var user = await userManager.FindByNameAsync(personalInfo.Name);
            var b = user.Name;
            if (user.Name == null)
            {
                User userToRegister = new User();
                userToRegister.Name = personalInfo.Name;
                userToRegister.Rank = personalInfo.Rank;
                userToRegister.Email = personalInfo.Email;
                var result = await userManager.CreateAsync(userToRegister, personalInfo.PasswordHash);
                if (result.Succeeded)
                {
                    return StatusCode(StatusCodes.Status201Created, new
                    {
                        data = userToRegister
                    });
                }
                ModelState.AddModelError("Registration error", string.Join(' ', result.Errors.SelectMany(item => item.Description).ToList()));
                return ValidationProblem(ModelState);
            }
            else if (user.IsValid == "deleted") return Ok("Account is deleted");
            return Ok("This customer is already registered with id of " + user.Id.ToString());
        }
        [HttpPost]
        public ActionResult<TokenResponse> AuthenticateClientApp(APIClientApp model)
        {
            if (!ModelState.IsValid)
                   return ValidationProblem(ModelState);
            //bgif(model.ClientId)
            /*string issuer = configuration["YourDesignServerAuthentication:Domain"];
            string audience = configuration["YourDesignServerAuthentication:Audience"];
            var claims = new List<Claim>()
            {
                new Claim("Application","YourDesign")
            };
            var mbToken = new JwtSecurityToken(issuer, audience, claims,
                                                expires: DateTime.UtcNow.AddMinutes(30),
                                                signingCredentials: new SigningCredentials(
                                                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["YourDesignServerAuthentication:SecurityKey"])),
                                                    SecurityAlgorithms.HmacSha256));
            return Ok(new JwtSecurityTokenHandler().WriteToken(mbToken));*/

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(configuration["Jwt:Issuer"],
              configuration["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddDays(7),
              signingCredentials: credentials);

            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}