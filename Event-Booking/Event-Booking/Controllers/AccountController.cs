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
        [HttpPost]
        public async Task<JsonResult> Registration(string userDetails)
        {
            if (userDetails != null)
            {
                var appUserViewModel = JsonConvert.DeserializeObject<ApplicationUserViewModel>(userDetails);
                if (appUserViewModel != null)
                {
                    var checkEmail = await _userHelper.FindByEmailAsync(appUserViewModel.Email).ConfigureAwait(false);
                    if (checkEmail != null)
                    {
                        return Json(new { isError = true, msg = "Email Already Exists" });
                    }
                    if (appUserViewModel.Password != appUserViewModel.ConfirmPassword)
                    {
                        return Json(new { isError = true, msg = "Password and Confirm password do not match" });
                    }
                    
                    var createUser = await _userHelper.RegisterUser(appUserViewModel).ConfigureAwait(false);
                    if (createUser)
                    {
                        return Json(new { isError = false, msg = "Registration Successful, Login to your email and follow the instructions" });
                    }
                    return Json(new { isError = true, msg = "Unable to register" });
                }
            }
            return Json(new { isError = true, msg = "Network Error" });
        }

        [HttpGet]
        public IActionResult AdminRegister()
        {
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> AdminRegistration(string userDetails)
        {
            if (userDetails != null)
            {
                var appUserViewModel = JsonConvert.DeserializeObject<ApplicationUserViewModel>(userDetails);
                if (appUserViewModel != null)
                {
                    var checkEmail = await _userHelper.FindByEmailAsync(appUserViewModel.Email).ConfigureAwait(false);
                    if (checkEmail != null)
                    {
                        return Json(new { isError = true, msg = "Email Already Exists" });
                    }
                    if (appUserViewModel.Password != appUserViewModel.ConfirmPassword)
                    {
                        return Json(new { isError = true, msg = "Password and Confirm password do not match" });
                    }

                    var createUser = await _userHelper.RegisterAdmin(appUserViewModel).ConfigureAwait(false);
                    if (createUser)
                    {
                        return Json(new { isError = false, msg = "Registration Successful, Login to your email and follow the instructions" });
                    }
                    return Json(new { isError = true, msg = "Unable to register" });
                }
            }
            return Json(new { isError = true, msg = "Network Error" });
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [HttpPost]
        public async Task<JsonResult> Login(string email, string password)
        {
            if (email != null && password != null)
            {
                var filterSpace = email.Replace(" ", "");
                var url = "";
                var existingUser = _userHelper.FindByEmailAsync(filterSpace).Result;
                if (existingUser != null)
                {
                    var checkforUserDeactivation = _userHelper.CheckIfUserIsDeactivated(email);
                    if (checkforUserDeactivation)
                    {
                        return Json(new { isError = true, msg = "You are deactivated" });
                    }
                    var PasswordSignIn = await _signInManager.PasswordSignInAsync(existingUser, password, true, true).ConfigureAwait(false);
                    if (PasswordSignIn.Succeeded)
                    {
                        var userRole = await _userManager.GetRolesAsync(existingUser).ConfigureAwait(false);
                        if (userRole.FirstOrDefault().ToLower().Contains("admin"))
                        {
                            url = "/Admin/Index";
                            return Json(new { isError = false, dashboard = url });
                        }
                        else
                        {
                            url = "/User/Index";
                            return Json(new { isError = false, dashboard = url });
                        }    
                    }
                    
                }
                return Json(new { isError = true, msg = "Account does not exist, contact admin" });
            }
            return Json(new { isError = true, msg = "Username and Password Required" });
        }
    }
}
