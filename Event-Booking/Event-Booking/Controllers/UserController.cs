using Core.DB;
using Core.ViewModels;
using Logic.IHelpers;
using Logic.Service;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;
using static Core.DTO.MemberApiDTO;

namespace Event_Booking.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserHelper _userHelper;
        private readonly AppDbContext _context;
        private readonly IMemberBaseService _memberBaseService;
        public UserController(IUserHelper userHelper, AppDbContext context, IMemberBaseService memberBaseService)
        {
            _userHelper = userHelper;
            _context = context;
            _memberBaseService = memberBaseService;
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

        public async Task<JsonResult> BookEvent(string bookingDetails)
        {
            if (bookingDetails != null)
            {
                var bookingViewModel = JsonConvert.DeserializeObject<EventBookingsViewModel>(bookingDetails);
                if (bookingViewModel != null)
                {
                    if (bookingViewModel.LastName == "" || bookingViewModel.FirstName == null)
                    {
                        return Json(new { isError = true, msg = "Please fill in your name" });
                    }
                    var checkEmail = _userHelper.checkIfEmailExists(bookingViewModel.Email);
                    if (checkEmail)
                    {
                        return Json(new { isError = true, msg = "Email Already Exists" });
                    }
                    var creteEventbooking = _userHelper.CreateEventBooking(bookingViewModel);
                    if (creteEventbooking != null)
                    {
                        var contactPayload = new CreateContactRequest()
                        {
                            firstName = bookingViewModel.FirstName,
                            surname = bookingViewModel.LastName,
                            emailAddress = bookingViewModel.Email,
                        };
                        var createContact = await _memberBaseService.CreateContactAsync(contactPayload);
                        if (createContact.Message != "Successful")
                        {
                            return Json(new { isError = true, msg = createContact.Message });
                        }
                        return Json(new { isError = false, msg = "BookingRecord Created Successfully" });
                    }
                    return Json(new { isError = true, msg = "Unable to Create" });
                }
            }
            return Json(new { isError = true, msg = "Network Failure" });
        }





    }
}
