using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrderApp.Controllers
{
    public class CustomerController : Controller
    {

        // Customer Data


        // Transportation types
        public IActionResult Index()
        {
            return View();
        }
    }
}