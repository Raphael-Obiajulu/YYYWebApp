using Core.Models;
using Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.IHelpers
{
    public interface IUserHelper
    {
        Task<bool> CreateUser(ApplicationUserViewModel applicationUserViewModel);
        Task<ApplicationUser>? FindByEmailAsync(string email);
        ApplicationUser FindById(string Id);
        Task<ApplicationUser> FindByUserNameAsync(string username);
        Task<ApplicationUser> FindUserById(string id);
        bool CheckEventName(string eventTitle);
        bool CreateEvent(UpComingEventViewModel upComingEvents, string base64);
        UpComingEvents GetDetails(int id);
        List<PrayerRequestViewModel> ListofPrayerRequest();
        List<UpComingEventViewModel> ListofEvents();
        bool AddPrayerRequest(PrayerRequestViewModel prayerRequest, ApplicationUser loggedInUser);
        List<PrayerRequestViewModel> ApprovedPrayerRequest();
        List<PrayerRequestViewModel> ListofUsersPrayerRequest(string loggedInUser);
        bool ApproveRequest(int id);
        bool DeclineRequest(int id);
        List<CommentsViewModel> ListofComments();
        List<DiscussionForumViewModel> ListofDiscussions();
        bool AddDiscussion(DiscussionForumViewModel discussion, ApplicationUser loggedInUser);
        Comment CreateComment(string message, int id, ApplicationUser loggedInUser);
        int TotalLikes(int discussionId);
        DiscussionForumViewModel GetDiscussion(int discussionId);
        int TotalComments(int discussionId);
        bool CheckUserLike(string userId, int id);
        bool AddLike(int id, ApplicationUser loggedInUser);
        bool CheckRequestStatus(int requestId);
        PrayerRequestViewModel GetRequest(int id);
        bool SaveEditedRequest(PrayerRequestViewModel requestDetails, ApplicationUser loggedInUser);
        bool ApproveComment(int id);
        bool DeclineComment(int id);
        Task<bool> CreateAdmin(ApplicationUserViewModel applicationUserViewModel);
        int GetTotalAnnouncements();
        int GetTotalEvents();
        int GetTotalRequests(string userId);
        int GetTotalDiscussions();
        List<ApplicationUserViewModel> ListofUsers();
        bool DeactivateUser(string userId);

        bool ReactivateUser(string userId);
        ApplicationUserViewModel GetUserDetails(string userId);
        bool SaveEditedProfile(ApplicationUserViewModel profileDetails, string base64);
        bool AddAnnouncements(AnnouncementViewModel announcenent, ApplicationUser loggedInUser);
        List<AnnouncementViewModel> ListofAnnouncement();
        bool CheckIfDeactivated(string userName);
		bool DeleteEvent(int id);
        bool DeleteDiscussion(int id);
        DiscussionForumViewModel GetDisccusion(int id);
        bool EditDiscussion(DiscussionForumViewModel discussion, ApplicationUser loggedInUser);
        bool DeleteAnnounce(int id);
        List<AnnouncementViewModel> ListofAnnouncementForAdmin();
        AnnouncementViewModel GetAnnouncement(int id);
        bool EditAnnouncement(AnnouncementViewModel announcementdetails, ApplicationUser loggedInUser);
        bool DeletePrayer(int id);
        List<BibleStudyViewModel> listofBibleStudy();

        //BibleStudyViewModel BibleStudy(int id);
        bool CreateBibleStudy(BibleStudyViewModel biblestudyDetails, ApplicationUser loggedInUser);
        //bool BibleStudy(BibleStudyViewModel bibleStudy, ApplicationUser loggedInUser);

        bool DeleteBibleStudy(int id);
        bool SaveMediaGallery(MediaGalleryViewModel mediaDetails, string base64, string userId);
        bool DeleteMediaGallery(int id);
        List<MediaGalleryViewModel> ListofMedia();
        BibleStudyViewModel Getbiblestudy(int id);

        bool SaveEditedBibleStudy(BibleStudyViewModel biblestudy, ApplicationUser loggedInUser);
        List<Video> ListofVideos();
        bool SaveVideo(Video videoDetails);
    }
}
