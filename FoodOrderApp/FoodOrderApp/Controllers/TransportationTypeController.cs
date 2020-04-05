using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FoodOrderApp.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class TransportationTypeController : Controller
    {
        private sqlContext _context;

        public TransportationTypeController(sqlContext context)
        {
            _context = context;
        }

        // TransportationTypes Data
        // show all Transportation types
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransportationType>>> GetTransportationTypes()
        {
            return await _context.TransportationType.ToListAsync();
        }

        // GET: api/TransportationType/2
        [HttpGet("{id}")]
        public async Task<ActionResult<TransportationType>> GetTransportationType(int id)
        {
            var transportationType = await _context.TransportationType.FindAsync(id);

            if (transportationType == null)
            {
                return NotFound();
            }

            return transportationType;
        }

        ////Update one transportation type object by id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTransportationType(int id, TransportationType transportationType)
        {
            if (id != transportationType.TransportationId)
            {
                return BadRequest();
            }
            _context.Entry(transportationType).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransportationTypeExists(id))
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

        // POST: api/TransportationType
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<CustomerInfo>> PostTransportationType(TransportationType transportationType)
        {
            _context.TransportationType.Add(transportationType);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetTransportationTypes", new { id = transportationType.TransportationId }, transportationType);
        }

        private bool TransportationTypeExists(int id)
        {
            throw new NotImplementedException();
        }
    }
}