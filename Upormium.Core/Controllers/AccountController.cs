using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Upormium.Controllers
{
    public class AccountController: Controller
    {
        #region Private Variables
        private readonly ILogger<HomeController> _logger;
        #endregion
        #region Contructor
        public AccountController(
            ILogger<HomeController> logger
            )
        {
            _logger = logger;
        }
        #endregion
        #region Public Methods

        /// <summary>
        /// To Validate if already logged in, if not return View
        /// </summary>
        /// <param name="returnUrl">URL to return to after login</param>
        /// <returns>View of Log in page or redirect to home page</returns>
        public IActionResult LogIn(string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        #endregion
        #region Private Methods
        #endregion
    }
}
