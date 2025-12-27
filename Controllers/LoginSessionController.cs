using Asp.NetCoreLoginWithSession.Models;
using Asp.NetCoreLoginWithSession.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Asp.NetCoreLoginWithSession.Controllers
{
    public class LoginSessionController : Controller
    {
        private IUserService _userService;
        public LoginSessionController(IUserService userService)
        {
            _userService=userService;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string UserEmail,string Password)
        {
            var data=await _userService.ValidateUser(UserEmail, Password);
            if (data != null)
            {
                //HttpContext.Session.SetString("Usermail", data.UserEmail);
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, data.UserEmail),
                    new Claim(ClaimTypes.Name,data.UserName)
                };
                var identity=new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
                var principal=new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                return RedirectToAction("Dashboard");
            }
            else
            {
                ViewBag.error = "invalid user name or password";
                return View();
            }
        }


        public IActionResult Registration() 
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Registration(User usr)
        {
            await _userService.AddUserAsync(usr);
            return RedirectToAction("Login");
        }

        public async Task<IActionResult> Dashboard()
        {
            //var Email = HttpContext.Session.GetString("Usermail");
            var Email=User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userService.GetUserAsync(Email);
            return View(user);
        }

        public async Task<IActionResult> Logout()
        {
            //HttpContext.Session.Remove("Usermail");
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }

        public async Task<IActionResult> EditProfile(string Email)
        {
            var data = await _userService.GetUserAsync(Email);
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> EditProfile(User usr)
        {
            //var data=HttpContext.Session.GetString("Usermail");
            //var user=await _userService.GetUserAsync(data);
            //usr.UserId = user.UserId;
            await _userService.UpdateUserAsync(usr);
            return RedirectToAction("Dashboard");
        }

        public IActionResult ChangePassword()
        {
            return View();
        }


        [HttpPost]
        public async  Task<IActionResult> ChangePassword(ChangePass cps)
        {
            var data=await _userService.ChangePass(cps);
            if(data==1)
            {
                TempData["msg"] = "Password Updated Successfully.";
                return RedirectToAction("Dashboard");
            }
            else
            {
                ViewBag.msg = "Invalid Current Password.";
                ModelState.Clear();
                return View();
            }
        }
    }
}
