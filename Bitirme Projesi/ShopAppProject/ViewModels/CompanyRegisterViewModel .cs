//Data/CompanyRegisterViewModel.cs
using System;
using System.ComponentModel.DataAnnotations;

namespace ShopAppProject.Data
{
    public class CompanyRegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string? ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string? FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string? LastName { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string? Address { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string? PhoneNumber { get; set; }




        [Required]
        public string? BusinessCompany { get; set; }

        [Required]
        [Display(Name = "Business ID")]
        public string? BusinessID { get; set; }

        [Required]
        [Display(Name = "Business Mail")]
        public string? BusinessMail { get; set; }

        [Required]
        [Display(Name = "Business Address")]
        public string? BusinessAddress { get; set; }

        [Required]
        [Display(Name = "Business Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string? BusinessPhoneNumber { get; set; }

    }


}
