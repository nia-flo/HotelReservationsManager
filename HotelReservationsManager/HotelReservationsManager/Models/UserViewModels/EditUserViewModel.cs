using HotelReservationsManager.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace HotelReservationsManager.Models.UserViewModels
{
    public class EditUserViewModel
    {
        public string Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        [Display(Name = "Username")]
        [RegularExpression("^[a-zA-Z0-9.-]*$", ErrorMessage = "Only letters, numbers, periods and dashes allowed.")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        [RegularExpression("^[a-zA-Z-]*$", ErrorMessage = "Only letters and dashes allowed.")]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        [RegularExpression("^[a-zA-Z-]*$", ErrorMessage = "Only letters and dashes allowed.")]
        [Display(Name = "Middle name")]
        public string MiddleName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        [RegularExpression("^[a-zA-Z-]*$", ErrorMessage = "Only letters and dashes allowed.")]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Only numbers allowed.")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "The {0} must be 10 characters")]
        [Display(Name = "EGN")]
        public string EGN { get; set; }

        [Required]
        //[Phone]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Only numbers allowed.")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "The {0} must be 10 characters")]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Hire date")] 
        public DateTime HireDate { get; set; }

        public bool IsActive { get; set; }

        [Required]
        [DateGreaterThan("HireDate")]
        [Display(Name = "Dismiss date")]
        public DateTime DismissDate { get; set; }
    }
}
