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
        List<UpComingEventViewModel> ListofEvents();
        UpComingEvents GetDetails(int id);
    }
}
