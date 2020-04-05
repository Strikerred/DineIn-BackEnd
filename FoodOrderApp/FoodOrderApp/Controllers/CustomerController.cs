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

namespace FoodOrderApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class CustomerController : Controller
    {
        private sqlContext _context;

        public CustomerController(sqlContext context)
        {
            _context = context;
        }

        // Customer Data
        // show all customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerInfo>>> GetCustomers()
        {
            return await _context.CustomerInfo.ToListAsync();
        }
        // Get one Customer object by id
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerInfo>> GetCustomer(int id)
        {
            var oneCustomer = await _context.CustomerInfo.FindAsync(id);
            if (oneCustomer == null)
            {
                return NotFound();
            }
            return oneCustomer;
        }

        //Update one customer object by id
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UpdateCustomerInfo(int id, CustomerInfo customerInfo)
        {
            if(id != customerInfo.CustomerId)
            {
                return BadRequest();
            }
            _context.Entry(customerInfo).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if(!CustomerInfoExists(id))
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

        // Transportation types
        
    }
}