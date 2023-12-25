//Data/ApplicationUser.cs
using Microsoft.AspNetCore.Identity;
using ShopAppProject.Models;

namespace ShopAppProject.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public string? Address { get; set; }
        public new string? PhoneNumber { get; set; }

        public string? FullName => $"{FirstName} {LastName}";
        public ICollection<UserProductList>? UserProductLists { get; set; }

        public string? BusinessCompany { get; set; }
        public string? BusinessID { get; set; }
        public string? BusinessMail { get; set; }
        public string? BusinessAddress { get; set; }
        public string? BusinessPhoneNumber { get; set; }
        public Wallet? Wallet { get; set; }

    }
}
