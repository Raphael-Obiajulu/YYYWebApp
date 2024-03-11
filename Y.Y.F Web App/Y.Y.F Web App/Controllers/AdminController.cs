using Core.DB;
using Core.Models;
using Core.ViewModels;
using Logic.IHelpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Buffers.Text;

namespace Y.Y.F_Web_App.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IUserHelper _userHelper;


        public AdminController(AppDbContext context, IUserHelper userHelper)
        {
            _context = context;

            _userHelper = userHelper;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]

        public IActionResult UpComingEvents()
        {
            return View();
        }


        [HttpPost]
        public JsonResult UpComingEvents(string userDetails, string base64)
        {
            if (userDetails != null && base64 != null)
            {
                var applicationUserViewModel = JsonConvert.DeserializeObject<UpComingEvents>(userDetails);
                if (applicationUserViewModel != null)
                {
                    //var createEvents = _userHelper.CreateEvent(applicationUserViewModel, base64);
                    //if (createEvents)
                    //{

                    //}
                }
            }
            return Json(new { isError = false, msg = "Registration Successful" });
        }
    }
}