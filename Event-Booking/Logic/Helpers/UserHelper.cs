using Core.config;
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
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IGeneralConfiguration _generalConfiguration;

        public UserHelper(AppDbContext context, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _context = context;
            _userManager = userManager;
        }

        public async Task<ApplicationUser> FindByEmailAsync(string email)
        {
            return await _userManager.Users.Where(s => s.Email == email)?.FirstOrDefaultAsync();
        }
        public ApplicationUser FindByUserName(string username)
        {
            return _userManager.Users.Where(s => s.UserName == username).FirstOrDefault();
        }
        public async Task<ApplicationUser> FindByUserNameAsync(string username)
        {
            return await _userManager.Users.Where(s => s.UserName == username).FirstOrDefaultAsync();
        }

        public string GetUserRole(string userId)
        {
            if (userId != null)
            {
                var userRole = _context.UserRoles.Where(x => x.UserId == userId).FirstOrDefault();
                if (userRole != null)
                {
                    var roles = _context.Roles.Where(x => x.Id == userRole.RoleId).FirstOrDefault();
                    if (roles != null)
                    {
                        return roles.Name;
                    }
                }
            }
            return null;
        }
        public string GetUserId(string username)
        {
            return _userManager.Users.Where(s => s.UserName == username)?.FirstOrDefaultAsync().Result.Id?.ToString();
        }
        public ApplicationUser FindById(string Id)
        {
            return _userManager.Users.Where(s => s.Id == Id).FirstOrDefault();
        }
        public string GetCurrentUserId(string username)
        {
            try
            {
                if (username != null)
                {
                    return _userManager.Users.Where(s => s.UserName == username)?.FirstOrDefault()?.Id?.ToString();
                }
                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> RegisterUser(ApplicationUserViewModel userDetails)
        {
            try
            {
                if (userDetails != null)
                {
                    var user = new ApplicationUser();
                    user.UserName = userDetails.Email;
                    user.Email = userDetails.Email;
                    user.FirstName = userDetails.FirstName;
                    user.LastName = userDetails.LastName;
                    user.PhoneNumber = userDetails.Phonenumber;
                    user.DateRegistered = DateTime.Now;
                    user.Deactivated = false;
                    user.IsAdmin = false;
                    var createUser = await _userManager.CreateAsync(user, userDetails.Password).ConfigureAwait(false);
                    if (createUser.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, "User").ConfigureAwait(false);
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> RegisterAdmin(ApplicationUserViewModel userDetails)
        {
            try
            {
                if (userDetails != null)
                {
                    var user = new ApplicationUser();
                    user.UserName = userDetails.Email;
                    user.Email = userDetails.Email;
                    user.FirstName = userDetails.FirstName;
                    user.LastName = userDetails.LastName;
                    user.PhoneNumber = userDetails.Phonenumber;
                    user.DateRegistered = DateTime.Now;
                    user.Deactivated = false;
                    user.IsAdmin = false;
                    var createUser = await _userManager.CreateAsync(user, userDetails.Password).ConfigureAwait(false);
                    if (createUser.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, "Admin").ConfigureAwait(false);
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool CheckIfUserIsDeactivated(string email)
        {
            if (email != null)
            {
                var CheckUser = _context.ApplicationUser.Where(x => x.Email == email && !x.Deactivated).FirstOrDefault();
                if (CheckUser != null)
                {
                    return true;
                }
            }
            return false;
        }

        public bool CheckExistingeventName(string title)
        {
            if (title != null)
            {
                var checkeventName = _context.Events.Where(x => x.Title == title && x.Active).FirstOrDefault();
                if (checkeventName != null)
                {
                    return true;
                }
            }
            return false;
        }

        public bool CreateEvent(EventsViewModel eventsViewModel)
        {
            if (eventsViewModel != null)
            {
                var creatEvent = new Events()
                {
                    Title = eventsViewModel.Title,
                    Summary = eventsViewModel.Summary,
                    EventLocation = eventsViewModel.EventLocation,
                    EventCapacity = eventsViewModel.EventCapacity,
                    EventDate = eventsViewModel.EventDate,
                    DateCreated = DateTime.Now,
                    Active = true,
                };
                _context.Events.Add(creatEvent);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public List<EventsViewModel> GetEvents()
        {
            var eventViewModel = new List<EventsViewModel>();
            eventViewModel = _context.Events.Where(x => x.Id > 0 && x.Active)
                .Select(x => new EventsViewModel()
                {
                    Id = x.Id,
                    Title = x.Title,
                    Summary = x.Summary,
                    EventDate = x.EventDate,
                    EventLocation = x.EventLocation,
                    EventCapacity = x.EventCapacity,
                    DateCreated = x.DateCreated,
                }).ToList();
            return eventViewModel;
        }

        public EventsViewModel GetEventToEdit(int id)
        {
            var eventToEdit = _context.Events.Where(a => a.Id == id && a.Active)
                .Select(a => new EventsViewModel()
                {
                    Title = a.Title,
                    Summary = a.Summary,
                    EventLocation = a.EventLocation,
                    EventCapacity = a.EventCapacity,
                    EventDate =     a.EventDate,
                    DateCreated =  a.DateCreated,
                    Id = a.Id,
                    Active = true,
                }).FirstOrDefault();
            if (eventToEdit != null)
            {
                return eventToEdit;
            }
            return null;
        }

        public bool SaveEditedEvent(EventsViewModel eventsViewModel)
        {
            if (eventsViewModel != null)
            {
                var editEvent = _context.Events.Where(x => x.Id == eventsViewModel.Id && x.Active).FirstOrDefault();
                if (editEvent != null)
                {
                    editEvent.Title = eventsViewModel.Title;
                    editEvent.EventCapacity = eventsViewModel.EventCapacity;
                    editEvent.EventLocation = eventsViewModel.EventLocation;
                    editEvent.EventDate = eventsViewModel.EventDate;
                    editEvent.Summary = eventsViewModel.Summary;
                }
                _context.Update(editEvent);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool DeleteEvent(int id)
        {
            var eventToDelete = _context.Events.Where(a => a.Id == id && a.Active).FirstOrDefault();
            if (eventToDelete != null)
            {
                eventToDelete.Active = false;
                _context.Update(eventToDelete);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public EventsViewModel GetEventDetailsById(int id)
        {
            var getEvent = _context.Events.Where(a => a.Id == id && a.Active)
                .Select(a => new EventsViewModel()
                {
                    Title = a.Title,
                    Summary = a.Summary,
                    EventLocation = a.EventLocation,
                    EventCapacity = a.EventCapacity,
                    EventDate = a.EventDate,
                    DateCreated = a.DateCreated,
                    Id = a.Id,
                    Active = true,
                }).FirstOrDefault();
            if (getEvent != null)
            {
                return getEvent;
            }
            return null;
        }

        public bool CreateEventBooking(EventBookingsViewModel bookingViewModel)
        {
            if (bookingViewModel != null)
            {
                var creatEventbooking = new EventBookings()
                {
                    EventId = bookingViewModel.EventId,
                    BookerId = bookingViewModel.BookerId,
                    Name = bookingViewModel.Name,
                    Email = bookingViewModel.Email,
                    Note = bookingViewModel.Note,
                    BookingStatus = EventEnum.BookingStatus.Pending,
                    DateCreated = DateTime.Now,
                    Active = true,
                };
                _context.Bookings.Add(creatEventbooking);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
