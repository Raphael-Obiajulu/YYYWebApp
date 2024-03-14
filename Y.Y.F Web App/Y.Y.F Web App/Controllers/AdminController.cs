using Core.DB;
using Core.Models;
using Core.ViewModels;
using Logic.IHelpers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Y.Y.F_Web_App.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IUserHelper _userHelper;
        private object applicationUserViewModel;

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
            var listofEvents = _userHelper.ListofEvents();
            return View(listofEvents);
        }

        [HttpPost]
        public JsonResult Events(string userDetails, string base64)
        {
            if (userDetails != null && base64 != null)
            {
                var upComingEvents = JsonConvert.DeserializeObject<UpComingEventViewModel>(userDetails);
                if (upComingEvents != null)
                {
                    var checkName = _userHelper.CheckEventName(upComingEvents?.EventTitle);
                    if (checkName)
                    {
                        return Json(new { isError = true, msg = "The event name already exists" });
                    }
                    var createEvents = _userHelper.CreateEvent(upComingEvents, base64);
                    if (createEvents)
                    {
                        return Json(new { isError = false, msg = "upComingEvent added successfully" });
                    }
                    return Json(new { isError = true, msg = "Unable to add UpComingEvent" });
                }
            }
            return Json(new { isError = true, msg = "Could not be found" });
        }


        public JsonResult GetEventDetails(int id) 
        {
            if(id > 0)
            {
                var eventdetails = _userHelper.GetDetails(id);
                if (eventdetails != null) 
                {
                    return Json(eventdetails);
                }

            }
            return Json(new { isError = true, msg = "The event name already exists" });
        }



        public IActionResult PrayerRequest()
        {
            return View();
        }
    }

}