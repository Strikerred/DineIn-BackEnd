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
            dynamic jsonResponse = new JObject();

            if (_context.CustomerInfo.Any(u => u.UsersEmail == userName))
            {
                int id = _context.CustomerInfo.Where(u => u.UsersEmail == userName).FirstOrDefault().CustomerId;

                var customerInfo = await _context.CustomerInfo.FindAsync(id);

                return customerInfo;
            }

            jsonResponse.status = "Profile has not been completed yet";
            return Json(jsonResponse);
        }

        // POST: api/CustomerInfo
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<CustomerInfo>> PostCustomerInfo(CustomerInfo customerInfo)
        {
            var claim = HttpContext.User.Claims.ElementAt(0);
            string userNameJwt = claim.Value;
            customerInfo.UsersEmail = userNameJwt;

            if (customerInfo.TransportationType == null)
            {
                customerInfo.TransportationType = 0;
            }

            if (!_context.CustomerInfo.Any(u => u.UsersEmail == userNameJwt))
            {
                await AddRole(userNameJwt, customerInfo.UserRole);                

                _context.Add(customerInfo);

                await _context.SaveChangesAsync();

                return Ok("Profile has been completed");
            }
            else
            {
                var userId = new CustomerRepo(_context).GetCustomer(userNameJwt);

                customerInfo.CustomerId = userId;

                var local = _context.Set<CustomerInfo>().Local.FirstOrDefault(entry => entry.CustomerId.Equals(customerInfo.CustomerId));

                if (local != null)
                {
                    _context.Entry(local).State = EntityState.Detached;
                }
                
                _context.Entry(customerInfo).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                    return Ok("Profile has been updated");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerInfoExists(userId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }               
            }          
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