using Core.DB;
using Core.Models;
using Core.ViewModels;
using Logic.IHelpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
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
    }
}
