using Core.DB;
using Core.Models;
using Core.ViewModels;
using Logic.IHelpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Helpers
{
    public class UserHelper : IUserHelper
    {
        private readonly AppDbContext _context;
        private UserManager<ApplicationUser> _userManager;

        public UserHelper(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<ApplicationUser> FindByUserNameAsync(string username)
        {
            return await _userManager.Users.Where(s => s.UserName == username)?.FirstOrDefaultAsync();
        }
        public async Task<ApplicationUser>? FindByEmailAsync(string email)
        {
            try
            {
                var user = await _context.ApplicationUsers
                    .Where(s => s.Email == email && !s.Deactivated)
                    .FirstOrDefaultAsync().ConfigureAwait(false);
                if (user != null)
                {
                    return user;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return null;
        }
        public ApplicationUser FindById(string Id)
        {
            return _context.ApplicationUsers.Where(s => s.Id == Id)?.FirstOrDefault();
        }
        public async Task<ApplicationUser> FindUserById(string id)
        {
            return _userManager.Users.Where(s => s.Id == id)?.FirstOrDefault();
        }

        public async Task<bool> CreateUser(ApplicationUserViewModel applicationUserViewModel)
        {
            try
            {
                if (applicationUserViewModel != null)
                {
                    var user = new ApplicationUser()
                    {
                        UserName = applicationUserViewModel.UserName,
                        Email = applicationUserViewModel.Email,
                        PhoneNumber = applicationUserViewModel.PhoneNumber,
                        FirstName = applicationUserViewModel.FirstName,
                        LastName = applicationUserViewModel.LastName,
                        GenderId = applicationUserViewModel.GenderId,
                        DateCreated = DateTime.Now,
                        Deactivated = false,
                    };
                    var createUser = await _userManager.CreateAsync(user, applicationUserViewModel.Password);
                    if (createUser.Succeeded)
                    {
                       await _userManager.AddToRoleAsync(user, "User").ConfigureAwait(false);
                       return true;
                    }
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        public async Task<bool> CreateAdmin(ApplicationUserViewModel applicationUserViewModel)
        {
            try
            {
                if (applicationUserViewModel != null)
                {
                    var user = new ApplicationUser()
                    {
                        UserName = applicationUserViewModel.UserName,
                        Email = applicationUserViewModel.Email,
                        PhoneNumber = applicationUserViewModel.PhoneNumber,
                        FirstName = applicationUserViewModel.FirstName,
                        LastName = applicationUserViewModel.LastName,
                        GenderId = applicationUserViewModel.GenderId,
                        DateCreated = DateTime.Now,
                        Deactivated = false,
                    };
                    var createUser = await _userManager.CreateAsync(user, applicationUserViewModel.Password);
                    if (createUser.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, "Admin").ConfigureAwait(false);
                        return true;
                    }
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public bool CheckEventName(string eventTitle)
        {
            if (eventTitle != null)
            {
                var checkName = _context.UpComingEvents.Where(x => x.EventTitle == eventTitle && x.Active && !x.Deleted).FirstOrDefault();
                if (checkName != null) 
                {
                    return true;
                }
            }
            return false;
        }

        public bool CreateEvent(UpComingEventViewModel upComingEvents, string base64)
        {
            if (upComingEvents != null ) 
            {
                var createEvent = new UpComingEvents()
                {
                    EventTitle = upComingEvents?.EventTitle,
                    EventDate = upComingEvents?.EventDate,
                    EventDetails = upComingEvents?.EventDetails,
                    EventTime = upComingEvents?.EventTime,
                    DateCreated = DateTime.Now,
                    Active = true,
                    Deleted = false,
                    EventImage = base64 != null ? base64 : null
                };
                _context.Add(createEvent);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

       public List<UpComingEventViewModel> ListofEvents()
       {
            var upComingEventsviewmodel = new List<UpComingEventViewModel>();
            upComingEventsviewmodel = _context.UpComingEvents.Where(a => a.Id > 0 && a.Active && !a.Deleted)
            .Select(a => new UpComingEventViewModel()
            {
                EventDate = a.EventDate,
                EventDetails = a.EventDetails,
                EventTime = a.EventTime,
                EventTitle = a.EventTitle,
                DateCreated = a.DateCreated,
                Active = a.Active,
                Deleted = a.Deleted,
                EventImage = a.EventImage,
                Id = a.Id,
            }).ToList();

            return upComingEventsviewmodel;
       }

        public UpComingEvents GetDetails(int id)
        {
            var getdetails = _context.UpComingEvents.Where(x => x.Id == id && x.Active).FirstOrDefault();
            if (getdetails != null)
            {
                return getdetails;
            }
            return null;
        }

        public bool AddPrayerRequest(PrayerRequestViewModel prayerRequest, ApplicationUser loggedInUser)
        {
            if (prayerRequest != null)
            {
                var addPrayerRequest = new PrayerRequest()
                {
                    PrayerRequestTitle = prayerRequest?.PrayerRequestTitle,
                    PrayerRequestDetails = prayerRequest?.PrayerRequestDetails,
                    DateCreated = DateTime.Now,
                    Active = true,
                    Deleted = false,
                    PrayerRequestStatus = YYFEnums.StatusEnum.Pending,
                    UserId = loggedInUser.Id,
                };
                _context.Add(addPrayerRequest);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public List<PrayerRequestViewModel> ListofPrayerRequest()
        {
            var prayerRequestViewModel = new List<PrayerRequestViewModel>();
            prayerRequestViewModel = _context.PrayerRequests.Where(a => a.Id > 0 && a.Active && !a.Deleted && a.PrayerRequestStatus == YYFEnums.StatusEnum.Pending)
                .Include(x => x.User)
            .Select(a => new PrayerRequestViewModel()
            {
                PrayerRequestStatus = a.PrayerRequestStatus,
                PrayerRequestTitle = a.PrayerRequestTitle,
                PrayerRequestDetails = a.PrayerRequestDetails,
                DateCreated = a.DateCreated,
                UserId = a.UserId,
                Username = a.User.UserName,
                Id = a.Id,
               

            }).ToList();

            return prayerRequestViewModel;
        }

        public List<PrayerRequestViewModel> ListofUsersPrayerRequest(string loggedInUser)
        {
            if (loggedInUser != null) 
            {
                var prayerRequestViewModel = new List<PrayerRequestViewModel>();
                prayerRequestViewModel = _context.PrayerRequests.Where(a => a.Id > 0 && a.Active && !a.Deleted && a.UserId == loggedInUser)
                    .Include(x => x.User)
                .Select(a => new PrayerRequestViewModel()
                {
                    PrayerRequestStatus = a.PrayerRequestStatus,
                    PrayerRequestTitle = a.PrayerRequestTitle,
                    PrayerRequestDetails = a.PrayerRequestDetails,
                    DateCreated = a.DateCreated,
                    UserId = a.UserId,
                    Username = a.User.UserName,
                    Id = a.Id,

                }).ToList();

                return prayerRequestViewModel;
            }

            return null;
        }
        public List<PrayerRequestViewModel> ApprovedPrayerRequest()
        {
            var prayerRequestViewModel = new List<PrayerRequestViewModel>();
            prayerRequestViewModel = _context.PrayerRequests.Where(a => a.Id > 0 && a.Active && !a.Deleted && a.PrayerRequestStatus == YYFEnums.StatusEnum.Approved)
                .Include(x => x.User)
            .Select(a => new PrayerRequestViewModel()
            {
                PrayerRequestStatus = a.PrayerRequestStatus,
                PrayerRequestTitle = a.PrayerRequestTitle,
                PrayerRequestDetails = a.PrayerRequestDetails,
                DateCreated = a.DateCreated,
                UserId = a.UserId,
                Username = a.User.UserName,
                Id = a.Id,


            }).ToList();

            return prayerRequestViewModel;
        }

        
        public bool ApproveRequest(int id)
        {
            var dd = _context.PrayerRequests.Where(x => x.Id == id && x.Active && x.PrayerRequestStatus == YYFEnums.StatusEnum.Pending).FirstOrDefault();
            if (dd != null)
            {
                dd.PrayerRequestStatus = YYFEnums.StatusEnum.Approved;
                _context.Update(dd);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        public bool DeclineRequest(int id)
        {
            var dd = _context.PrayerRequests.Where(x => x.Id == id && x.Active && x.PrayerRequestStatus == YYFEnums.StatusEnum.Pending).FirstOrDefault();
            if (dd != null)
            {
                dd.PrayerRequestStatus = YYFEnums.StatusEnum.Declined;
                _context.Update(dd);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public List<CommentsViewModel> ListofComments()
        {
            var commentsViewModel = new List<CommentsViewModel>();
            commentsViewModel = _context.Comments.Where(a => a.Id > 0 && a.CommentStatus == YYFEnums.StatusEnum.Pending)
                .Include(x => x.User).Include( b => b.Discussion)
            .Select(a => new CommentsViewModel()
            {
                Message = a.Message,
                UserId = a.UserId,
                UserName = a.User.UserName,
                //User = a.User,
                Discussion = a.Discussion,
                DiscussionForumId = a.DiscussionForumId,
                Id = a.Id,
            }).ToList();

            return commentsViewModel;
        }

        public List<CommentsViewModel> ListofApprovedComments()
        {
            var commentsViewModel = new List<CommentsViewModel>();
            commentsViewModel = _context.Comments.Where(a => a.Id > 0 && a.CommentStatus == YYFEnums.StatusEnum.Approved)
                .Include(x => x.User).Include(b => b.Discussion)
            .Select(a => new CommentsViewModel()
            {
                Message = a.Message,
                UserId = a.UserId,
                UserName = a.User.UserName,
                //User = a.User,
                Discussion = a.Discussion,
                DiscussionForumId = a.DiscussionForumId,
                Id = a.Id,
            }).ToList();

            return commentsViewModel;
        }
        public List<DiscussionForumViewModel> ListofDiscussions()
        {
            var discussionViewModel = new List<DiscussionForumViewModel>();
            discussionViewModel = _context.Discussions.Where(a => a.Id > 0 && a.Active && !a.Deleted)
            .Select(a => new DiscussionForumViewModel()
            {
                DiscussionTitle = a.DiscussionTitle,
                DiscussionDetails = a.DiscussionDetails,
                DateCreated = a.DateCreated,
                Id = a.Id,
            }).ToList();

            return discussionViewModel;
        }

        public bool AddDiscussion(DiscussionForumViewModel discussion, ApplicationUser loggedInUser)
        {
            if (discussion != null)
            {
                var addDiscussion = new DiscussionForum()
                {
                    DiscussionDetails = discussion?.DiscussionDetails,
                    DiscussionTitle = discussion?.DiscussionTitle,
                    DateCreated = DateTime.Now,
                    Active = true,
                    Deleted = false,
                    UserId = loggedInUser.Id,
                };
                _context.Add(addDiscussion);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool CreateComment(string message, int id, ApplicationUser loggedInUser)
        {
            if (message != null && loggedInUser != null)
            {
                var addComment = new Comment()
                {
                    Message = message,
                    UserId = loggedInUser.Id,
                    CommentStatus = YYFEnums.StatusEnum.Pending,
                    DiscussionForumId = id,
                };
                _context.Add(addComment);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public int TotalLikes(int discussionId)
        {
            if (discussionId > 0)
            {
               return _context.Likes.Where(x => x.DiscussionForumId == discussionId).Count();
            }
            return 0;
        }

        public int TotalComments(int discussionId)
        {
            if (discussionId > 0)
            {
                return _context.Comments.Where(x => x.DiscussionForumId == discussionId && x.CommentStatus == YYFEnums.StatusEnum.Approved).Count();
            }
            return 0;
        }

        public DiscussionForumViewModel GetDiscussion(int discussionId)
        {
            var comments = ListofApprovedComments();
            var discussionViewModel = new DiscussionForumViewModel();
            discussionViewModel = _context.Discussions.Where(a => a.Id == discussionId && a.Active && !a.Deleted)
            .Select(a => new DiscussionForumViewModel()
            {
                DiscussionTitle = a.DiscussionTitle,
                DiscussionDetails = a.DiscussionDetails,
                DateCreated = a.DateCreated,
                Id = a.Id,
                Active = a.Active,
                Comments = comments,
            }).FirstOrDefault();

            return discussionViewModel;
        }
        
        public bool CheckUserLike(string userId)
        {
            var check = _context.Likes.Where(x => x.UserId == userId).FirstOrDefault();
            if (check != null)
            {
                return true;
            }
            return false;
        }

        public bool AddLike(int id, ApplicationUser loggedInUser)
        {
            if (id > 0 && loggedInUser != null)
            {
                var addLike = new Like()
                {
                    UserId = loggedInUser.Id,
                    DiscussionForumId = id,
                };
                _context.Add(addLike);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool CheckRequestStatus(int requestId)
        {
            var check = _context.PrayerRequests.Where(x => x.Id == requestId && 
            (x.PrayerRequestStatus == YYFEnums.StatusEnum.Approved || x.PrayerRequestStatus == YYFEnums.StatusEnum.Declined)
            && x.Active).FirstOrDefault();
            if (check != null)
            {
                return true;
            }
            return false;
        }

        public PrayerRequestViewModel GetRequest( int id)
        {
            var prayerRequestViewModel = new PrayerRequestViewModel();
            prayerRequestViewModel = _context.PrayerRequests.Where(a => a.Id == id && a.Active && !a.Deleted && a.PrayerRequestStatus == YYFEnums.StatusEnum.Pending)
            .Select(a => new PrayerRequestViewModel()
            {
                PrayerRequestTitle = a.PrayerRequestTitle,
                PrayerRequestDetails = a.PrayerRequestDetails,
                Id = a.Id,
            }).FirstOrDefault();

            return prayerRequestViewModel;
        }
        public bool SaveEditedRequest(PrayerRequestViewModel requestDetails, ApplicationUser loggedInUser)
        {
            if (requestDetails != null && loggedInUser != null)
            {
                var editRequest = _context.PrayerRequests.Where(x => x.Id == requestDetails.Id && x.Active && !x.Deleted).FirstOrDefault();
                if (editRequest != null)
                {
                    editRequest.PrayerRequestTitle = requestDetails.PrayerRequestTitle;
                    editRequest.PrayerRequestDetails = requestDetails.PrayerRequestDetails;
                    editRequest.UserId = loggedInUser.Id;
                    
                    _context.Update(editRequest);
                    _context.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public bool ApproveComment(int id)
        {
            var dd = _context.Comments.Where(x => x.Id == id && x.CommentStatus == YYFEnums.StatusEnum.Pending).FirstOrDefault();
            if (dd != null)
            {
                dd.CommentStatus = YYFEnums.StatusEnum.Approved;
                _context.Update(dd);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool DeclineComment(int id)
        {
            var dd = _context.Comments.Where(x => x.Id == id && x.CommentStatus == YYFEnums.StatusEnum.Pending).FirstOrDefault();
            if (dd != null)
            {
                dd.CommentStatus = YYFEnums.StatusEnum.Declined;
                _context.Update(dd);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public int GetTotalAnnouncements()
        {
            return _context.Announcements.Where(x => x.Id > 0 && x.Active && !x.Deleted).Count();
        }
        public int GetTotalRequests(string userId)
        {
            if (userId != null)
            {
                return _context.PrayerRequests.Where(x => x.UserId == userId && x.Active && !x.Deleted).Count();
            }
            return 0;
        }
        public int GetTotalEvents()
        {
            return _context.UpComingEvents.Where(x => x.Id > 0 && x.Active && !x.Deleted).Count();
        }

        public int GetTotalDiscussions()
        {
            return _context.Discussions.Where(x => x.Id > 0 && x.Active && !x.Deleted).Count();
        }
        public List<ApplicationUserViewModel> ListofUsers()
        {
            var appViewModel = new List<ApplicationUserViewModel>();
                appViewModel = _context.ApplicationUsers.Where(a => a.Id != null && !a.Deactivated)
                .Include(x => x.Gender)
                .Select(a => new ApplicationUserViewModel()
                {
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    UserName = a.UserName,
                    PhoneNumber = a.PhoneNumber,
                    DateCreated = a.DateCreated,
                    Email = a.Email,
                    GenderName = a.Gender.Name,
                    Id = a.Id,
                }).ToList();

            return appViewModel;
        }

        public bool DeactivateUser(string userId)
        {
            var userToDeactivate = _context.ApplicationUsers.Where(a => a.Id == userId && a.UserName != null && !a.Deactivated).FirstOrDefault();
            if (userToDeactivate != null)
            {
                userToDeactivate.Deactivated = true;
                _context.Update(userToDeactivate);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public ApplicationUserViewModel GetUserDetails(string userId)
        {
            if (userId != null)
            {
                var appViewModel = new ApplicationUserViewModel();
                    appViewModel = _context.ApplicationUsers.Where(a => a.Id == userId && !a.Deactivated)
                .Include(x => x.Gender)
                .Select(a => new ApplicationUserViewModel()
                {
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    UserName = a.UserName,
                    PhoneNumber = a.PhoneNumber,
                    DateCreated = a.DateCreated,
                    Email = a.Email,
                    GenderName = a.Gender.Name,
                    Id = a.Id,
                    Name = a.FirstName + " " + a.LastName,
                    ProfileImage = a.ProfileImage,
                }).FirstOrDefault();

                return appViewModel;
            }
            return null;
        }

        public bool SaveEditedProfile(ApplicationUserViewModel profileDetails, string base64)
        {
            if (profileDetails != null)
            {
                var edit = _context.ApplicationUsers.Where(x => x.Id == profileDetails.Id && !x.Deactivated).FirstOrDefault();
                var image = edit?.ProfileImage;
                if (edit != null)
                {
                    edit.FirstName = profileDetails.FirstName;
                    edit.LastName = profileDetails.LastName;
                    edit.PhoneNumber = profileDetails.PhoneNumber;
                    edit.Email = profileDetails.Email;
                    edit.ProfileImage = base64 != null ? base64 : image;

                    _context.Update(edit);
                    _context.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public bool AddAnnouncements(AnnouncementViewModel announcenent, ApplicationUser loggedInUser)
        {
            if (announcenent != null)
            {
                var addAnnouncement = new Announcements()
                {
                    AnnouncementTitle = announcenent?.AnnouncementTitle,
                    AnnouncementDetails = announcenent?.AnnouncementDetails,
                    DurationFrom = announcenent.DurationFrom,
                    DurationTill = announcenent.DurationTill,
                    DateCreated = DateTime.Now,
                    Active = true,
                    Deleted = false,
                    UserId = loggedInUser.Id,
                };
                _context.Add(addAnnouncement);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        public List<AnnouncementViewModel> ListofAnnouncement()
        {
            var announcementViewModel = new List<AnnouncementViewModel>();
            announcementViewModel = _context.Announcements.Where(a => a.Id > 0 && a.Active && !a.Deleted)
            .Select(a => new AnnouncementViewModel()
            {
                AnnouncementTitle = a.AnnouncementTitle,
                AnnouncementDetails = a.AnnouncementDetails,
                DurationFrom = a.DurationFrom,
                DurationTill = a.DurationTill,
                DateCreated = a.DateCreated,
                Id = a.Id,
            }).ToList();

            return announcementViewModel;
        }
       



    }
}
