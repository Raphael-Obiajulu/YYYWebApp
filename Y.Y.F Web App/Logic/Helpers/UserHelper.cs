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
        public static object listofEvents;

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
            if (upComingEvents != null && base64 !=null) 
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
    }
}
