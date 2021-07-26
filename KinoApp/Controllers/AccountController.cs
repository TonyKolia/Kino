using Kino.Models.KinoDBModel;
using KinoApp.Helpers;
using KinoApp.Models;
using KinoApp.ServiceHelpers;
using KinoApp.ServiceHelpers.Draws;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KinoApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;

        private readonly IServiceMethods serviceMethods;

        public AccountController(ILogger<AccountController> logger, IServiceMethods serviceMethods, IHttpContextAccessor httpContext)
        {
            _logger = logger;
            this.serviceMethods = serviceMethods;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!AddUser(registerViewModel))
            {
                return View();
            }

            if (await LoginUser(registerViewModel.Username, registerViewModel.Password) == false)
            {
                return Redirect("/Account/Login");
            }

            return Redirect("/Home/Index");
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            if(string.IsNullOrEmpty(returnUrl) || returnUrl == "/")
            {
                returnUrl = "~/Home/Index";
            }
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel, string returnUrl)
        {
            if (await LoginUser(loginViewModel.Username, loginViewModel.Password) == false)
            {
                return View();
            }

            return Redirect(returnUrl);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/Home/Index");
        }


        #region HelperMethods

        private async Task<bool> LoginUser(string username, string password)
        {
            if (ModelState.IsValid)
            {
                var user = serviceMethods.PerformGet<User>(UserServiceEndpoints.ValidateUserCredentials, new[] { username, GeneralHelper.Encrypt(password) }, out var result);
                if(result.Success)
                {
                    var claims = new List<Claim>
                    {
                        new Claim("Id", user.Id.ToString()),
                        new Claim(ClaimTypes.NameIdentifier, user.Username),
                        new Claim(ClaimTypes.Role, user.Category.ToString()),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim("BirthDate", user.Birthdate.ToString()),
                        new Claim("RegistrationDate", user.RegisterDate.ToString())
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    await HttpContext.SignInAsync(claimsPrincipal);
                    return true;
                }
                else
                {
                    ModelState.AddModelError("InvalidCredentials", result.Message);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private bool UserOver18(DateTime userBirthDate)
        {
            return DateTime.Now.Year - userBirthDate.Year >= 18;
        }

        private bool AddUser(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                if (UserOver18(registerViewModel.BirthDate.Value))
                {
                    var user = new User
                    {
                        Birthdate = registerViewModel.BirthDate,
                        Category = 1,
                        Email = registerViewModel.Email,
                        Password = GeneralHelper.Encrypt(registerViewModel.Password),
                        RegisterDate = DateTime.Now,
                        Username = registerViewModel.Username
                    };

                    serviceMethods.PerformPost(UserServiceEndpoints.AddUser, null, user, out var result);
                    if (!result.Success)
                    {
                        ModelState.AddModelError("UserExists", result.Message);
                        return false;
                    }

                    return true;
                }
                else
                {
                    ModelState.AddModelError("BirthDate", "You must be over 18 to register");
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}
