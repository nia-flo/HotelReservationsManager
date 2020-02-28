using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservationsManager.Models.ClientViewModels
{
    public class ClientEditViewModel
    {
        public string Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        [RegularExpression("^[a-zA-Z-]*$", ErrorMessage = "Only letters and dashes allowed.")]
        [Display(Name = "First name")]

        public string FirstName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        [RegularExpression("^[a-zA-Z-]*$", ErrorMessage = "Only letters and dashes allowed.")]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

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

        public bool IsAdult { get; set; }
    }
}
