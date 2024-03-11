﻿using Core.DB;
using Core.Models;
using Logic.IHelpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Y.Y.F_Web_App.Controllers
{
    public class UserController : Controller
    {
        
        private readonly AppDbContext _context;
        private readonly IUserHelper _userHelper;

        public UserController(AppDbContext context, IUserHelper userHelper)
        {
            _context = context;

            _userHelper = userHelper;
        }
            public IActionResult Index()
            {
            return View();
            }
        public IActionResult UserUpComingEvents()  
        {
            return View();
        }
        public IActionResult PrayerRequest() 
        { 
            return View();
        }
    }
}
