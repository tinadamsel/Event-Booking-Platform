using Core.DB;
using Core.ViewModels;
using Logic.IHelpers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Event_Booking.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserHelper _userHelper;
        private readonly AppDbContext _context;
        public UserController(IUserHelper userHelper, AppDbContext context)
        {
            _userHelper = userHelper;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Events()
        {
            var eventList = _userHelper.GetEvents();
            return View(eventList);
        }
        public IActionResult EventDetailPage(int id)
        {
            if (id > 0)
            {
                var getEventDetails = _userHelper.GetEventDetailsById(id);
                if (getEventDetails != null)
                {
                    return View(getEventDetails);
                }
            }
            return RedirectToAction(" Events", "User");
        }

        //This is a partial view 
        public IActionResult BookingForm(int eventid)
        {
            if (eventid > 0)
            {
                var currentUser = _userHelper.FindByUserName(User.Identity.Name);
                if (currentUser != null)
                {
                    var model = new EventBookingsViewModel()
                    {
                        BookerId = currentUser.Id,
                        EventId = eventid,
                    };
                    return PartialView(model);
                }
                return RedirectToAction("Login","Account");
            }
            return View();
        }

        public JsonResult BookEvent(string bookingDetails)
        {
            if (bookingDetails != null)
            {
                var bookingViewModel = JsonConvert.DeserializeObject<EventBookingsViewModel>(bookingDetails);
                if (bookingViewModel != null)
                {
                    if (bookingViewModel.Name == "" || bookingViewModel.Name == null)
                    {
                        return Json(new { isError = true, msg = "Please fill in your name" });
                    }
                    var checkEmail = _userHelper.FindByEmailAsync(bookingViewModel.Email);
                    if (checkEmail != null)
                    {
                        return Json(new { isError = true, msg = "Email Already Exists" });
                    }
                    var creteEventbooking = _userHelper.CreateEventBooking(bookingViewModel);
                    if (creteEventbooking)
                    {
                        return Json(new { isError = false, msg = "Event Created Successfully" });
                    }
                    return Json(new { isError = true, msg = "Unable to Create" });
                }
            }
            return Json(new { isError = true, msg = "Network Failure" });
        }





    }
}
