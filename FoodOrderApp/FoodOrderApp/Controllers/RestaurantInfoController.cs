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
    public class RestaurantInfoController : ControllerBase
    {
        private readonly sqlContext _context;
        public RestaurantInfoController(sqlContext context)
        {
            _context = context;
        }
        // GET: api/RestaurantInfoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RestaurantInfo>>> GetRestaurantInfo()
        {
            return await _context.RestaurantInfo.ToListAsync();
        }
        // GET: api/RestaurantInfoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RestaurantInfo>> GetRestaurantInfo(int id)
        {
            var restaurantInfo = await _context.RestaurantInfo.FindAsync(id);
            if (restaurantInfo == null)
            {
                return NotFound();
            }
            return restaurantInfo;
        }
        // PUT: api/RestaurantInfoes/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> PutRestaurantInfo(int id, RestaurantInfo restaurantInfo)
        {
            if (id != restaurantInfo.RestaurantId)
            {
                return BadRequest();
            }
            _context.Entry(restaurantInfo).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RestaurantInfoExists(id))
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
        // POST: api/RestaurantInfoes
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<RestaurantInfo>> PostRestaurantInfo(RestaurantInfo restaurantInfo)
        {
            _context.RestaurantInfo.Add(restaurantInfo);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetRestaurantInfo", new { id = restaurantInfo.RestaurantId }, restaurantInfo);
        }
        // DELETE: api/RestaurantInfoes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<RestaurantInfo>> DeleteRestaurantInfo(int id)
        {
            var restaurantInfo = await _context.RestaurantInfo.FindAsync(id);
            if (restaurantInfo == null)
            {
                return NotFound();
            }
            _context.RestaurantInfo.Remove(restaurantInfo);
            await _context.SaveChangesAsync();
            return restaurantInfo;
        }
        private bool RestaurantInfoExists(int id)
        {
            return _context.RestaurantInfo.Any(e => e.RestaurantId == id);
        }
    }
}