using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodOrderApp.Data;
using FoodOrderApp.Repositories;
using FoodOrderApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrderApp.Controllers
{
    public class RoleController : Controller
    {
        ApplicationDbContext _context;

        public RoleController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Role
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            RoleRepo roleRepo = new RoleRepo(_context);
            return View(roleRepo.GetAllRoles());
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Create(RoleVM roleVM)
        {
            if (ModelState.IsValid)
            {
                RoleRepo roleRepo = new RoleRepo(_context);
                var success = roleRepo.CreateRole(roleVM.RoleName);
                if (success)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewBag.Error = "An error occurred while creating this role. Please try again.";
            return View();
        }
    }
}