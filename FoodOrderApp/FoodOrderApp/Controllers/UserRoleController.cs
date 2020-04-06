﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodOrderApp.Data;
using FoodOrderApp.Repositories;
using FoodOrderApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FoodOrderApp.Controllers
{
    public class UserRoleController : Controller
    {
        private ApplicationDbContext _context;
        private IServiceProvider _serviceProvider;

        public UserRoleController(ApplicationDbContext context,
                                    IServiceProvider serviceProvider)
        {
            _context = context;
            _serviceProvider = serviceProvider;
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            UserRepo userRepo = new UserRepo(_context);
            var users = userRepo.All();
            return View(users);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Detail(string userName)
        {
            UserRoleRepo userRoleRepo = new UserRoleRepo(_serviceProvider);
            var roles = await userRoleRepo.GetUserRoles(userName);
            ViewBag.UserName = userName;
            return View(roles);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create(string userName)
        {
            ViewBag.SelectedUser = userName;


            RoleRepo roleRepo = new RoleRepo(_context);
            var roles = roleRepo.GetAllRoles().ToList();

            var preRoleList = roles.Select(r =>
                new SelectListItem { Value = r.RoleName, Text = r.RoleName })
                    .ToList();

            var roleList = new SelectList(preRoleList, "Value", "Text");


            ViewBag.RoleSelectList = roleList;

            var userList = _context.Users.ToList();

            var preUserList = userList.Select(u => new SelectListItem
            { Value = u.Email, Text = u.Email }).ToList();
            SelectList userSelectList = new SelectList(preUserList, "Value", "Text");

            ViewBag.UserSelectList = userSelectList;
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(UserRoleVM userRoleVM)
        {
            UserRoleRepo userRoleRepo = new UserRoleRepo(_serviceProvider);

            if (ModelState.IsValid)
            {
                var addUR = await userRoleRepo.AddUserRole(userRoleVM.Email,
                                                            userRoleVM.Role);
            }
            try
            {
                return RedirectToAction("Detail", "UserRole",
                        new { userName = userRoleVM.Email });
            }
            catch
            {
                return View();
            }
        }
    }
}