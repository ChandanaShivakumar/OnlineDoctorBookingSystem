using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Sprint.Models;
using Sprint.Repository;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Sprint.Controllers
{
    [Route("[controller]/[action]")]
    public class UserController : Controller
    {
        private UserManager<ApplicationUser> usermanager;
        private IConfiguration configuration;
        private IPasswordHasher<ApplicationUser> passwordHasher;
        private SignInManager<ApplicationUser> signInManager;
        private RoleManager<IdentityRole> roleManager;
        private IUserRepository userRepository;

        public UserController(UserManager<ApplicationUser> umgr,
            IPasswordHasher<ApplicationUser> phasher, SignInManager<ApplicationUser> smgr, 
            IUserRepository ur, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            this.roleManager = roleManager;
            this.usermanager = umgr;
            this.passwordHasher = phasher;
            this.configuration = configuration;
            this.signInManager = smgr;
            this.userRepository = ur;
        }

        [HttpPost]
        //[Route("user/login")]
        public async Task<IActionResult> Login(Login model)
        {
            var user = await usermanager.FindByNameAsync(model.UserName);
            if (user != null && await usermanager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await usermanager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }
                if (userRoles.Count == 0)
                {
                    userRoles.Add("User");
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: configuration["JWT:ValidIssuer"],
                    audience: configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                return Ok(new
                {
                    role = userRoles[0],
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized("Please Enter Valid Credentials");
        }
        //Register
        [HttpPost]
       // [Route("user/register")]
        public async Task<IActionResult> Register(Registration model)
        {
            try
            {
                var userExists = await usermanager.FindByNameAsync(model.UserName);
                if (userExists != null)
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

                ApplicationUser user = new ApplicationUser()
                {
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = model.UserName
                };
                var result = await usermanager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

                return Ok(new Response { Status = "Success", Message = "User created successfully!" });
            }

            catch (Exception e)
            {
                return NotFound(new
                {
                    mes = e.Message
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAdmin(Registration model)
        {
            try
            {
                var userExists = await usermanager.FindByNameAsync(model.UserName);
                if (userExists != null)
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

                ApplicationUser user = new ApplicationUser()
                {
                    Email = model.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = model.UserName
                };
                var result = await usermanager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                if (await roleManager.RoleExistsAsync(UserRoles.Admin))
                {
                    await usermanager.AddToRoleAsync(user, UserRoles.Admin);
                }

                return Ok(new Response { Status = "Success", Message = "User created successfully!" });
            }
            catch (Exception e)
            {
                return NotFound(new
                {
                    mes = e.Message
                });

            }
        }
        public async Task<JsonResult> Logout()
        {
            await signInManager.SignOutAsync();
            return Json("Logout Successfully");
        }
    }
}