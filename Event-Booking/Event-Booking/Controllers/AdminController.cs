using Microsoft.AspNetCore.Mvc;

namespace Event_Booking.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
