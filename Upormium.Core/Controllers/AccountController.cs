using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Upormium.Model.ApplicationClasses.Accounts;
using Upormium.Model.Models.Users;
using Upormium.Util.StringConstants;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Upormium.Controllers
{
    public class AccountController: Controller
    {
        #region Private Variables
        private readonly ILogger<HomeController> _logger;
        private readonly IStringConstant _stringConstant;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        #endregion
        #region Contructor
        public AccountController(
            ILogger<HomeController> logger,
            IStringConstant stringConstant,
            UserManager<User> userManager,
            SignInManager<User> signInManager
            )
        {
            _logger = logger;
            _stringConstant = stringConstant;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        #endregion
        #region Public Methods

        /// <summary>
        /// To validate if already logged in, if not return View
        /// </summary>
        /// <param name="returnUrl">URL to return to after login</param>
        /// <returns>View of Log in page or redirect to home page</returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult LogIn(string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        /// <summary>
        /// To login if email and password are correct
        /// </summary>
        /// <param name="loginModel">Details of user email and password</param>
        /// <param name="returnUrl">URL to redirect to after login</param>
        /// <returns>View of the url or error message</returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel loginModel, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, _stringConstant.InvalidModelError);
                return View(loginModel);
            }

            User user = await _userManager.FindByEmailAsync(loginModel.Email);
            if(user == null)
            {
                ModelState.AddModelError(string.Empty, _stringConstant.InvalidLoginError);
                return View(loginModel);
            }

            SignInResult result = await _signInManager.PasswordSignInAsync(loginModel.Email, loginModel.Password, isPersistent: false, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, _stringConstant.InvalidLoginError);
                return View(loginModel);
            }

            return Redirect(returnUrl);
        }
        #endregion
        #region Private Methods
        #endregion
    }
}
