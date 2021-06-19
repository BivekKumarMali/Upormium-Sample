using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using Upormium.DomainModel.ApplicationClasses;
using Upormium.DomainModel.Models.Users;
using Upormium.Util.StringConstants;

namespace Upormium.Util.SeedDatabase
{
    public class SeedDatabase
    {
        #region Private Variables
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly IStringConstant _stringConstant;
        private readonly AdminDetails _adminDetails;
        #endregion
        #region Contructors
        public SeedDatabase(
            RoleManager<IdentityRole> roleManager,
            IStringConstant stringConstant,
            UserManager<User> userManager,
            AdminDetails adminDetails
            )
        {
            _roleManager = roleManager;
            _stringConstant = stringConstant;
            _userManager = userManager;
            _adminDetails = adminDetails;
        }
        #endregion
        public async Task SeedAsync()
        {
            await SeedAllRolesAsync();
            await SeedDefaultAdminAsync();
        }

        /// <summary>
        /// To add default admin
        /// </summary>
        /// <returns></returns>
        private async Task SeedDefaultAdminAsync()
        {
            User user = await _userManager.FindByEmailAsync(_adminDetails.Email);
            if (user == null)
            {
                user = new User()
                {
                    Name = _adminDetails.Name,
                    UserName = _adminDetails.Name,
                    Email = _adminDetails.Email,
                    CreatedDateTime = DateTime.UtcNow
                };
                await _userManager.CreateAsync(user, _adminDetails.Password);
                await _userManager.AddToRoleAsync(user, _stringConstant.Admin);
            }
        }

        /// <summary>
        /// To adds required roles
        /// </summary>
        /// <returns></returns>
        private async Task SeedAllRolesAsync()
        {
            if (!await _roleManager.RoleExistsAsync(_stringConstant.Admin))
            {
                await _roleManager.CreateAsync(new IdentityRole(_stringConstant.Admin));
            }
        }
    }
}
