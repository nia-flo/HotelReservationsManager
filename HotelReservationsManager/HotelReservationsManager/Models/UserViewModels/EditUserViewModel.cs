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
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        public string MiddleName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        public string LastName { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*$")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "The {0} must be 10 characters")]
        public string EGN { get; set; }

        [Required]
        //[Phone]
        [RegularExpression(@"^[0-9]*$")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "The {0} must be 10 characters")]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DateGreaterThan("DismissDate")]
        public DateTime HireDate { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public DateTime DismissDate { get; set; }
    }
}
