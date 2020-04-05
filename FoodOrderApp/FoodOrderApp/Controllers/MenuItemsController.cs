using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FoodOrderApp.Data;
using FoodOrderApp.Models;

namespace FoodOrderApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemsController : ControllerBase
    {
        private readonly sqlContext _context;

        public MenuItemsController(sqlContext context)
        {
            _context = context;
        }


        // GET: api/MenuItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuItems>>> GetMenuItems()
        {
            return await _context.MenuItems.ToListAsync();
        }

        // GET: api/MenuItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MenuItems>> GetMenuItems(int id)
        {
            var menuItems = await _context.MenuItems.FindAsync(id);

            if (menuItems == null)
            {
                return NotFound();
            }

            return menuItems;
        }

        // PUT: api/MenuItems/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMenuItems(int id, MenuItems menuItems)
        {
            if (id != menuItems.MenuItemId)
            {
                return BadRequest();
            }

            _context.Entry(menuItems).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MenuItemsExists(id))
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

        // POST: api/MenuItems
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<MenuItems>> PostMenuItems(MenuItems menuItems)
        {
            _context.MenuItems.Add(menuItems);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMenuItems", new { id = menuItems.MenuItemId }, menuItems);
        }

        // DELETE: api/MenuItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MenuItems>> DeleteMenuItems(int id)
        {
            var menuItems = await _context.MenuItems.FindAsync(id);
            if (menuItems == null)
            {
                return NotFound();
            }

            _context.MenuItems.Remove(menuItems);
            await _context.SaveChangesAsync();

            return menuItems;
        }

        private bool MenuItemsExists(int id)
        {
            return _context.MenuItems.Any(e => e.MenuItemId == id);
        }
    }
}