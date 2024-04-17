using Core.DB;
using Core.Migrations;
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

        public AdminController(AppDbContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
        }
        public IActionResult Index()
        {
            var totalAnnouncements = _userHelper.GetTotalAnnouncements();
            var totalDiscussions = _userHelper.GetTotalDiscussions();
            var totalUpcomingEvents = _userHelper.GetTotalEvents();

            var model = new ApplicationUserViewModel()
            {
                TotalAnnouncements = totalAnnouncements,
                TotalDiscussions = totalDiscussions,
                TotalUpcomingEvents = totalUpcomingEvents,
            };
            return View(model);
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
            if (userDetails != null)
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
            if (id > 0)
            {
                var eventdetails = _userHelper.GetDetails(id);
                if (eventdetails != null)
                {
                    return Json(eventdetails);
                }

            }
            return Json(new { isError = true, msg = "Did not see the event" });
        }

        public IActionResult PrayerRequest()
        {
            var listofPrayerRequest = _userHelper.ListofPrayerRequest();
            return View(listofPrayerRequest);
        }

        public JsonResult ApproveRequest(int id)
        {
            if (id > 0)
            {
                var approveRequest = _userHelper.ApproveRequest(id);
                if (approveRequest)
                {
                    return Json(new { isError = false, msg = " Approved Successfully" });
                }
                return Json(new { isError = true, msg = "Did not approve prayer request" });
            }
            return Json(new { isError = true, msg = "Did not find the event" });
        }

        public JsonResult DeclineRequest(int id)
        {
            if (id > 0)
            {
                var declineRequest = _userHelper.DeclineRequest(id);
                if (declineRequest)
                {
                    return Json(new { isError = false, msg = " Declined Successfully" });
                }
                return Json(new { isError = true, msg = "Did not decline prayer request" });
            }
            return Json(new { isError = true, msg = "Did not find the event" });
        }

        public IActionResult CreateDiscussion()
        {
            var listofDiscussions = _userHelper.ListofDiscussions();
            return View(listofDiscussions);
        }
        public IActionResult Comments()
        {
            var listofComments = _userHelper.ListofComments();
            return View(listofComments);
        }

        public JsonResult AddDiscussion(string details)
        {
            if (details != null)
            {
                var loggedInUser = _userHelper.FindByUserNameAsync(User.Identity.Name).Result;
                var discussionDetails = JsonConvert.DeserializeObject<DiscussionForumViewModel>(details);
                if (discussionDetails != null)
                {
                    var discussion = _userHelper.AddDiscussion(discussionDetails, loggedInUser);
                    if (discussion)
                    {
                        return Json(new { isError = false, msg = "Discussion added successfully" });
                    }
                    return Json(new { isError = true, msg = "Unable to add " });
                }
            }
            return Json(new { isError = true, msg = "Network Failure" });
        }

        public JsonResult ApproveComment(int commentId)
        {
            if (commentId > 0)
            {
                var approveComment = _userHelper.ApproveComment(commentId);
                if (approveComment)
                {
                    return Json(new { isError = false, msg = " Approved Successfully" });
                }
                return Json(new { isError = true, msg = "Did not approve comment" });
            }
            return Json(new { isError = true, msg = "Did not find the event" });
        }

        public JsonResult DeclineComment(int commentId)
        {
            if (commentId > 0)
            {
                var declineComment = _userHelper.DeclineComment(commentId);
                if (declineComment)
                {
                    return Json(new { isError = false, msg = " Decline Successfully" });
                }
                return Json(new { isError = true, msg = "Unable to decline comment" });
            }
            return Json(new { isError = true, msg = "Did not find the event" });
        }

        public JsonResult CreateAnnouncement(string details)
        {
            if (details != null)
            {
                var loggedInUser = _userHelper.FindByUserNameAsync(User.Identity.Name).Result;
                var announcementDetails = JsonConvert.DeserializeObject<AnnouncementViewModel>(details);
                if (announcementDetails != null)
                {
                    if (announcementDetails.DurationTill < announcementDetails.DurationFrom)
                    {
                        return Json(new { isError = true, msg = "Add the correct duration" });
                    }
                    if (announcementDetails.AnnouncementDetails == "")
                    {
                        return Json(new { isError = true, msg = "Please add announcement detail" });
                    }
                    var announcement = _userHelper.AddAnnouncements(announcementDetails, loggedInUser);
                    if (announcement)
                    {
                        return Json(new { isError = false, msg = "Announcement added successfully" });
                    }
                    return Json(new { isError = true, msg = "Unable to add " });
                }
            }
            return Json(new { isError = true, msg = "Network Failure" });
        }
        [HttpGet]
        public IActionResult AddAnnouncement()
        {
            var listofAnnouncement = _userHelper.ListofAnnouncementForAdmin();
            return View(listofAnnouncement);
        }

        public IActionResult AddBibleStudy()
        {
            var listofBibleStudy = _userHelper.listofBibleStudy();
            return View(listofBibleStudy);
        }

        public JsonResult CreateBibleStudy(string details)
        {
            if (details != null)
            {
                var loggedInUser = _userHelper.FindByUserNameAsync(User.Identity.Name).Result;
                var biblestudyDetails = JsonConvert.DeserializeObject<BibleStudyViewModel>(details);
                if (biblestudyDetails != null)
                {
                    var biblestudy = _userHelper.CreateBibleStudy(biblestudyDetails, loggedInUser);
                    if (biblestudy != null)
                    {
                        return Json(new { isError = false, msg = "BibleStudy added successfully" });
                    }
                    return Json(new { isError = true, msg = "Unable to add " });
                }
            }
            return Json(new { isError = true, msg = "Network Failure" });
        }
        public IActionResult MediaGallery()
        {
            var listofMedia = _userHelper.ListofMedia();
            var listOfVideos = _userHelper.ListofVideos();
            var model = new MediaGalleryViewModel()
            {
                MediaVideos = listOfVideos,
                AllMedia = listofMedia,
            };
            return View(model);
        }

        public IActionResult AllUsers()
        {
            var listofUsers = _userHelper.ListofUsers();
            return View(listofUsers);
        }

        public JsonResult DeactivateUser(string userId)
        {
            if (userId != null)
            {
                var deactivateUser = _userHelper.DeactivateUser(userId);
                if (deactivateUser)
                {
                    return Json(new { isError = false, msg = "Member Deactivated" });
                }
            }
            return Json(new { isError = true, msg = "Member not found" });
        }

        public JsonResult DeleteEvent(int id)
        {
            if (id > 0)
            {
                var deleteEvent = _userHelper.DeleteEvent(id);
                if (deleteEvent)
                {
                    return Json(new { isError = false, msg = "Event Deleted" });
                }
            }
            return Json(new { isError = true, msg = "Member not found" });
        }

        public JsonResult GetDiscussionToEdit(int id)
        {
            if (id > 0)
            {
                var getDiscus = _userHelper.GetDisccusion(id);
                if (getDiscus != null)
                {
                    return Json(getDiscus);
                }
                return Json(new { isError = true, msg = " Could not get discussion" });
            }
            return Json(new { isError = true, msg = "Network Failure" });
        }

        public JsonResult SaveEditedDiscussion(string details)
        {
            if (details != null)
            {
                var loggedInUser = _userHelper.FindByUserNameAsync(User.Identity.Name).Result;
                var discussionDetails = JsonConvert.DeserializeObject<DiscussionForumViewModel>(details);
                if (discussionDetails != null)
                {
                    var discussion = _userHelper.EditDiscussion(discussionDetails, loggedInUser);
                    if (discussion)
                    {
                        return Json(new { isError = false, msg = "Discussion Edited successfully" });
                    }
                    return Json(new { isError = true, msg = "Unable to Edit " });
                }
            }
            return Json(new { isError = true, msg = "Network Failure" });
        }

        public JsonResult DelDiscussion(int id)
        {
            if (id > 0)
            {
                var delete = _userHelper.DeleteDiscussion(id);
                if (delete)
                {
                    return Json(new { isError = false, msg = "Discussion Deleted sucessfully" });
                }
            }
            return Json(new { isError = true, msg = "Discussion not found" });
        }

        public JsonResult GetAnnounceToEdit(int id)
        {
            if (id > 0)
            {
                var get = _userHelper.GetAnnouncement(id);
                if (get != null)
                {
                    return Json(get);
                }
                return Json(new { isError = true, msg = " Could not get Annoucement" });
            }
            return Json(new { isError = true, msg = "Network Failure" });
        }

        public JsonResult SaveEditedAnnouncement(string announcedetails)
        {
            if (announcedetails != null)
            {
                var loggedInUser = _userHelper.FindByUserNameAsync(User.Identity.Name).Result;
                var announcementDetails = JsonConvert.DeserializeObject<AnnouncementViewModel>(announcedetails);
                if (announcementDetails != null)
                {
                    if (announcementDetails.DurationTill < announcementDetails.DurationFrom)
                    {
                        return Json(new { isError = true, msg = "Add the correct durations" });
                    }
                    var editannouncement = _userHelper.EditAnnouncement(announcementDetails, loggedInUser);
                    if (editannouncement)
                    {
                        return Json(new { isError = false, msg = "Announcement Edited successfully" });
                    }
                    return Json(new { isError = true, msg = "Unable to Edit " });
                }
            }
            return Json(new { isError = true, msg = "Network Failure" });
        }


        public JsonResult DelAnnounce(int id)
        {
            if (id > 0)
            {
                var delete = _userHelper.DeleteAnnounce(id);
                if (delete)
                {
                    return Json(new { isError = false, msg = "Announcement Deleted sucessfully" });
                }
            }
            return Json(new { isError = true, msg = "Announcement not found" });
        }
        public JsonResult DeleteBibleStudy(int id)
        {
            if (id > 0)
            {
                var delete = _userHelper.DeleteBibleStudy(id);

                if (delete)
                {
                    return Json(new
                    {
                        isError = false,
                        msg = "BibleStudy deleted successfully"
                    });
                }
            }

            return Json(new { isError = true, msg = "BibleStudy not found" });
        }

        [HttpPost]
        public JsonResult CreateMedia(string mediaDetails, string base64)
        {
            if (mediaDetails != null)
            {
                var loggedInUser = _userHelper.FindByUserNameAsync(User.Identity.Name).Result;
                var details = JsonConvert.DeserializeObject<MediaGalleryViewModel>(mediaDetails);
                if (details != null)
                {
                    var addMediaGallery = _userHelper.SaveMediaGallery(details, base64, loggedInUser.Id);
                    if (addMediaGallery)
                    {
                        return Json(new { isError = false, msg = "Media Added Succesfully" });
                    }
                    return Json(new { isError = true, msg = "Unable to Add" });
                }
            }
            return Json(new { isError = true, msg = "Network Failure" });
        }

        public JsonResult CreateVideo(string videoDetails)
        {
            if (videoDetails != null)
            {
                var details = JsonConvert.DeserializeObject<Video>(videoDetails);
                if (details != null)
                {
                    var addVideo = _userHelper.SaveVideo(details);
                    if (addVideo)
                    {
                        return Json(new { isError = false, msg = "Video Added Succesfully" });
                    }
                    return Json(new { isError = true, msg = "Unable to Add" });
                }
            }
            return Json(new { isError = true, msg = "Network Failure" });
        }

        public JsonResult DelMedia(int id)
        {
            if (id > 0)
            {
                var delete = _userHelper.DeleteMediaGallery(id);
                if (delete)
                {
                    return Json(new {isError = false, msg = "Media deleted successfully" });
                }
            }
            return Json(new { isError = true, msg = " Media not found" });
        }

        public JsonResult GetBibleToEdit(int id)
        {
            if (id > 0)
            {
                var getbiblestudy = _userHelper.Getbiblestudy(id);
                if (getbiblestudy != null)
                {
                    return Json(getbiblestudy);
                }
                return Json(new { isError = true, msg = " Could not get Annoucement" });
            }
            return Json(new { isError = true, msg = "Network Failure" });
        }

        public JsonResult SaveEditedBibleStudy(string details)
        {
            if (details != null)
            {
                var loggedInUser = _userHelper.FindByUserNameAsync(User.Identity.Name).Result;
                var bibleStudyDetails = JsonConvert.DeserializeObject<BibleStudyViewModel>(details);
                if (bibleStudyDetails != null)
                {
                    var biblestudy = _userHelper.SaveEditedBibleStudy(bibleStudyDetails, loggedInUser);
                    if (biblestudy)
                    {
                        return Json(new { isError = false, msg = "BibleStudy Edited successfully" });
                    }
                    return Json(new { isError = true, msg = "Unable to Edit " });
                }
            }
            return Json(new { isError = true, msg = "Network Failure" });
        }
    }
}
