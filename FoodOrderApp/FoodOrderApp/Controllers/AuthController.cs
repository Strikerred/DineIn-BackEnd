using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using FoodOrderApp.Data;
using FoodOrderApp.ResponseModel.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;

namespace FoodOrderApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private IConfiguration _config;
        private IServiceProvider _serviceProvider;
        private ApplicationDbContext _context;
        private UserManager<IdentityUser> _userManager;
        private readonly IEmailSender _emailSender;

        public AuthController(SignInManager<IdentityUser> signInManager,
                                IConfiguration config,
                                IServiceProvider serviceProvider,
                                ApplicationDbContext context,
                                UserManager<IdentityUser> userManager,
                                IEmailSender emailSender)
        {
            _signInManager = signInManager;
            _config = config;
            _serviceProvider = serviceProvider;
            _context = context;
            _userManager = userManager;
            _emailSender = emailSender;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<JsonResult> Register([FromBody]RegisterRM RegisterRM)
        {
            dynamic jsonResponse = new JObject();
            var user = new IdentityUser { UserName = RegisterRM.Email, Email = RegisterRM.Email };
            var result = await _userManager.CreateAsync(user, RegisterRM.Password);
            if (result.Succeeded)
            {
                string code = _userManager.GenerateEmailConfirmationTokenAsync(user).Result;
                string codeTokenLink = Url.Action("confirmEmail", "Auth", new
                {
                    userid = user.Id,
                    token = code
                }, protocol: HttpContext.Request.Scheme);

                await _emailSender.SendEmailAsync(RegisterRM.Email, "Confirm your email",
                    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(codeTokenLink)}'>clicking here</a>.");

                if (user != null)
                {
                    var tokenString = GenerateJSONWebToken(user);
                    jsonResponse.token = tokenString;
                    jsonResponse.status = "Please check your email";
                    return Json(jsonResponse);
                }
            }
            jsonResponse.token = "";
            jsonResponse.status = "Invalid Login";
            return Json(jsonResponse);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);

            return new RedirectResult("https://www.google.ca");
        }


        [HttpPost]
        [Route("Login")]
        public async Task<JsonResult> OnPostAsync([FromBody]LoginRM loginRM)
        {
            dynamic jsonResponse = new JObject();
            if (ModelState.IsValid)
            {
                var result = await
                            _signInManager.PasswordSignInAsync(loginRM.Email.ToUpper(),
                            loginRM.Password, loginRM.RememberMe, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    var UserManager = _serviceProvider
                        .GetRequiredService<UserManager<IdentityUser>>();
                    var user = await UserManager.FindByEmailAsync(loginRM.Email);

                    if (user != null)
                    {
                        var tokenString = GenerateJSONWebToken(user);
                        jsonResponse.token = tokenString;
                        jsonResponse.status = "OK";
                        return Json(jsonResponse);
                    }
                }
                else if (result.IsLockedOut)
                {
                    jsonResponse.token = "";
                    jsonResponse.status = "Locked Out";
                    return Json(jsonResponse);
                }
            }
            jsonResponse.token = "";
            jsonResponse.status = "Have you check your email? If so, either email or password didn't match";
            return Json(jsonResponse);
        }

        [HttpPost]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            dynamic jsonResponse = new JObject();

            await _signInManager.SignOutAsync();

            jsonResponse.token = "";
            jsonResponse.status = "Logged Out";

            return Json(jsonResponse);
        }

        List<Claim> AddUserRoleClaims(List<Claim> claims, string userId)
        { 
            var userRoleList = _context.UserRoles.Where(ur => ur.UserId == userId);
            var roleList = from ur in userRoleList
                           from r in _context.Roles
                           where r.Id == ur.RoleId
                           select new { r.Name };

            foreach (var roleItem in roleList)
            {
                claims.Add(new Claim(ClaimTypes.Role, roleItem.Name));
            }
            return claims;
        }

        string GenerateJSONWebToken(IdentityUser user)
        {
            var securityKey
                = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt_Key"]));
            var credentials
                = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim> {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti,
                            Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            claims = AddUserRoleClaims(claims, user.Id);

            var token = new JwtSecurityToken(_config["Jwt_Issuer"],
                _config["Jwt_Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}