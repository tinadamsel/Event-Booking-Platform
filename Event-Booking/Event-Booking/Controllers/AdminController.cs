using Core.config;
using Core.DB;
using Core.Models;
using Core.ViewModels;
using Logic.IHelpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Event_Booking.Controllers
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
            return View();
        }

        public IActionResult Events()
        {
            var eventDetails = _userHelper.GetEvents();
            return View(eventDetails);
        }
        public JsonResult CreateEvent(string eventDetails)
        {
            if (eventDetails != null)
            {
                var eventViewModel = JsonConvert.DeserializeObject<EventsViewModel>(eventDetails);
                if (eventViewModel != null)
                {
                    var checkeventName = _userHelper.CheckExistingeventName(eventViewModel?.Title);
                    if (!checkeventName)
                    {
                        var creteEvent = _userHelper.CreateEvent(eventViewModel);
                        if (creteEvent)
                        {
                            return Json(new { isError = false, msg = "Event Created Successfully" });
                        }
                        return Json(new { isError = true, msg = "Unable to Create" });
                    }
                    return Json(new { isError = true, msg = "Event Already Created" });
                }
            }
            return Json(new { isError = true, msg = "Network Failure" });
        }

        public JsonResult EditEvent(int Id)
        {
            if (Id > 0)
            {
                var eventToEdit = _userHelper.GetEventToEdit(Id);
                if (eventToEdit != null)
                {
                    return Json(eventToEdit);
                }
                return Json(new { isError = true, msg = "Unable To Get Event" });
            }
            return Json(new { isError = true, msg = "Network Error" });
        }

        public JsonResult EditedEvent(string eventDetails)
        {
            if (eventDetails != null)
            {
                var eventViewModel = JsonConvert.DeserializeObject<EventsViewModel>(eventDetails);
                if (eventViewModel != null)
                {
                    var editDepartment = _userHelper.SaveEditedEvent(eventViewModel);
                    if (editDepartment)
                    {
                        return Json(new { isError = false, msg = "Event Edited Successfully" });
                    }
                    return Json(new { isError = true, msg = "Unable to Edit Event" });
                }
            }
            return Json(new { isError = true, msg = "Network Error" });
        }

        public JsonResult DeleteEvent(int id)
        {
            if (id > 0)
            {
                var deleteEvent = _userHelper.DeleteEvent(id);
                if (deleteEvent)
                {
                    return Json(new { isError = false, msg = "Event Deleted successfully" });
                }
                return Json(new { isError = true, msg = "Unable To Delete Event" });
            }
            return Json(new { isError = true, msg = "Network Error" });
        }

    }
}
