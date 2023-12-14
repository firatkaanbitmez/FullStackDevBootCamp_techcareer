//Data/ApplicationUser.cs
using Microsoft.AspNetCore.Identity;

namespace ShopAppProject.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }

        public string? FullName => $"{FirstName} {LastName}";

    }
}
