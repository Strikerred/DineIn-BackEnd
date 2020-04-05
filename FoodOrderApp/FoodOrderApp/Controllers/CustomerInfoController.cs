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
    public class CustomerInfoController : Controller
    {
        private sqlContext _context;

        public CustomerInfoController(sqlContext context)
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

        // GET: api/CustomerInfo/2
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerInfo>> GetCustomerInfo(int id)
        {
            var customerInfo = await _context.CustomerInfo.FindAsync(id);

            if (customerInfo == null)
            {
                return NotFound();
            }

            return customerInfo;
        }

        ////Update one customer object by id
        //[HttpPut("{id}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        //public async Task<IActionResult> UpdateCustomerInfo(int id, CustomerInfo customerInfo)
        //{
        //    if(id != customerInfo.CustomerId)
        //    {
        //        return BadRequest();
        //    }
        //    _context.Entry(customerInfo).State = EntityState.Modified;
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch(DbUpdateConcurrencyException)
        //    {
        //        if(!CustomerInfoExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }
        //    return NoContent();
        //}

        // POST: api/customer

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<CustomerInfo>> PostCustomerInfo(CustomerInfo customerInfo)
        {
            _context.CustomerInfo.Add(customerInfo);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetCustomers", new { id = customerInfo.CustomerId }, customerInfo);
        }

        // DELETE: api/CustomerInfo/2
        [HttpDelete("{id}")]
        public async Task<ActionResult<CustomerInfo>> DeleteCustomerInfo(int id)
        {
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



        // Transportation types

    }
}