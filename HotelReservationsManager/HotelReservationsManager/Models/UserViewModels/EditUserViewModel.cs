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
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        [Display(Name = "Middle name")]
        public string MiddleName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*$")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "The {0} must be 10 characters")]
        [Display(Name = "EGN")]
        public string EGN { get; set; }

        [Required]
        //[Phone]
        [RegularExpression(@"^[0-9]*$")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "The {0} must be 10 characters")]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DateGreaterThan("DismissDate")]
        [Display(Name = "Hire date")] 
        public DateTime HireDate { get; set; }

        public bool IsActive { get; set; }

        [Required]
        [Display(Name = "Dismiss date")]
        public DateTime DismissDate { get; set; }
    }
}
