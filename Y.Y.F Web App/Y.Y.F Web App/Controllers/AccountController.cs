using Core.DB;
using Core.Models;
using Core.ViewModels;
using Logic.IHelpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Cryptography.X509Certificates;

namespace Y.Y.F_Web_App.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserHelper _userHelper;
        private readonly IDropdownHelper _dropdownhelper;

        public AccountController(AppDbContext context, UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, IUserHelper userHelper, IDropdownHelper dropdownhelper)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _userHelper = userHelper;
            _dropdownhelper = dropdownhelper;
        }
        [HttpGet]
        public IActionResult Register()
        {
            ViewBag.Gender = _dropdownhelper.DropdownOfGender();
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> Registration(string userDetails)
        {
            if (userDetails != null) 
            {
                var applicationUserViewModel = JsonConvert.DeserializeObject<ApplicationUserViewModel>(userDetails); 
                if (applicationUserViewModel != null)
                {
                    var checkEmail = _userHelper.FindByEmailAsync(applicationUserViewModel?.Email).Result;
                    if (checkEmail != null)
                    {
                        return Json(new { isError = true, msg = "Email Already Exists" });
                    }
                    var checkUserName = _userHelper.FindByUserNameAsync(applicationUserViewModel?.UserName).Result;
                    if (checkUserName != null)
                    {
                        return Json(new { isError = true, msg = "Username Already Exists" });
                    }
                    if (applicationUserViewModel.GenderId == 0)
                    {
                        return Json(new { isError = true, msg = "Please select a gender" });
                    };
                    if (applicationUserViewModel.Password != applicationUserViewModel.ConfirmPassword)
                    {
                        return Json(new { isError = true, msg = "Password and confirm password did not match" });
                    }
                    var createUser = await _userHelper.CreateUser(applicationUserViewModel).ConfigureAwait(false);
                    if (createUser) 
                    {
                        return Json(new { isError = false, msg = "Registration Successful" });
                    }
                    return Json(new { isError = true, msg = "Could not register" });
                }
            }
            return Json(new { isError = true, msg = "Network Issue" });
        }

        [HttpGet]
        public IActionResult AdminRegister()
        {
            ViewBag.Gender = _dropdownhelper.DropdownOfGender();
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> AdminRegistration(string userDetails)
        {
            if (userDetails != null)
            {
                var applicationUserViewModel = JsonConvert.DeserializeObject<ApplicationUserViewModel>(userDetails);
                if (applicationUserViewModel != null)
                {
                    var checkEmail = _userHelper.FindByEmailAsync(applicationUserViewModel?.Email).Result;
                    if (checkEmail != null)
                    {
                        return Json(new { isError = true, msg = "Email Already Exists" });
                    }
                    var checkUserName = _userHelper.FindByUserNameAsync(applicationUserViewModel?.UserName).Result;
                    if (checkUserName != null)
                    {
                        return Json(new { isError = true, msg = "Username Already Exists" });
                    }
                    if (applicationUserViewModel.GenderId == 0)
                    {
                        return Json(new { isError = true, msg = "Please select a gender" });
                    };
                    if (applicationUserViewModel.Password != applicationUserViewModel.ConfirmPassword)
                    {
                        return Json(new { isError = true, msg = "Password and confirm password did not match" });
                    }
                    var createUser = await _userHelper.CreateAdmin(applicationUserViewModel).ConfigureAwait(false);
                    if (createUser)
                    {
                        return Json(new { isError = false, msg = "Registration Successful" });
                    }
                    return Json(new { isError = true, msg = "Could not register" });
                }
            }
            return Json(new { isError = true, msg = "Network Issue" });
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Login(string username, string password)
        {
            if (username != null && password  != null)
            {
                var userName =  _userHelper.FindByUserNameAsync(username).Result;
                if (userName != null)
                {
                    var result =  _signInManager.PasswordSignInAsync(userName, password, true, false).ConfigureAwait(false).GetAwaiter().GetResult();
                    if (result.Succeeded)
                    {
                        var url = "";
                        var isInRole =  _userManager.IsInRoleAsync(userName, "Admin").Result;
                        if (isInRole)
                        {
                            url = "/Admin/Index";
                        }
                        else
                        {
                            url = "/User/Index";
                        }
                        return Json(new { isError = false, dashboard = url });
                    }
                    return Json(new { isError = true, msg = "username or password not found" });
                }
            }
            return Json(new { isError = true, msg = "Check UserName and Password" });
        }

        public async Task<IActionResult> LogOut()
        {
            try
            {
                await _signInManager.SignOutAsync();
                return RedirectToAction("Index", "Home");
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

    }

}
