using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FoodOrderApp.Models;
using FoodOrderApp.Repositories;
using FoodOrderApp.ResponseModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace FoodOrderApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class OrderController : Controller
    {
        private sqlContext _context;
        private IConfiguration _config;

        public OrderController(sqlContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Payment([FromBody]OrderRM OrderRM)
        {

            var claim = HttpContext.User.Claims.ElementAt(0);
            string userName = claim.Value;

            var order = await new PaymentRepo(_context, _config).Order(OrderRM, userName);

            if (!order.Item1)
            {
                return BadRequest(order.Item2);
            }

            return Ok(order.Item2);
        }
    }
}