using Core.DB;
using Core.Models;
using Core.ViewModels;
using Logic.Helpers;
using Logic.IHelpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
            var loggedInUser = _userHelper.FindByUserNameAsync(User.Identity.Name).Result;
            var totalAnnouncements = _userHelper.GetTotalAnnouncements();
            var totalPrayerRequests = _userHelper.GetTotalRequests(loggedInUser.Id);
            var totalUpcomingEvents = _userHelper.GetTotalEvents();

            var model = new ApplicationUserViewModel()
            {
                TotalAnnouncements = totalAnnouncements,
                TotalPrayerRequests = totalPrayerRequests,
                TotalUpcomingEvents = totalUpcomingEvents,
            };
            return View(model);
        }
        public IActionResult UserUpComingEvents()  
        {
            var listofEvents = _userHelper.ListofEvents();
            return View(listofEvents);
        }
        public IActionResult PrayerRequest() 
        {
           var loggedInUser = _userHelper.FindByUserNameAsync(User.Identity.Name).Result;
            var listofPrayerRequest = _userHelper.ListofUsersPrayerRequest(loggedInUser.Id);
            return View(listofPrayerRequest);
        }
        [HttpPost]
        public JsonResult AddPrayerRequest(string prayerRequest)
        {
            if (prayerRequest != null)
            {
                var loggedInUser = _userHelper.FindByUserNameAsync(User.Identity.Name).Result;
                var PrayerRequestDetails = JsonConvert.DeserializeObject<PrayerRequestViewModel>(prayerRequest);
                if (PrayerRequestDetails != null)
                {
                    //PrayerRequestDetails.UserId = loggedInUser.Id;
                    var prayerRequests = _userHelper.AddPrayerRequest(PrayerRequestDetails, loggedInUser);
                    if (prayerRequests)
                    {
                        return Json(new { isError = false, msg = "PrayerRequest added successfully" });
                    }
                    return Json(new { isError = true, msg = "Unable to add prayerRequest" });
                }
            }
            return Json(new { isError = true, msg = "Could not be found" });
        }

        [HttpGet]
        public IActionResult ApprovedPrayerRequest()
        {
            var listofApprovedPrayerRequests = _userHelper.ApprovedPrayerRequest();
            return View(listofApprovedPrayerRequests);
        }

        public IActionResult DiscussionForum()
        {
            var listofDiscussions = _userHelper.ListofDiscussions();
            return View(listofDiscussions);
        }

        [HttpGet]
        public IActionResult Discussion(int id) 
        {
            if (id > 0)
            {
                var getDiscussion = _userHelper.GetDiscussion(id);
                var getTotalLikes = _userHelper.TotalLikes(id);
                var getTotalComments = _userHelper.TotalComments(id);
                var model = new DiscussionForumViewModel
                {
                    NoOfLikes = getTotalLikes,
                    NoOfComments = getTotalComments,
                    SingleDiscussion = getDiscussion,
                };
                return View(model);
            }
            return View();
        }

        public JsonResult CreateComment(string message, int id)
        {
            if (message != null && id > 0)
            {
                var loggedInUser = _userHelper.FindByUserNameAsync(User.Identity.Name).Result;
                if (loggedInUser != null)
                {
                    var addComment = _userHelper.CreateComment(message, id, loggedInUser);
                    if (addComment)
                    {
                        return Json(new { isError = false, msg = "Comment added successfully" });
                    }
                    return Json(new { isError = true, msg = "Unable to add" });
                }
                return Json(new { isError = true, msg = "Could not find user" });
            }
            return Json(new { isError = true, msg = "Network Failure" });
        }

        public JsonResult AddLike(int id)
        {
            if (id > 0)
            {
                var loggedInUser = _userHelper.FindByUserNameAsync(User.Identity.Name).Result;
                if (loggedInUser != null)
                {
                    var checkIfUserHasLikedBefore = _userHelper.CheckUserLike(loggedInUser.Id);
                    if (!checkIfUserHasLikedBefore)
                    {
                        var addLike = _userHelper.AddLike(id, loggedInUser);
                        if (addLike)
                        {
                            return Json(new { isError = false });
                        }
                    }
                    //return Json(new { isError = true, });
                }  
            }
           return Json(new { isError = true, });
        }

        public JsonResult GetPrayerRequest(int id)
        {
            if (id > 0)
            {
                var checkRequestStatus = _userHelper.CheckRequestStatus(id);
                if (checkRequestStatus)
                {
                    return Json(new { isError = true, msg = " You cannot edit this Request again." });
                }
                var getRequest = _userHelper.GetRequest(id);
                if (getRequest != null)
                {
                    return Json(getRequest);
                }
                return Json(new { isError = true, msg = " Could not get request" });
            }
            return Json(new { isError = true, msg = "Network Failure" });
        }
        [HttpPost]
        public JsonResult EditedPrayerRequest(string prayerDetails)
        {
            if (prayerDetails != null)
            {
                var loggedInUser = _userHelper.FindByUserNameAsync(User.Identity.Name).Result;
                var requestDetails = JsonConvert.DeserializeObject<PrayerRequestViewModel>(prayerDetails);
                if (requestDetails != null)
                {
                    var prayerRequests = _userHelper.SaveEditedRequest(requestDetails, loggedInUser);
                    if (prayerRequests)
                    {
                        return Json(new { isError = false, msg = "PrayerRequest Edited successfully" });
                    }
                    return Json(new { isError = true, msg = "Unable to Edit prayerRequest" });
                }
            }
            return Json(new { isError = true, msg = "Could not be found" });
        }

        [HttpGet]
        public IActionResult Announcements()
        {
            var listofAnnouncement = _userHelper.ListofAnnouncement();
            return View(listofAnnouncement);
        }
        [HttpGet]
        public IActionResult BibleStudy()
        {
            var listofBibleStudy = _userHelper.listofBibleStudy();
            return View(listofBibleStudy);
        }

        [HttpGet]
        public IActionResult MediaGallery()
        {
            return View();
        }
		public IActionResult Profile()
		{
            var loggedInUser = _userHelper.FindByUserNameAsync(User.Identity.Name).Result;
            var user = _userHelper.GetUserDetails(loggedInUser.Id);
			return View(user);
		}

        [HttpPost]
        public JsonResult EditProfile(string profileDetails, string base64)
        {
            if (profileDetails != null)
            {
                var details = JsonConvert.DeserializeObject<ApplicationUserViewModel>(profileDetails);
                if (details != null)
                {
                    var editProfile = _userHelper.SaveEditedProfile(details, base64);
                    if (editProfile)
                    {
                        return Json(new { isError = false, msg = "Profile Edited Succesfully" });
                    }
                    return Json(new { isError = true, msg = "Unable to Edit" });
                }
            }
            return Json(new { isError = true, msg = "Network" });
        }

        public JsonResult DeleteRequest(int id)
        {
            if (id > 0)
            {
                var deleteRequest = _userHelper.DeletePrayer(id);
                if (deleteRequest)
                {
                    return Json(new { isError = false, msg = "Prayer Request Deleted sucessfully" });
                }
            }
            return Json(new { isError = true, msg = "Request not found" });
        }




    }
}
