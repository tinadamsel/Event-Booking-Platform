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
        bool CheckExistingeventName(string title);
        bool checkIfEmailExists(string email);
        bool CheckIfUserIsDeactivated(string email);
        bool CreateEvent(EventsViewModel eventsViewModel);
        EventBookings CreateEventBooking(EventBookingsViewModel bookingViewModel);
        bool DeleteEvent(int id);
        Task<ApplicationUser> FindByEmailAsync(string email);
        ApplicationUser FindById(string Id);
        ApplicationUser FindByUserName(string username);
        Task<ApplicationUser> FindByUserNameAsync(string username);
        string GetCurrentUserId(string username);
        EventsViewModel GetEventDetailsById(int id);
        List<EventsViewModel> GetEvents();
        EventsViewModel GetEventToEdit(int id);
        string GetUserId(string username);
        string GetUserRole(string userId);
        Task<bool> RegisterAdmin(ApplicationUserViewModel userDetails);
        Task<bool> RegisterUser(ApplicationUserViewModel userDetails);
        bool SaveEditedEvent(EventsViewModel eventsViewModel);
    }
}
