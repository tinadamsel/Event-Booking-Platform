using Core.config;
using Core.DB;
using Core.Models;
using Logic.IHelpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Event_Booking.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserHelper _userHelper;
        private readonly IGeneralConfiguration _generalConfiguration;

        public AccountController(AppDbContext context, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IUserHelper userHelper, IGeneralConfiguration generalConfiguration)
        {
            _signInManager = signInManager;
            _context = context;
            _userManager = userManager;
            _userHelper = userHelper;
            _generalConfiguration = generalConfiguration;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        
        public IActionResult Login()
        {
            return View();
        }


    }
}
