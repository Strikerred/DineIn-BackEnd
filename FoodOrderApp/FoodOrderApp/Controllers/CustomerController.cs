using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodOrderApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrderApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private sqlContext _context;

        public CustomerController(sqlContext context)
        {
            _context = context;
        }

        // Customer Data
        // show all customers
        public IActionResult Index()
        {
            CustomerInfo customerInfo = new CustomerInfo(_context);
            return View();
        }


        // Transportation types
        
    }
}