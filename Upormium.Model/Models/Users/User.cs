using Microsoft.AspNetCore.Identity;
using System;

namespace Upormium.DomainModel.Models.Users
{
    public class User: IdentityUser
    {
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}
