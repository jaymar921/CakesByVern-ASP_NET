using CakesByVern_ASP_NET_WEB.Models.Auth;
using CakesByVern_Data.Database;
using CakesByVern_Data.Entity;
using CakesByVern_Data.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CakesByVern_ASP_NET_WEB.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly IDataRepository dataRepository;

        public AccountController(IDataRepository dataRepository)
        {
            this.dataRepository = dataRepository;
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl = "/")
        {
            return View(new AccountModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(AccountModel account)
        {
            if(account == null)
            {
                TempData["Message"] = "Account is null";
                return View();
            }
            else
            {
                string username = account.Username;
                string password = account.Password;
                User? user = dataRepository.GetUser(username, password.MD5Hash());
               
                if(user!=null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.FullName),
                        new Claim(ClaimTypes.Name, user.Credential.UserName),
                        new Claim(ClaimTypes.Role, user.Role)
                    };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties { IsPersistent = false });
                    return LocalRedirect(account.ReturnUrl);
                }
                else
                {
                    if(dataRepository.GetUser(username) != null)
                        TempData["Message"] = "Invalid Password";
                    else
                        TempData["Message"] = "Account not found";
                    return View();
                }
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Register(UserModel account)
        {

            if (dataRepository.GetUser(account.Username) != null)
            {
                TempData["Message"] = "Account already exists!";
                return View();
            }

            bool status = dataRepository.RegisterUser(new User(0, account.Fullname, DateOnly.FromDateTime(account.Birthdate), account.Email, "CLIENT", new CakesByVern_Data.Security.Credential(account.Username, account.Password.MD5Hash())));

            if(!status)
            {
                TempData["Message"] = "Failed to register account!";
                return View();
            }

            TempData["Message"] = "Account Created";
            return RedirectToAction("Login", "Account");
        }
    }
}
