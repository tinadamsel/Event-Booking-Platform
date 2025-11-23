using Microsoft.AspNetCore.Mvc;

namespace Event_Booking.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
