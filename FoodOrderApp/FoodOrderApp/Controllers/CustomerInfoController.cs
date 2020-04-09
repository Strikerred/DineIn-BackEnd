using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FoodOrderApp.Data;
using FoodOrderApp.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using FoodOrderApp.Repositories;
using Newtonsoft.Json.Linq;
using Stripe;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace FoodOrderApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class CustomerInfoController : Controller
    {
        private sqlContext _context;
        private IServiceProvider _serviceProvider;

        public CustomerInfoController(sqlContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _serviceProvider = serviceProvider;
        }

        // Customer Data
        // show all customers
        [HttpGet]
        [Route("getall")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<IEnumerable<CustomerInfo>>> GetCustomers()
        {
            return await _context.CustomerInfo.ToListAsync();
        }

        // GET: api/CustomerInfo/2
        [HttpGet]
        [Route("currentuser")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<CustomerInfo>> GetCustomerInfo()
        {
            var claim = HttpContext.User.Claims.ElementAt(0);
            string userName = claim.Value;

            int id = _context.CustomerInfo.Where(u => u.UsersEmail == userName).FirstOrDefault().CustomerId;

            var customerInfo = await _context.CustomerInfo.FindAsync(id);

            if (customerInfo == null)
            {
                return NotFound();
            }

            return customerInfo;
        }

        ////Update one customer object by id
        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UpdateCustomerInfo(CustomerInfo customerInfo)
        {
            var claim = HttpContext.User.Claims.ElementAt(0);
            string userName = claim.Value;

            int id = _context.CustomerInfo.Where(u => u.UsersEmail == userName).FirstOrDefault().CustomerId;

            if (id == 0)
            {
                return BadRequest();
            }
            _context.Entry(customerInfo).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerInfoExists(id))
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

        // POST: api/CustomerInfo
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<CustomerInfo>> PostCustomerInfo(CustomerInfo customerInfo)
        {
            var claim = HttpContext.User.Claims.ElementAt(0);
            string userNameJwt = claim.Value;

            await AddRole(userNameJwt, customerInfo.UserRole);

            customerInfo.UsersEmail = userNameJwt;

            _context.CustomerInfo.Add(customerInfo);
            await _context.SaveChangesAsync();

            return Ok("Profile has been completed");
        }

        // DELETE: api/CustomerInfo/2
        [HttpDelete]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<CustomerInfo>> DeleteCustomerInfo()
        {
            var claim = HttpContext.User.Claims.ElementAt(0);
            string userName = claim.Value;

            int id = _context.CustomerInfo.Where(u => u.UsersEmail == userName).FirstOrDefault().CustomerId;

            var customerInfo = await _context.CustomerInfo.FindAsync(id);
            if (customerInfo == null)
            {
                return NotFound();
            }
            _context.CustomerInfo.Remove(customerInfo);
            await _context.SaveChangesAsync();
            return customerInfo;
        }
        private bool CustomerInfoExists(int id)
        {
            return _context.CustomerInfo.Any(e => e.CustomerId == id);
        }

        private async Task<bool> AddRole(string userName, string role)
        {
            UserRoleRepo userRoleRepo = new UserRoleRepo(_serviceProvider);

            if (ModelState.IsValid)
            {
                var addUR = await userRoleRepo.AddUserRole(userName, role);
                if (addUR)
                {
                    return true; 
                }
            }
            return false; 
        }
    }
}