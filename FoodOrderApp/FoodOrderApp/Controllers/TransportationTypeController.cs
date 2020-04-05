using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodOrderApp.Models;
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
    }
}