using Microsoft.AspNetCore.Identity;
using System;

namespace HotelReservationsManager.Data.Models
{
    public class User : IdentityUser<string>
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string EGN { get; set; }

        public DateTime HireDate { get; set; }

        public bool IsActive { get; set; }

        public DateTime DismissDate { get; set; }
    }
}
